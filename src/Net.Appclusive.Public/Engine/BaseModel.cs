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
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using biz.dfch.CS.Commons;

namespace Net.Appclusive.Public.Engine
{
    [Serializable]
    [BehaviourDefinitionFor(typeof(BaseBehaviour))]
    public class BaseModel : AttributeBaseDto, BaseBehaviour, ISerializable
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        private static readonly ConcurrentDictionary<Type, bool> _typesNotUsingValidationAttributes = new ConcurrentDictionary<Type, bool>();

        private static readonly Lazy<StateMachine> _stateMachine = new Lazy<StateMachine>(() =>
        {
            return new StateMachine
            {
                {() => BaseModelStates.InitialState, typeof(Initialise), () => BaseModelStates.DecommissionedState}
                ,
                {() => BaseModelStates.DecommissionedState, typeof(Finalise), () => BaseModelStates.FinalState}
                ,
                {() => BaseModelStates.ErrorState, typeof(Remedy), () => BaseModelStates.ErrorState}
            };
        });

        public BaseModel()
        {
            // public parameter-less constructor needed 
            // for conversion, serialisation and activation support

            var thisType = GetType();

            if (_typesNotUsingValidationAttributes.ContainsKey(thisType))
            {
                return;
            }

            // we try to set the DefaultValue of the implemented Behaviour
            // 1. if there is one
            // 2. if the UseValidationFrom...Attribute specifies to use the DefaultValue
            // 3. there is a DefaultValueAttribute on that corresponding property

            var propInfos = thisType.GetProperties(BINDING_FLAGS);
            Contract.Assert(null != propInfos);
            var hasDefaultValue = false;

            foreach (var propInfo in propInfos)
            {
                var attribute = propInfo.GetCustomAttribute<UseValidationFromModelAttribute>();
                if (null == attribute)
                {
                    attribute = propInfo.GetCustomAttribute<UseValidationFromBehaviourAttribute>();
                    if (null == attribute)
                    {
                        continue;
                    }
                }

                if (!attribute.UseDefaultValue)
                {
                    continue;
                }

                var attributeNameAttribute = propInfo.GetCustomAttribute<AttributeNameAttribute>();
                if (null == attributeNameAttribute)
                {
                    continue;
                }

                var propertyInfo = attribute.Model
                    .GetProperties(BINDING_FLAGS)
                    .FirstOrDefault(e => e.GetCustomAttribute<AttributeNameAttribute>().Name == attributeNameAttribute.Name);
                if (null == propertyInfo)
                {
                    continue;
                }

                var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                if (null == defaultValueAttribute)
                {
                    continue;
                }

                propInfo.SetValue(this, defaultValueAttribute.Value);
                hasDefaultValue = true;
            }

            if (!hasDefaultValue)
            {
                _typesNotUsingValidationAttributes.AddOrUpdate(thisType, true, (type, value) => true);
            }
        }

        [SecurityCritical]
        protected BaseModel(SerializationInfo info, StreamingContext context)
             : base(info, context)
        {
            // N/A
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        public sealed override bool IsValid()
        {
            return base.IsValid();
        }

        public sealed override bool IsValid(string propertyName)
        {
            return base.IsValid(propertyName);
        }

        public sealed override bool IsValid(string propertyName, object value)
        {
            return base.IsValid(propertyName, value);
        }

        public sealed override IList<string> GetErrorMessages()
        {
            return base.GetErrorMessages();
        }

        public sealed override IList<string> GetErrorMessages(string propertyName)
        {
            return base.GetErrorMessages(propertyName);
        }

        public sealed override IList<string> GetErrorMessages(string propertyName, object value)
        {
            return base.GetErrorMessages(propertyName, value);
        }

        protected class States : BaseModelStates
        {
            // inherit all states from BaseModelStates
        }

        public virtual StateMachine GetStateMachine()
        {
            return _stateMachine.Value;
        }

        public virtual DictionaryParameters GetConfiguration()
        {
            return new DictionaryParameters();
        }

        public class Initialise : BaseModelAction
        {
            // this nested class is a default implementation for the Initialse transition DTO
            // this effectively means, this transition does not expect any parameters
        }

        public class Finalise : BaseModelAction
        {
            // this nested class is a default implementation for the Finalise transition DTO
            // this effectively means, this transition does not expect any parameters
        }

        public class Remedy : BaseModelAction
        {
            // this nested class is a default implementation for the Finalise transition DTO
            // this effectively means, this transition does not expect any parameters
        }
    }
}
