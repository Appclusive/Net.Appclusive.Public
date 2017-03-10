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

namespace Net.Appclusive.Public.Constants
{
    public static class ModelExecutor
    {
        public const string SECTION_NAME = "modelExecutorConfiguration";

        public const string VALUE_SEPARATOR = "|";

        public const string ASSEMBLY_FILTER_WILDCARD = "*.dll";

        public const char ASSEMBLY_NAME_DELIMITER = '.';

        public const string SECURITY_PERMISSION_FLAGS = "Execution|SerializationFormatter";

        public const string SHARED_TENANT = Identity.Tenant.SHARED;

        public const string TRUSTED_ASSEMBLY_TYPES = "biz.dfch.CS.Commons.BaseDto|Net.Appclusive.Public.Engine.BaseModel|Net.Appclusive.Executor.Security.ModelExecutor";

        public const string ROOT_ASSEMBLY = "Net.Appclusive.Public";

        public const string ASSEMBLY_PREFIXES = "com" + VALUE_SEPARATOR + "net" + VALUE_SEPARATOR  + "org" + VALUE_SEPARATOR + "ch" + VALUE_SEPARATOR + "biz";
    }
}
