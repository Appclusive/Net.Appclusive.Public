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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using biz.dfch.CS.Commons.Validation;

namespace Net.Appclusive.Public.Engine
{
    public class UseValidationFromModelAttribute : DataTypeValidationBaseAttribute
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        public Type Model { get; }

        public override bool RequiresValidationContext { get; } = true;

        public bool UseDefaultValue { get; set; }

        protected UseValidationFromModelAttribute(Type behaviour, bool typeIsBehaviour)
        {
            Contract.Requires(null != behaviour);
            Contract.Requires(behaviour.IsInterface);
            Contract.Requires(typeof(BaseBehaviour).IsAssignableFrom(behaviour));

            Model = BehaviourManager.GetBehaviourDefinitionOf(behaviour);
        }

        public UseValidationFromModelAttribute(Type model)
        {
            // DFTODO - move to validation methods
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));

            Model = model;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var modelPropertyInfo = validationContext
                .ObjectInstance
                .GetType()
                .GetProperty(validationContext.MemberName, BINDING_FLAGS);
            Contract.Assert(null != modelPropertyInfo);

            var modelAttribute = modelPropertyInfo.GetCustomAttribute<AttributeNameAttribute>();
            Contract.Assert(null != modelAttribute);

            var instance = Activator.CreateInstance(Model) as BaseModel;
            Contract.Assert(null != instance);

            var propertyInfo = Model
                .GetProperties(BINDING_FLAGS)
                .FirstOrDefault(e => e.GetCustomAttribute<AttributeNameAttribute>().Name == modelAttribute.Name);
            Contract.Assert(null != propertyInfo);

            propertyInfo.SetValue(instance, value, null);

            var validationResult = instance.GetValidationResults(propertyInfo.Name, value);
            return validationResult.FirstOrDefault();
        }
    }
}
