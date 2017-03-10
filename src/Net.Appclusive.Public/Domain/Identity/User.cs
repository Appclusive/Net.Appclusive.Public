﻿/**
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
using Net.Appclusive.Public.Domain.Security;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Domain.Identity
{
    public class User : PublicEntity
    {
        [Required]
        public string MappedId { get; set; }

        [Required]
        public string MappedType { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        public bool IsHidden { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
