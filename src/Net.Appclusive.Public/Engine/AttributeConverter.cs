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
using System.Diagnostics.Contracts;
using System.Reflection;
using biz.dfch.CS.Commons;
using biz.dfch.CS.Commons.Converters;

namespace Net.Appclusive.Public.Engine
{
    public class AttributeConverter : ConvertibleBaseDtoConverter
    {
        private const string METHOD_NAME_CONVERT_FROM_DICTIONARY = "Convert";
        private static readonly MethodInfo _methodInfoConvertFromDictionary;

        private static readonly IConvertibleBaseDtoConverter _converter;

        static AttributeConverter()
        {
            _converter = new ConvertibleBaseDtoConverter();

            // determine generic Convert method and convert it to non-generic method
            // so it can easily be used from PowerShell
            _methodInfoConvertFromDictionary = typeof(AttributeConverter)
                .GetMethod
                (
                    METHOD_NAME_CONVERT_FROM_DICTIONARY,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy,
                    null,
                    new[] { typeof(DictionaryParameters), typeof(bool) },
                    new ParameterModifier[] { }
                );
            Contract.Assert(null != _methodInfoConvertFromDictionary);
        }

        public static T Convert<T>(string jsonString) where T : AttributeBaseDto, new()
        {
            var dictionaryParameters = new DictionaryParameters(jsonString);
            return Convert<T>(dictionaryParameters);
        }

        public static T Convert<T>(DictionaryParameters dictionaryParameters)
            where T : AttributeBaseDto, new()
        {
            var result = _converter.Convert<T, AttributeNameAttribute>(dictionaryParameters, false);
            return result;
        }

        public static T Convert<T>(DictionaryParameters dictionaryParameters, bool includeAllProperties)
            where T : AttributeBaseDto, new()
        {
            var result = _converter.Convert<T, AttributeNameAttribute>(dictionaryParameters, includeAllProperties);
            return result;
        }

        public static AttributeBaseDto Convert(Type type, DictionaryParameters dictionaryParameters)
        {
            Contract.Requires(null != type);
            Contract.Ensures(null != Contract.Result<AttributeBaseDto>());

            var genericMethodInfo = _methodInfoConvertFromDictionary.MakeGenericMethod(type);
            Contract.Assert(null != genericMethodInfo, type.FullName);

            var result = (AttributeBaseDto)genericMethodInfo.Invoke(typeof(AttributeConverter), new object[] { dictionaryParameters, false });
            return result;
        }

        public static AttributeBaseDto Convert(Type type, DictionaryParameters dictionaryParameters, bool includeAllProperties)
        {
            Contract.Requires(null != type);
            Contract.Ensures(null != Contract.Result<AttributeBaseDto>());

            var genericMethodInfo = _methodInfoConvertFromDictionary.MakeGenericMethod(type);
            Contract.Assert(null != genericMethodInfo, type.FullName);

            var result = (AttributeBaseDto) genericMethodInfo.Invoke(typeof(AttributeConverter), new object[] { dictionaryParameters, includeAllProperties });
            return result;
        }

        public static DictionaryParameters Convert(AttributeBaseDto attributeBaseDto)
        {
            var result = _converter.Convert<AttributeNameAttribute>(attributeBaseDto, false);
            return result;
        }

        public static DictionaryParameters Convert(AttributeBaseDto attributeBaseDto, bool includeAllProperties)
        {
            var result = _converter.Convert<AttributeNameAttribute>(attributeBaseDto, includeAllProperties);
            return result;
        }

        public static IList<TBase> Convert<TBase>(DictionaryParameters dictionaryParameters, string version)
            where TBase : AttributeBaseDto
        {
            var result = _converter.Convert<TBase, AttributeNameAttribute>(dictionaryParameters, version);
            return result;
        }

        public static IList<TBase> Convert<TBase>(DictionaryParameters dictionaryParameters, string version, bool includeAllProperies)
            where TBase : AttributeBaseDto
        {
            var result = _converter.Convert<TBase, AttributeNameAttribute>(dictionaryParameters, version, includeAllProperies);
            return result;
        }
    }
}
