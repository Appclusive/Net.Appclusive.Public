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

namespace Net.Appclusive.Public.Types
{
    public class NameValuePair : NameValuePairT<string, string>
    {
        public NameValuePair()
        {
            // parameterless constructor needed for LINQ to Entities
        }

        // we use key instead of name as constructor paramete rname to prevent
        // AutoMapper from using this constructor
        public NameValuePair(string key, string value)
            : base(key, value)
        {
            // N/A
        }

        public static implicit operator DictionaryEntry(NameValuePair nameValuePair)
        {
            return new DictionaryEntry(nameValuePair.Name, nameValuePair.Value);
        }

        public static implicit operator KeyValuePair<string, object>(NameValuePair nameValuePair)
        {
            return new KeyValuePair<string, object>(nameValuePair.Name, nameValuePair.Value);
        }

        public override string ToString()
        {
            return string.Format(TOSTRING_FORMAT, Name ?? string.Empty, Value ?? string.Empty);
        }
    }
}
