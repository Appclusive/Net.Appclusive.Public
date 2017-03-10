/**
 * Copyright 2017 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using biz.dfch.CS.Commons;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Engine
{
    [Serializable]
    public class StateMachine : BaseDto, IEnumerable
    {
        private readonly List<StateMachineTransition> stateMachineTransitions = new List<StateMachineTransition>();

        private const char DELIMITER = '-';
        private const int ARRAY_LENGTH = 2;
        private const int SOURCE_INDEX = 0;
        private const int ACTION_INDEX = 1;

        public const string STATE_NAME_INITIALSTATE = nameof(BaseModelStates.InitialState);
        public const string STATE_NAME_DECOMMISSIONEDSTATE = nameof(BaseModelStates.DecommissionedState);
        public const string STATE_NAME_FINALSTATE = nameof(BaseModelStates.FinalState);
        public const string STATE_NAME_ERRORSTATE = nameof(BaseModelStates.ErrorState);

        public const int INTERSECTING_STATES_COUNT = 4;

        public const string ACTION_NAME_INITIALISE = nameof(BaseModel.Initialise);
        public const string ACTION_NAME_FINALISE = nameof(BaseModel.Finalise);
        public const string ACTION_NAME_REMEDEY = nameof(BaseModel.Remedy);

        public const int INTERSECTING_ACTIONS_COUNT = 3;

        #region ========== extending StateMachine ==========
        public static StateMachine Create(string jsonEncodedStateMachineDefinition)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(jsonEncodedStateMachineDefinition));

            var result = StateMachine.DeserializeObject<StateMachine>(jsonEncodedStateMachineDefinition);
            return result;
        }

        public void Add(Expression<Func<ModelState>> source, Type action, Expression<Func<ModelState>> destination)
        {
            Contract.Requires(null != source);
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Requires(null != destination);

            // we have to call the constructor and must not use the object initialiser (with the default constructor), 
            // otherwise we would generate a ContractInvariant exception
            stateMachineTransitions.Add(new StateMachineTransition(source, action, destination));
        }

        internal void Add(string source, string action, string destination)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(source));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(destination));

            stateMachineTransitions.Add(new StateMachineTransition(source, action, destination));
        }
        #endregion
        
        public override string ToString()
        {
            var stateMachineAsJsonString = stateMachineTransitions
                .ToDictionary(transition => string.Concat(transition.Source, DELIMITER, transition.Action), action => action.Destination);
            return JsonConvert.SerializeObject(stateMachineAsJsonString, Formatting.None);
        }

        public IEnumerator GetEnumerator()
        {
            return stateMachineTransitions.GetEnumerator();
        }

        #region ========== get information about StateMachine ==========

        public IList<StateMachineTransition> Transitions
        {
            get
            {
                Contract.Ensures(null != Contract.Result<IList<StateMachineTransition>>());
                Contract.Ensures(Contract.Result<IList<StateMachineTransition>>().Any());

                return stateMachineTransitions;
            }
        }
            
        public IList<string> SourceStates
        {
            get
            {
                Contract.Ensures(null != Contract.Result<IList<string>>());
                Contract.Ensures(Contract.Result<IList<string>>().Any());

                return stateMachineTransitions.Select(stateMachineTransition => stateMachineTransition.Source)
                    .Distinct()
                    .ToList();
            }
        }
            
        public IList<string> DestinationStates
        {
            get
            {
                Contract.Ensures(null != Contract.Result<IList<string>>());
                Contract.Ensures(Contract.Result<IList<string>>().Any());

                return stateMachineTransitions.Select(stateMachineTransition => stateMachineTransition.Destination)
                    .Distinct()
                    .ToList();
            }
        }
            
        public IList<string> States
        {
            get
            {
                Contract.Ensures(null != Contract.Result<IList<string>>());
                Contract.Ensures(2 <= Contract.Result<IList<string>>().Count);

                // probably a simple foreach with Add to a precreated list is faster
                return stateMachineTransitions.Select(stateMachineTransition => stateMachineTransition.Source)
                    .Union(stateMachineTransitions.Select(stateMachineTransition => stateMachineTransition.Destination))
                    .Distinct()
                    .ToList();
            }
        }

        public IList<string> Actions
        {
            get
            {
                Contract.Ensures(null != Contract.Result<IList<string>>());
                Contract.Ensures(Contract.Result<IList<string>>().Any());

                return stateMachineTransitions
                    .Select(stateMachineTransition => stateMachineTransition.Action)
                    .Distinct()
                    .ToList();
            }
        }

        public IList<string> GetActions(string sourceState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Ensures(null != Contract.Result<IList<string>>());

            return GetActions(sourceState, allowEmptyResult: false);
        }

        public IList<string> GetActions(string sourceState, bool allowEmptyResult)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Ensures(null != Contract.Result<IList<string>>());
            Contract.Ensures(allowEmptyResult || Contract.Result<IList<string>>().Any());

            return stateMachineTransitions
                .Where(stateMachineTransition => stateMachineTransition.Source == sourceState)
                .Select(e => e.Action)
                .Distinct()
                .ToList();
        }

        public IList<StateMachineTransition> GetTransitions(string sourceState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Ensures(null != Contract.Result<IList<StateMachineTransition>>());

            return GetTransitions(sourceState, allowEmptyResult: false);
        }

        public IList<StateMachineTransition> GetTransitions(string sourceState, bool allowEmptyResult)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Ensures(null != Contract.Result<IList<StateMachineTransition>>());
            Contract.Ensures(allowEmptyResult || Contract.Result<IList<StateMachineTransition>>().Any());

            return stateMachineTransitions
                .Where(stateMachineTransition => stateMachineTransition.Source == sourceState)
                .ToList();
        }

        public IList<string> GetActionsTo(string destinationState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(destinationState));
            Contract.Ensures(null != Contract.Result<IList<string>>());

            return GetActionsTo(destinationState, allowEmptyResult: false);
        }

        public IList<string> GetActionsTo(string destinationState, bool allowEmptyResult)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(destinationState));
            Contract.Ensures(null != Contract.Result<IList<string>>());
            Contract.Ensures(allowEmptyResult || Contract.Result<IList<string>>().Any());

            return stateMachineTransitions
                .Where(stateMachineTransition => stateMachineTransition.Destination == destinationState)
                .Select(e => e.Action)
                .Distinct()
                .ToList();
        }

        public IList<StateMachineTransition> GetTransitionsTo(string destinationState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(destinationState));
            Contract.Ensures(null != Contract.Result<IList<StateMachineTransition>>());

            return GetTransitionsTo(destinationState, allowEmptyResult: false);
        }

        public IList<StateMachineTransition> GetTransitionsTo(string destinationState, bool allowEmptyResult)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(destinationState));
            Contract.Ensures(null != Contract.Result<IList<StateMachineTransition>>());
            Contract.Ensures(allowEmptyResult || Contract.Result<IList<StateMachineTransition>>().Any());

            return stateMachineTransitions
                .Where(stateMachineTransition => stateMachineTransition.Destination == destinationState)
                .ToList();
        }

        public string GetDestination(string action)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            return GetDestination(action, allowEmptyResult: false);
        }

        public string GetDestination(string action, bool allowEmptyResult)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Ensures(allowEmptyResult || !string.IsNullOrWhiteSpace(Contract.Result<string>()));

            return stateMachineTransitions
                .Where(stateMachineTransition => stateMachineTransition.Action == action)
                .Select(e => e.Destination)
                .FirstOrDefault();
        }

        #endregion

        #region ========== Validation ==========

        public override bool IsValid()
        {
            return 0 >= TryValidate().Count;
        }

        public override IList<ValidationResult> GetValidationResults()
        {
            return TryValidate();
        }

        public override IList<string> GetErrorMessages()
        {
            return TryValidate().Select(result => result.ErrorMessage).ToList();
        }
        
        private List<ValidationResult> TryValidate()
        {
            var results = new List<ValidationResult>();
            
            // find duplicate items
            var duplicates = stateMachineTransitions
                .GroupBy(e => new {Source = e.Source, Action = e.Action, Destination = e.Destination})
                .Where(e => e.Count() > 1)
                .Select(e => e.Key).ToList();
            
            if (0 < duplicates.Count)
            {
                results.AddRange
                (
                    duplicates
                    .Select(item => string.Format(Message.StateMachine_TryValidate__Duplicate_State_Action, item.Source, item.Action, item.Destination))
                    .Select(message => new ValidationResult(message))
                );
            }

            #region InitialState and Initialise
            var hasInvalidInitialStateAsDestination = stateMachineTransitions.Any(e => e.Destination == STATE_NAME_INITIALSTATE);
            if (hasInvalidInitialStateAsDestination)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__InitialState_Destination);
                results.Add(validationResult);
            }

            var hasActionWithInitialStateAsSource = stateMachineTransitions.Any(e => e.Source == STATE_NAME_INITIALSTATE);
            if (!hasActionWithInitialStateAsSource)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__InitialState_Source);
                results.Add(validationResult);
            }

            var actionsWithInitialiseAction = stateMachineTransitions.Count(e => e.Action == ACTION_NAME_INITIALISE);
            if (1 != actionsWithInitialiseAction)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__InitialState);
                results.Add(validationResult);
            }

            var initialStateTransitions = stateMachineTransitions.Count(e => e.Source == STATE_NAME_INITIALSTATE && e.Action == ACTION_NAME_INITIALISE);
            if (1 != initialStateTransitions)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__InitialState_Initialise);
                results.Add(validationResult);
            }
            #endregion

            #region Decommissioned
            var hasDecommissionedState = stateMachineTransitions.Any(e => e.Destination == STATE_NAME_DECOMMISSIONEDSTATE);
            if (!hasDecommissionedState)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__Decommissioned);
                results.Add(validationResult);
            }

            var decommissionedToFinalStateTranstions = stateMachineTransitions.Count(e => e.Source == STATE_NAME_DECOMMISSIONEDSTATE && e.Action == ACTION_NAME_FINALISE && e.Destination == STATE_NAME_FINALSTATE);
            if (1 != decommissionedToFinalStateTranstions)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__Decommissioned_Finalise_FinalState);
                results.Add(validationResult);
            }
            #endregion

            #region FinalState and Finalise
            var hasExactlyOneFinalState = stateMachineTransitions.Count(e => e.Destination == STATE_NAME_FINALSTATE);
            if (1 != hasExactlyOneFinalState)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__FinalState);
                results.Add(validationResult);
            }

            var actionsWithFinaliseAction = stateMachineTransitions.Count(e => e.Action == ACTION_NAME_FINALISE);
            if (1 != actionsWithFinaliseAction)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__Finalise);
                results.Add(validationResult);
            }

            var finalStateActions = stateMachineTransitions.Count(e => e.Destination == STATE_NAME_FINALSTATE && e.Action == ACTION_NAME_FINALISE);
            if (1 != finalStateActions)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__FinalState_Finalise);
                results.Add(validationResult);
            }

            var hasInvalidFinalStateAsSource = stateMachineTransitions.Any(e => e.Source == STATE_NAME_FINALSTATE);
            if (hasInvalidFinalStateAsSource)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__FinalState_Source);
                results.Add(validationResult);
            }
            #endregion

            #region ErrorState
            var hasInvalidErrorTransition = 1 != stateMachineTransitions.Count(e => e.Source == STATE_NAME_ERRORSTATE && e.Action == ACTION_NAME_REMEDEY);
            if (hasInvalidErrorTransition)
            {
                var validationResult = new ValidationResult(Message.StateMachine_TryValidate__ErrorState_Remedy);
                results.Add(validationResult);
            }
            #endregion

            return results;
        }
        
        public override void Validate()
        {
            var results = TryValidate();
            var isValid = 0 >= results.Count;

            if (isValid)
            {
                return;
            }

            foreach (var result in results)
            {
                Contract.Assert(isValid, result.ErrorMessage);
            }
        }

        #endregion

        #region ========== Serialisation ==========

        // DFTODO - why do we specify "Type type" parameter here, but do not use it ?
        public new static object DeserializeObject(string value, Type type)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value));
            Contract.Requires(type.IsAssignableFrom(typeof(StateMachine)));
            Contract.Ensures(null != Contract.Result<object>());

            var result = new StateMachine();

            var stateActions = BaseDto.DeserializeObject<Dictionary<string, string>>(value);
            foreach (var stateActionItem in stateActions)
            {
                var sourceAndActionArray = stateActionItem.Key.Split(DELIMITER);
                Contract.Assert(ARRAY_LENGTH == sourceAndActionArray.Length);

                var source = sourceAndActionArray[SOURCE_INDEX];
                var action = sourceAndActionArray[ACTION_INDEX];
                var destination = stateActionItem.Value;

                var stateAction = new StateMachineTransition(source, action, destination);
                result.stateMachineTransitions.Add(stateAction);
            }

            return result;
        }

        public new static T DeserializeObject<T>(string value)
            where T : StateMachine
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value));
            Contract.Ensures(null != Contract.Result<T>());

            var result = StateMachine.DeserializeObject(value, typeof(StateMachine)) as T;
            return result;
        }

        #endregion

        public StateMachine Clone()
        {
            Contract.Ensures(null != Contract.Result<StateMachine>());

            var newStateMachine = new StateMachine();

            foreach (var transition in Transitions)
            {
                newStateMachine.Add(transition.Source, transition.Action, transition.Destination);
            }

            return newStateMachine;
        }

        internal StateMachine Merge(StateMachine stateMachine, string sourceState, string action)
        {
            Contract.Requires(null != stateMachine);
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(INTERSECTING_STATES_COUNT == States.Intersect(stateMachine.States).Count());
            Contract.Requires(INTERSECTING_ACTIONS_COUNT == Actions.Intersect(stateMachine.Actions).Count());
            Contract.Ensures(null != Contract.Result<StateMachine>());
            
            foreach (var transition in stateMachine.Transitions)
            {
                if (STATE_NAME_INITIALSTATE == transition.Source && ACTION_NAME_INITIALISE == transition.Action)
                {
                    this.Add(sourceState, action, transition.Destination);
                    continue;
                }

                if (STATE_NAME_DECOMMISSIONEDSTATE == transition.Source && ACTION_NAME_FINALISE == transition.Action && STATE_NAME_FINALSTATE == transition.Destination)
                {
                    continue;
                }

                if (STATE_NAME_ERRORSTATE == transition.Source && ACTION_NAME_REMEDEY == transition.Action)
                {
                    continue;
                }

                this.Add(transition.Source, transition.Action, transition.Destination);
            }

            return this;
        }
    }
}
