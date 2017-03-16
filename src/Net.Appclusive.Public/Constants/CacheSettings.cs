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
    public static class CacheSettings
    {
        public const string SECTION_NAME = "cacheManagerSettingsConfiguration";

        public const long DEFAULT_TIMEOUT = 60;

        // we use lower camel case for these constants as they are used as XML attributes 
        // inside app.configconfiguration section

        // ReSharper disable InconsistentNaming
        public const long defaultCacheManagerSettingsTimeout = DEFAULT_TIMEOUT;
        public const long modelCacheManagerSettingsTimeout = DEFAULT_TIMEOUT;
        public const long behaviourCacheManagerSettingsTimeout = DEFAULT_TIMEOUT;
        public const long tenantCacheManagerSettingsTimeout = DEFAULT_TIMEOUT;
        public const long roleCacheManagerSettingsTimeout = 5 * DEFAULT_TIMEOUT;
        public const long rightCacheManagerSettingsTimeout = 5 * DEFAULT_TIMEOUT;
        public const long workflowCacheManagerSettingsTimeout = 30 * 60;
        // ReSharper restore InconsistentNaming
    }
}
