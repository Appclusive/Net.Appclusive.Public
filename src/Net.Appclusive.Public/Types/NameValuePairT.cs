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

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Net.Appclusive.Public.Types
{
    public class NameValuePairT<TName, TValue> : NameValuePairBase
    {
        public NameValuePairT()
        {
            // parameterless constructor needed for LINQ to Entities
        }

        // we use key instead of name as constructor paramete rname to prevent
        // AutoMapper from using this constructor
        public NameValuePairT(TName key, TValue value)
        {
            Name = key;
            Value = value;
        }

        public static implicit operator DictionaryEntry(NameValuePairT<TName, TValue> nameValuePair)
        {
            return new DictionaryEntry(nameValuePair.Name, nameValuePair.Value);
        }

        public static implicit operator KeyValuePair<string, object>(NameValuePairT<TName, TValue> nameValuePair)
        {
            return new KeyValuePair<string, object>(nameValuePair.Name.ToString(), nameValuePair.Value);
        }

        public override string ToString()
        {
            return string.Format(TOSTRING_FORMAT, null != Name ? Name.ToString() : string.Empty, null != Value ? Value.ToString() : string.Empty);
        }

        [Key]
        public TName Name { get; set; }

        public TValue Value { get; set; }
    }
}
