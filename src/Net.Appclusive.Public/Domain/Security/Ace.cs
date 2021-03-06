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

using System.ComponentModel.DataAnnotations;
using Net.Appclusive.Public.Domain.Identity;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Domain.Security
{
    public class Ace : PublicEntity
    {
        [Required]
        public long AclId { get; set; }

        [Range(1, long.MaxValue)]
        public long PermissionId { get; set; }

        [JsonIgnore]
        public virtual Permission Permission { get; set; }

        [Range(1, long.MaxValue)]
        public long? UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [Range(1, long.MaxValue)]
        public long? RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
