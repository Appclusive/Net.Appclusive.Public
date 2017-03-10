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
using System.ComponentModel.DataAnnotations;
using biz.dfch.CS.Commons;
using Net.Appclusive.Public.Constants;

namespace Net.Appclusive.Public.Domain.Identity
{
    public class Tenant : BaseDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(Model.BaseEntityPropertyNameMax)]
        [StringLength(Model.BaseEntityPropertyNameMax)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual TenantDetails Details { get; set; } = new TenantDetails();

        [Required]
        public string MappedId { get; set; }

        [Required]
        public string MappedType { get; set; }

        [Required]
        public Guid ParentId { get; set; }

        public virtual Tenant Parent { get; set; }

        [Required]
        public string Namespace { get; set; }

        public long CustomerId { get; set; }

        public virtual ICollection<Tenant> Children { get; set; }
    }
}
