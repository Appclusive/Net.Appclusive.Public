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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using biz.dfch.CS.Commons;

namespace Net.Appclusive.Public.Engine
{
    public class StateMachineValidator : BaseDto
    {
        private readonly object syncRoot = new object();
        private readonly Type modelToBeValidated;

        public StateMachineValidator(Type modelToBeValidated)
        {
            Contract.Requires(null != modelToBeValidated);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(modelToBeValidated));
            Contract.Requires(null != modelToBeValidated.BaseType);

            this.modelToBeValidated = modelToBeValidated;
        }

        public override bool IsValid()
        {
            return !TryValidate().Any();
        }

        public override IList<ValidationResult> GetValidationResults()
        {
            return TryValidate();
        }

        public override void Validate()
        {
            var results = TryValidate();

            var isValid = !results.Any();

            if (isValid)
            {
                return;
            }

            Contract.Assert(isValid, results[0].ErrorMessage);
        }

        public override IList<string> GetErrorMessages()
        {
            return TryValidate().Select(e => e.ErrorMessage).ToList();
        }

        #region ########## not implemented overridden members ##########
        public override bool IsValid(string propertyName)
        {
            throw new NotImplementedException();
        }

        public override bool IsValid(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override IList<ValidationResult> GetValidationResults(string propertyName)
        {
            throw new NotImplementedException();
        }

        public override IList<ValidationResult> GetValidationResults(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override IList<string> GetErrorMessages(string propertyName)
        {
            throw new NotImplementedException();
        }

        public override IList<string> GetErrorMessages(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override void Validate(string propertyName)
        {
            throw new NotImplementedException();
        }

        public override void Validate(string propertyName, object value)
        {
            throw new NotImplementedException();
        }
        #endregion

        private List<ValidationResult> TryValidate()
        {
            var validatedTypes = new ConcurrentBag<Type>();

            // 1. get BaseType and StateMachine
            var instanceOfModelToBeValidated = Activator.CreateInstance(modelToBeValidated) as BaseModel;
            Contract.Assert(null != instanceOfModelToBeValidated, modelToBeValidated.FullName);

            var stateMachine = instanceOfModelToBeValidated.GetStateMachine();
            var states = stateMachine.States;
            var actions = stateMachine.Actions;
            var results = new ConcurrentBag<ValidationResult>(stateMachine.GetValidationResults());

            if (typeof(AttributeBaseDto) != modelToBeValidated.BaseType)
            {
                // checked already in constructor
                // ReSharper disable once AssignNullToNotNullAttribute
                var modelBaseTypeInstance = Activator.CreateInstance(modelToBeValidated.BaseType) as BaseModel;

                validatedTypes.Add(modelToBeValidated.BaseType);

                // type checked already in constructor
                // ReSharper disable once PossibleNullReferenceException
                var baseTypeStateMachine = modelBaseTypeInstance.GetStateMachine();

                foreach(var missingState in baseTypeStateMachine.States.Where(state => !states.Contains(state)))
                {
                    results.Add(new ValidationResult(string.Format(Message.StateMachineValidator_TryValidate__DerivedModelMissingState,
                                modelToBeValidated.FullName, modelToBeValidated.BaseType.FullName, missingState)));
                }

                foreach(var missingAction in baseTypeStateMachine.Actions.Where(action => !actions.Contains(action)))
                {
                    results.Add(new ValidationResult(string.Format(Message.StateMachineValidator_TryValidate__DerivedModelMissingAction, 
                                modelToBeValidated.FullName, modelToBeValidated.BaseType.FullName, missingAction)));
                }
            
            }

            // 2. get all class type definitions of inherited class types (that inherit from INodeType and have an annotation)
            var behaviourDefinitions = BehaviourManager.GetBehaviourDefinitionsOf(modelToBeValidated);

            // 3. foreach definition validate StateMachine
            Parallel.ForEach(behaviourDefinitions, behaviourDefinition =>
            {
                // only validate if not done already
                if (validatedTypes.Contains(behaviourDefinition))
                {
                    return;
                }

                lock (syncRoot)
                {
                    if (validatedTypes.Contains(behaviourDefinition))
                    {
                        return;
                    }
                    validatedTypes.Add(behaviourDefinition);
                }
                    
                var behaviourDefinitionInstance = Activator.CreateInstance(behaviourDefinition) as BaseModel;
                Contract.Assert(null != behaviourDefinitionInstance, behaviourDefinition.FullName);

                var stateMachineFromImplementedBehaviour = behaviourDefinitionInstance.GetStateMachine();

                // 4. compare given FSM with all other required states and actions
                foreach(var missingState in stateMachineFromImplementedBehaviour.States.Where(state => !states.Contains(state)))
                {
                    results.Add(new ValidationResult(string.Format(Message.StateMachineValidator_TryValidate__DerivedModelMissingState,
                                modelToBeValidated.FullName, modelToBeValidated.BaseType.FullName, missingState)));
                }

                foreach(var missingAction in stateMachineFromImplementedBehaviour.Actions.Where(action => !actions.Contains(action)))
                {
                    results.Add(new ValidationResult(string.Format(Message.StateMachineValidator_TryValidate__DerivedModelMissingAction, 
                                modelToBeValidated.FullName, modelToBeValidated.BaseType.FullName, missingAction)));
                }
            });

            return results.ToList();
        }
    }
}
