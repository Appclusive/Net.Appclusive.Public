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
using biz.dfch.CS.Commons;

namespace Net.Appclusive.Public.Domain
{
    public class PublicEntity : BaseDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(Constants.Model.BaseEntityPropertyNameMax)]
        [StringLength(Constants.Model.BaseEntityPropertyNameMax)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        public PublicEntityDetails Details { get; set; } = new PublicEntityDetails();

        public T ShallowCopy<T>()
            where T : PublicEntity 
            => MemberwiseClone() as T;

        public void SetBaseEntityDefaults()
        {
            Id = 0;
            Name = nameof(Name);
            Description = nameof(Description);
            Details.Tid = Guid.Empty;
            Details.CreatedById = 1;
            Details.ModifiedById = 1;
            Details.Created = DateTimeOffset.Now;
            Details.Modified = Details.Created;
        }
    }
}
