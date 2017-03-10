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
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Net.Appclusive.Public.Engine
{
    public class StateMachineBuilder
    {
        private const string SEPARATOR = ", ";

        private readonly Type model;
        private StateMachine stateMachine;

        public static StateMachineBuilder For<TModel>()
            where TModel : BaseModel
        {
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return new StateMachineBuilder(typeof(TModel));
        }

        public static StateMachineBuilder For(Type model)
        {
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return new StateMachineBuilder(model);
        }

        private StateMachineBuilder()
            : this(typeof(BaseModel))
        {
            // N/A
        }

        private StateMachineBuilder(Type model)
        {
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));

            this.model = model;

            var modelBaseType = model.BaseType;
            Contract.Assert(null != model.BaseType, model.FullName);
            Contract.Assert(typeof(AttributeBaseDto) == modelBaseType || typeof(BaseModel).IsAssignableFrom(modelBaseType));

            var instance = Activator.CreateInstance
            (
                // ReSharper disable once AssignNullToNotNullAttribute
                typeof(AttributeBaseDto) == modelBaseType
                    ? model
                    : modelBaseType
            ) as BaseModel;
            // ReSharper disable once PossibleNullReferenceException
            Contract.Assert(null != instance, modelBaseType.FullName);

            //Logger.Get(Logging.TraceSourceName.ENGINE).TraceEvent(TraceEventType.Verbose, (int) Logging.ProductEngineEventId.StateMachineRetrieval, 
            //    Message.StateMachineBuilder_StateMachineBuilder__RetrievingStateMachine1, model.FullName, instance.GetType().FullName);

            // ReSharper disable once AssignNullToNotNullAttribute
            var retrievedStateMachine = instance.GetStateMachine();

            //Logger.Get(Logging.TraceSourceName.ENGINE).TraceEvent(TraceEventType.Information, (int) Logging.ProductEngineEventId.StateMachineRetrieval, 
            //    Message.StateMachineBuilder_StateMachineBuilder__RetrievingStateMachine2, model.FullName, instance.GetType().FullName, 
            //        string.Join(SEPARATOR, retrievedStateMachine.States), 
            //        string.Join(SEPARATOR, retrievedStateMachine.Actions));

            this.stateMachine = retrievedStateMachine;
        }

        public StateMachineBuilder InsertBefore(Type action, Type actionToBePrepended, Expression<Func<ModelState>> stateToBePrepended)
        {
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Requires(null != actionToBePrepended);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(actionToBePrepended));
            Contract.Requires(null != stateToBePrepended);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return InsertBefore(action.Name, actionToBePrepended.Name, StateMachineTransition.GetNodeTypeStateFieldName(stateToBePrepended));
        }

        internal StateMachineBuilder InsertBefore(string action, string actionToBePrepended, string stateToBePrepended)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(actionToBePrepended));
            Contract.Requires(action != actionToBePrepended);
            Contract.Requires(!string.IsNullOrWhiteSpace(stateToBePrepended));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(stateMachine.Actions.Contains(action), action);
            Contract.Assert(!stateMachine.Actions.Contains(actionToBePrepended), actionToBePrepended);

            var newStateMachine = new StateMachine();

            foreach (var stateMachineTransition in stateMachine.Transitions)
            {
                if (stateMachineTransition.Action != action)
                {
                    newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);

                    continue;
                }

                Contract.Assert(stateMachineTransition.Source != stateToBePrepended, stateToBePrepended);
                newStateMachine.Add(stateToBePrepended, actionToBePrepended, stateMachineTransition.Source);
                newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);
            }

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachineBuilder InsertAfter(Type action, Expression<Func<ModelState>> stateToBeAppended, Type actionToBeAppended)
        {
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Requires(null != actionToBeAppended);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(actionToBeAppended));
            Contract.Requires(null != stateToBeAppended);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return InsertAfter(action.Name, StateMachineTransition.GetNodeTypeStateFieldName(stateToBeAppended), actionToBeAppended.Name);
        }

        internal StateMachineBuilder InsertAfter(string action, string stateToBeAppended, string actionToBeAppended)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(stateToBeAppended));
            Contract.Requires(!string.IsNullOrWhiteSpace(actionToBeAppended));
            Contract.Requires(action != actionToBeAppended);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(stateMachine.Actions.Contains(action), action);
            Contract.Assert(!stateMachine.Actions.Contains(actionToBeAppended), actionToBeAppended);

            var newStateMachine = new StateMachine();

            foreach (var stateMachineTransition in stateMachine.Transitions)
            {
                if (stateMachineTransition.Action != action)
                {
                    newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);

                    continue;
                }

                Contract.Assert(stateMachineTransition.Source != stateToBeAppended, stateToBeAppended);
                newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateToBeAppended);
                newStateMachine.Add(stateToBeAppended, actionToBeAppended, stateMachineTransition.Destination);
            }

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachineBuilder InsertAction(Expression<Func<ModelState>> sourceState, Type actionToBeInserted, Expression<Func<ModelState>> destinationState)
        {
            Contract.Requires(null != sourceState);
            Contract.Requires(null != actionToBeInserted);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(actionToBeInserted));
            Contract.Requires(null != destinationState);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return InsertAction(StateMachineTransition.GetNodeTypeStateFieldName(sourceState), actionToBeInserted.Name, StateMachineTransition.GetNodeTypeStateFieldName(destinationState));
        }

        internal StateMachineBuilder InsertAction(string sourceState, string actionToBeInserted, string destinationState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Requires(!string.IsNullOrWhiteSpace(actionToBeInserted));
            Contract.Requires(!string.IsNullOrWhiteSpace(destinationState));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(stateMachine.SourceStates.Contains(sourceState), sourceState);
            Contract.Assert(!stateMachine.Actions.Contains(actionToBeInserted), actionToBeInserted);
            Contract.Assert(stateMachine.DestinationStates.Contains(destinationState), destinationState);

            var newStateMachine = new StateMachine();
            var hasActionBeenAdded = false;

            foreach (var stateMachineTransition in stateMachine.Transitions)
            {
                newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);

                if (!hasActionBeenAdded && stateMachineTransition.Source == sourceState)
                {
                    newStateMachine.Add(sourceState, actionToBeInserted, destinationState);
                    hasActionBeenAdded = true;
                }
            }

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachineBuilder ChangeSourceState(Expression<Func<ModelState>> existingSourceState, Type action, Expression<Func<ModelState>> newSourceState)
        {
            Contract.Requires(null != existingSourceState);
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Requires(null != newSourceState);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return ChangeSourceState(StateMachineTransition.GetNodeTypeStateFieldName(existingSourceState), action.Name, StateMachineTransition.GetNodeTypeStateFieldName(newSourceState));
        }

        internal StateMachineBuilder ChangeSourceState(string existingSourceState, string action, string newSourceState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(existingSourceState));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(newSourceState));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(stateMachine.SourceStates.Contains(existingSourceState), existingSourceState);
            Contract.Assert(stateMachine.Actions.Contains(action), action);
            Contract.Assert(stateMachine.SourceStates.Contains(newSourceState), newSourceState);

            var newStateMachine = new StateMachine();

            foreach (var stateMachineTransition in stateMachine.Transitions)
            {
                if (stateMachineTransition.Action != action)
                {
                    newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);
                    continue;
                }

                newStateMachine.Add(newSourceState, stateMachineTransition.Action, stateMachineTransition.Destination);
            }

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachineBuilder ChangeDestination(Expression<Func<ModelState>> existingDestinationState, Type action, Expression<Func<ModelState>> newDestinationState)
        {
            Contract.Requires(null != existingDestinationState);
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Requires(null != newDestinationState);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return ChangeDestination(StateMachineTransition.GetNodeTypeStateFieldName(existingDestinationState), action.Name, StateMachineTransition.GetNodeTypeStateFieldName(newDestinationState));
        }

        internal StateMachineBuilder ChangeDestination(string existingDestinationState, string action, string newDestinationState)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(existingDestinationState));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(newDestinationState));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(stateMachine.SourceStates.Contains(existingDestinationState), existingDestinationState);
            Contract.Assert(stateMachine.Actions.Contains(action), action);
            Contract.Assert(stateMachine.SourceStates.Contains(newDestinationState), newDestinationState);

            var newStateMachine = new StateMachine();

            foreach (var stateMachineTransition in stateMachine.Transitions)
            {
                if (stateMachineTransition.Action != action)
                {
                    newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, stateMachineTransition.Destination);
                    continue;
                }

                newStateMachine.Add(stateMachineTransition.Source, stateMachineTransition.Action, newDestinationState);
            }

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachineBuilder MergeWithBehaviour<TBehaviour>(Type action)
            where TBehaviour : BaseBehaviour
        {
            Contract.Requires(null != action);
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return MergeWithBehaviour(typeof(TBehaviour), nameof(BaseModelStates.InitialState), action.Name);
        }

        public StateMachineBuilder MergeWithBehaviour<TBehaviour>(Expression<Func<ModelState>> sourceState, Type action)
            where TBehaviour : BaseBehaviour
        {
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return MergeWithBehaviour(typeof(TBehaviour), StateMachineTransition.GetNodeTypeStateFieldName(sourceState), action.Name);
        }

        public StateMachineBuilder MergeWithBehaviour(Type behaviour, Type action)
        {
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return MergeWithBehaviour(behaviour, () => BaseModelStates.InitialState, action);
        }

        public StateMachineBuilder MergeWithBehaviour(Type behaviour, Expression<Func<ModelState>> sourceState, Type action)
        {
            Contract.Requires(null != behaviour);
            Contract.Requires(behaviour.IsInterface);
            Contract.Requires(typeof(BaseBehaviour).IsAssignableFrom(behaviour));
            Contract.Requires(null != action);
            Contract.Requires(typeof(BaseModelAction).IsAssignableFrom(action));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());

            return MergeWithBehaviour(behaviour, StateMachineTransition.GetNodeTypeStateFieldName(sourceState), action.Name);
        }

        internal StateMachineBuilder MergeWithBehaviour(Type behaviour, string sourceState, string action)
        {
            Contract.Requires(null != behaviour);
            Contract.Requires(behaviour.IsInterface);
            Contract.Requires(!string.IsNullOrWhiteSpace(sourceState));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Ensures(null != Contract.Result<StateMachineBuilder>());
            Contract.Assert(!stateMachine.Actions.Contains(action), action);

            var behaviourDefinition = BehaviourManager.GetBehaviourDefinitionOf(behaviour);

            var behaviourDefinitionInstance = Activator.CreateInstance(behaviourDefinition) as BaseModel;
            Contract.Assert(null != behaviourDefinitionInstance, behaviour.FullName);

            //Logger.Get(Logging.TraceSourceName.ENGINE).TraceEvent(TraceEventType.Verbose, (int) Logging.ProductEngineEventId.StateMachineRetrieval, 
            //    Message.StateMachineBuilder_StateMachineBuilder__RetrievingStateMachine1, behaviourDefinition.FullName, behaviourDefinitionInstance.GetType().FullName);

            var stateMachineFromBehaviourDefinition = behaviourDefinitionInstance.GetStateMachine();

            //Logger.Get(Logging.TraceSourceName.ENGINE).TraceEvent(TraceEventType.Verbose, (int) Logging.ProductEngineEventId.StateMachineRetrieval, 
            //    Message.StateMachineBuilder_StateMachineBuilder__RetrievingStateMachine1, behaviourDefinition.FullName, behaviourDefinitionInstance.GetType().FullName);
                
            var newStateMachine = stateMachine.Clone();
            newStateMachine.Merge(stateMachineFromBehaviourDefinition, sourceState, action);

            stateMachine = newStateMachine;

            return this;
        }

        public StateMachine GetStateMachine()
        {
            Contract.Ensures(null != Contract.Result<StateMachine>());

            return stateMachine.Clone();
        }
    }
}
