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

namespace Net.Appclusive.Public.Constants
{
    public static class Workflow
    {
        public const string WORKFLOW_MANAGER_CONFIGURATION_SECTION_NAME = "workflowManagerConfiguration";

        public const string WORKFLOW_ASSEMBLY_NAMESPACE_PREFIX = "Net.Appclusive.Workflows";

        public const string BUILD_MODEL_ACTIVITY = "BuildModel";

        public const string BUILD_MODEL_PARENT_ITEM_ID_IN_ARGUMENT = "ParentItemId";
        public const string BUILD_MODEL_MODEL_NAME_IN_ARGUMENT = "ModelName";
        public const string BUILD_MODEL_CONFIGURATION_IN_ARGUMENT = "Configuration";
    }
}