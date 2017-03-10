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

namespace Net.Appclusive.Public.Types
{
    public class IdValuePair
    {
        public IdValuePair()
        {
            // parameterless constructor needed for LINQ to Entities
        }

        // we use string key instead of string name to prevent
        // AutoMapper from using this constructor
        public IdValuePair(long key, string value)
        {
            Id = key;
            Value = value;
        }

        public long Id { get; set; }

        public string Value { get; set; }
    }
}
