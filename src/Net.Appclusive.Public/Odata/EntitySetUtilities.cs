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
using System.Diagnostics.Contracts;
using Net.Appclusive.Public.Domain;

namespace Net.Appclusive.Public.Odata
{
    public class EntitySetUtilities
    {
        public const char ENTITY_SET_SUFFIX = 's';

        public const string MANAGER_SUFFIX = "Manager";

        public const string DATA_MANAGER_SUFFIX = "DataManager";

        public const string CONTROLLER_SUFFIX = "Controller";

        public static string ResolveEntitySetName<T>()
            where T : PublicEntity
        {
            return ResolveEntitySetName(typeof(T));
        }

        public static string ResolveEntitySetName(Type type)
        {
            Contract.Requires(null != type);

            return string.Concat(type.Name, ENTITY_SET_SUFFIX);
        }

        public static string ResolveManagerName<T>()
        {
            return ResolveManagerName(typeof(T));
        }

        public static string ResolveManagerName(Type type)
        {
            Contract.Requires(null != type);

            return string.Concat(type.Name, MANAGER_SUFFIX);
        }

        public static string ResolveDataManagerName<T>()
        {
            return ResolveDataManagerName(typeof(T));
        }

        public static string ResolveDataManagerName(Type type)
        {
            Contract.Requires(null != type);

            return string.Concat(type.Name, DATA_MANAGER_SUFFIX);
        }

        public static string ResolveControllerName<T>()
        {
            return ResolveControllerName(typeof(T));
        }

        public static string ResolveControllerName(Type type)
        {
            Contract.Requires(null != type);

            return string.Concat(ResolveEntitySetName(type), CONTROLLER_SUFFIX);
        }
    }
}
