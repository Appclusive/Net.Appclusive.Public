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

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Domain.Catalogue
{
    public class CatalogueItem : PublicEntity
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long CatalogueId { get; set; }

        [JsonIgnore]
        public virtual Catalogue Catalogue { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long BlueprintId { get; set; }

        [JsonIgnore]
        public virtual Blueprint Blueprint { get; set; }
    }
}
