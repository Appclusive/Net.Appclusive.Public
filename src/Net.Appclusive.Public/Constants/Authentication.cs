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
    public static class Authentication
    {
        public const string ENCODING = "iso-8859-1";
        public const char BASIC_AUTHORIZATION_SEPARATOR = ':';

        public static class Type
        {
            public const string INTEGRATED = "Integrated";
            public const string INTERNAL = "Internal";
            public const string IMPERSONATED = "Impersonated";
        }

        public static class AuthenticationFilter
        {
            public const string SECTION_NAME = "authenticationManagerConfigurationSection";

            // we use lower camel case for these constants as they are used as XML attributes 
            // inside app.configconfiguration section

            // ReSharper disable InconsistentNaming
            public const bool localMachineAuthentication = false;
            public const bool activeDirectoryAuthentication = false;
            public const bool azureAuthentication = true;
            public const bool oAuth2Authentication = true;
            // ReSharper restore InconsistentNaming
        }

        public static class Header
        {
            public const string AUTHORIZATION = "Authorization";
            public const string TENANT_ID = "TenantId";
        }

        public static class Scheme
        {
            public const string BASIC = "Basic";
            public const string BEARER = "Bearer";
            public const string NEGOTIATE = "Negotiate";
        }
    }
}
