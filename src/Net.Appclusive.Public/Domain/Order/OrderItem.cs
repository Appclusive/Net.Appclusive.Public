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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Net.Appclusive.Public.Domain.Control;
using Net.Appclusive.Public.Types;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Domain.Catalogue
{
    public class OrderItem : PublicEntity
    {
        [Required]
        public long OrderId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [Required]
        public long JobId { get; set; }

        public virtual Job Job { get; set; }

        [Required]
        public long BlueprintId { get; set; }

        public virtual ICollection<IdValuePair> Configuration { get; set; }
    }
}
