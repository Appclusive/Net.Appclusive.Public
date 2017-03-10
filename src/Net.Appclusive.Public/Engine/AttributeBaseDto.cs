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
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using biz.dfch.CS.Commons;
using biz.dfch.CS.Commons.Converters;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Engine
{
    [JsonObject]
    [Serializable]
    public abstract class AttributeBaseDto : ConvertibleBaseDto, IBehaviourOrAttribute, ISerializable
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        // this class acts only as a base for Dtos 
        // that need to convert Attribute entities

        protected AttributeBaseDto()
        {
            // parameter-less constructor needed for conversion
        }

        [SecurityCritical]
        protected AttributeBaseDto(SerializationInfo info, StreamingContext context)
        {
            try
            {
                // we need to elevate privileges to use SetValue
                new PermissionSet(PermissionState.Unrestricted).Assert();

                var propertyInfos = this.GetType().GetProperties(BINDING_FLAGS);
                foreach (var propertyInfo in propertyInfos)
                {
                    var name = propertyInfo.Name;
                    var value = info.GetValue(name, propertyInfo.PropertyType);
                    propertyInfo.SetValue(this, value, null);
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }
        
        public DictionaryParameters Convert()
        {
            var result = Converter.Convert<AttributeNameAttribute>(this, false);
            return result;
        }

        public DictionaryParameters Convert(bool includeAllProperties)
        {
            var result = Converter.Convert<AttributeNameAttribute>(this, includeAllProperties);
            return result;
        }

        [SecurityCritical]
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var propertyInfos = this.GetType().GetProperties(BINDING_FLAGS);
            foreach (var propertyInfo in propertyInfos)
            {
                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(this, null);
                info.AddValue(name, value);
            }
        }
        
        [SecurityCritical]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException(nameof(info));
            }

            GetObjectData(info, context);
        }
    }
}
