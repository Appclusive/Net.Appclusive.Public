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

using System;
using System.Collections.Generic;
using Net.Appclusive.Public.Security;

namespace Net.Appclusive.Public.Domain.Identity
{
    public class TenantInformation
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public long CustomerId { get; set; }

        public BuiltInRoles BuiltInRoles { get; set; }

        public long ItemId { get; set; }

        public long AclId { get; set; }

        public Dictionary<string, long> CreatorOwnerRightMap { get; set; }
    }
}
