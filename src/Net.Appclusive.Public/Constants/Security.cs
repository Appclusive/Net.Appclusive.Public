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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Net.Appclusive.Public.Constants
{
    public static class Security
    {
        public const long SYSTEM_ROOT_ACL_ID = 1;

        public static class RoleId
        {
            public const long UBER_ADMIN = 1;
            public const long GLOBAL_EVERYONE = 2;
        }

        public static class RoleName
        {
            // ReSharper disable InconsistentNaming
            public const string UberAdmin = nameof(UberAdmin);
            public const string GlobalEveryone = nameof(GlobalEveryone);

            public const string TenantAdmin = nameof(TenantAdmin);
            public const string TenantUser = nameof(TenantUser);
            public const string TenantGuest = nameof(TenantGuest);
            public const string CreatorOwner = nameof(CreatorOwner);
            public const string Everyone = nameof(Everyone);
            public const string ParentTenant = nameof(ParentTenant);
            public const string ChildTenants = nameof(ChildTenants);
            // ReSharper restore InconsistentNaming
        }

        public static class PermissionId
        {
            public const long FULL_CONTROL = 1;
            public const long GENERIC_READ = 2;
            public const long GENERIC_WRITE = 3;
            public const long GENERIC_DELETE = 4;
        }

        public static class PermissionName
        {
            public const string FULL_CONTROL = nameof(FULL_CONTROL);
            public const string GENERIC_READ = nameof(GENERIC_READ);
            public const string GENERIC_WRITE = nameof(GENERIC_WRITE);
            public const string GENERIC_DELETE = nameof(GENERIC_DELETE);

            // ReSharper disable InconsistentNaming
            public const string ApprovalCanCreate = nameof(ApprovalCanCreate);
            public const string ApprovalCanRead = nameof(ApprovalCanRead);
            public const string ApprovalCanUpdate = nameof(ApprovalCanUpdate);
            public const string ApprovalCanDelete = nameof(ApprovalCanDelete);
            [Description("Trustee can approve or decline an Approval")]
            public const string ApprovalCanProcess = nameof(ApprovalCanProcess);
            // ReSharper restore InconsistentNaming
        }

        public static class RightId
        {
            public const long ACT_AS_PART_OF_THE_OPERATING_SYSTEM = 1;
            public const long TAKE_OWNERSHIP = 2;
            public const long GRANT_OWNERSHIP = 3;
            public const long SECURITY_MANAGEMENT = 4;
            public const long GENERIC_READ = 5;
        }

        public static class RightName
        {
            public const string ACT_AS_PART_OF_THE_OPERATING_SYSTEM = nameof(ACT_AS_PART_OF_THE_OPERATING_SYSTEM);
            public const string TAKE_OWNERSHIP = nameof(TAKE_OWNERSHIP);
            public const string GRANT_OWNERSHIP = nameof(GRANT_OWNERSHIP);
            public const string SECURITY_MANAGEMENT = nameof(SECURITY_MANAGEMENT);
            public const string GENERIC_READ = nameof(GENERIC_READ);
        }

        // ReSharper disable once InconsistentNaming
        public static readonly Lazy<ReadOnlyDictionary<string, long>> Permission =
            new Lazy<ReadOnlyDictionary<string, long>>(() =>
                new ReadOnlyDictionary<string, long>(new Dictionary<string, long>
                {
                    {PermissionName.FULL_CONTROL, PermissionId.FULL_CONTROL},
                    {PermissionName.GENERIC_READ, PermissionId.GENERIC_READ},
                    {PermissionName.GENERIC_WRITE, PermissionId.GENERIC_WRITE},
                    {PermissionName.GENERIC_DELETE, PermissionId.GENERIC_DELETE},
                }));

        public const string PERMISSION_INFIX = ":";

        public static class PermissionSuffix
        {
            public const string CREATE = "CanCreate";
            public const string READ = "CanRead";
            public const string UPDATE = "CanUpdate";
            public const string DELETE = "CanDelete";
        }

        public static class EventId
        {
            // ReSharper disable InconsistentNaming
            public const int Default = 0;

            public const int Audit = 1;
            public const int Alarm = 2;

            public const int AccessDeniedCreateItem = 4;
            public const int AccessDeniedItem = 5;

            public const int AccessDeniedCreateSecuredEntity = 6;
            public const int AccessDeniedSecuredEntity = 7;

            public const int AuditDefault = 8;
            public const int AuditAllowed = 9;
            public const int AuditDenied = 10;

            public const int AlarmDefault = 11;
            public const int AlarmAllowed = 12;
            public const int AlarmDenied = 13;

            public const int Stop = int.MaxValue - 2;
            public const int Start = int.MaxValue - 1;
            // ReSharper restore InconsistentNaming
        }
    }
}