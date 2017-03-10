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

namespace Net.Appclusive.Public.Constants
{
    public static class Identity
    {
        public static class User
        {
            public const long SYSTEM_USER_ID = 1;
            public const string SYSTEM_USER_NAME = "SYSTEM";
        }

        public static class Tenant
        {
            public const string DEFAULT = "00000000-0000-0000-0000-000000000000";
            public const string SYSTEM = "11111111-1111-1111-1111-111111111111";
            public const string HOME = "22222222-2222-2222-2222-222222222222";
            public const string GROUP = "33333333-3333-3333-3333-333333333333";
            public const string SHARED = "44444444-4444-4444-4444-444444444444";
            public const string PARENT = "55555555-5555-5555-5555-555555555555";
            public const string CHILD = "66666666-6666-6666-6666-666666666666";

            // ReSharper disable InconsistentNaming
            public static readonly Guid DEFAULT_TID = default(Guid);
            public static readonly Guid SYSTEM_TID = new Guid(SYSTEM);
            public static readonly Guid HOME_TID = new Guid(HOME);
            public static readonly Guid GROUP_TID = new Guid(GROUP);
            public static readonly Guid SHARED_TID = new Guid(SHARED);
            public static readonly Guid PARENT_TID = new Guid(PARENT);
            public static readonly Guid CHILD_TID = new Guid(CHILD);
            // ReSharper restore InconsistentNaming
        }
    }
}
