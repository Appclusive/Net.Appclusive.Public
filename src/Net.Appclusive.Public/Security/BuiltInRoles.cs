

using System.Diagnostics.Contracts;
/**
 * Copyright 2015 d-fens GmbH
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
namespace Net.Appclusive.Public.Security
{
    public class BuiltInRoles
    {
        public long UberAdmin { get; set; }

        public long Everyone { get; set; }

        public long ParentTenant { get; set; }

        public long ChildTenants { get; set; }

        public long CreatorOwner { get; set; }

        public long TenantAdmin { get; set; }

        public long TenantUser { get; set; }

        public long TenantGuest { get; set; }

        public long TenantEveryone { get; set; }

        public bool IsBuiltInRole(string name)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(name));

            switch (name)
            {
                default:
                    return false;
                case Constants.Security.RoleName.UberAdmin:
                case Constants.Security.RoleName.Everyone:
                case Constants.Security.RoleName.ParentTenant:
                case Constants.Security.RoleName.ChildTenants:
                case Constants.Security.RoleName.CreatorOwner:
                case Constants.Security.RoleName.TenantAdmin:
                case Constants.Security.RoleName.TenantUser:
                case Constants.Security.RoleName.TenantGuest:
                case Constants.Security.RoleName.TenantEveryone:
                    return true;
            }
        }
    }
}
