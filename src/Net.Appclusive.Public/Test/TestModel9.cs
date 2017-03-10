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
using System.Runtime.Serialization;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Test
{
    [Serializable]
    public class TestModel9 : BaseModel, ISerializable
    {
        public TestModel9()
        {
            // N/A
        }

        public TestModel9(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // N/A
        }

        [AttributeName("tralala")]
        [Required]
        public string StringProperty { get; set; }
    }
}
