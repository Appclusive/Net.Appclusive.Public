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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using biz.dfch.CS.Commons;

namespace Net.Appclusive.Public.Engine
{
    public class ModelValidator : BaseDto
    {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        private readonly BaseModel instance;

        public ModelValidator(Type model)
            : this(Activator.CreateInstance(model) as BaseModel)
        {
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));
        }

        public ModelValidator(BaseModel instance)
        {
            Contract.Requires(null != instance);

            this.instance = instance;
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

        private IList<ValidationResult> TryValidate()
        {
            var behaviourDefinitions = BehaviourManager.GetBehaviourDefinitionsOf(instance.GetType());

            var result = new List<ValidationResult>();

            // ReSharper disable once UseIsOperator.2
            if (typeof(BaseModelAction).IsInstanceOfType(instance))
            {
                
            }
            else
            {
                
            }

            foreach (var behaviourDefinition in behaviourDefinitions)
            {
                var behaviourDefinitionInstance = Activator.CreateInstance(behaviourDefinition) as BaseModel;
                Contract.Assert(null != behaviourDefinitionInstance);
                var stateMachine = behaviourDefinitionInstance.GetStateMachine();

                ValidateProperties(behaviourDefinition, result);
            }

            return result;
        }

        private void ValidateProperties(Type behaviourDefinition, IList<ValidationResult> validationResults)
        {
            Contract.Requires(null != behaviourDefinition);
            Contract.Requires(null != validationResults);

            var modelPropertyInfos = instance.GetType().GetProperties(FLAGS);

            var behaviourDefinitionPropertyInfos = behaviourDefinition.GetProperties(FLAGS);
            foreach (var behaviourDefinitionPropertyInfo in behaviourDefinitionPropertyInfos)
            {
                var behaviourDefinitionAttribute = behaviourDefinitionPropertyInfo.GetCustomAttribute<AttributeNameAttribute>();

                var isAttributeDefined = false;

                foreach (var modelPropertyInfo in modelPropertyInfos)
                {
                    var modelAttribute = modelPropertyInfo.GetCustomAttribute<AttributeNameAttribute>();
                    if (behaviourDefinitionAttribute.Name != modelAttribute.Name)
                    {
                        continue;
                    }

                    isAttributeDefined = true;
                    break;
                }

                if (!isAttributeDefined)
                {
                    validationResults.Add(new ValidationResult(
                        string.Format(Message.ModelValidator__Missing_Attribute, 
                        instance.GetType().FullName, behaviourDefinitionAttribute.Name, behaviourDefinition.FullName)));
                }
            }

            return;
        }
    }
}