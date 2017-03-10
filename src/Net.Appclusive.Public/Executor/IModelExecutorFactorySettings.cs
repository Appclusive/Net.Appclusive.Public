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
using System.Security;
using System.Security.Policy;

namespace Net.Appclusive.Public.Executor
{
    public interface IModelExecutorFactorySettings
    {
        string BaseDirectory { get; set; }

        string AssemblyFilterWildcard { get; set; }

        char AssemblyNameDelimiter { get; set; }

        Guid SharedTenant { get; set; }

        IPermission[] Permissions { get; set; }

        StrongName[] TrustedAssemblies { get; set; }

        string RootAssembly { get; set; }

        string[] AssemblyNamePrefixes { get; set; }
    }
}
