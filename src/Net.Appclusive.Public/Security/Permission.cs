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

namespace Net.Appclusive.Public.Security
{
    public static class Permission<TDataEntity>
        where TDataEntity : class
    {
        private static readonly Lazy<string> _create = new Lazy<string>(() => string.Concat(typeof(TDataEntity).Name, Constants.Security.PermissionSuffix.CREATE));
        public static string Create => _create.Value;

        private static readonly Lazy<string> _read = new Lazy<string>(() => string.Concat(typeof(TDataEntity).Name, Constants.Security.PermissionSuffix.READ));
        public static string Read => _read.Value;

        private static readonly Lazy<string> _update = new Lazy<string>(() => string.Concat(typeof(TDataEntity).Name, Constants.Security.PermissionSuffix.UPDATE));
        public static string Update => _update.Value;

        private static readonly Lazy<string> _delete = new Lazy<string>(() => string.Concat(typeof(TDataEntity).Name, Constants.Security.PermissionSuffix.DELETE));
        public static string Delete => _delete.Value;
    }
}
