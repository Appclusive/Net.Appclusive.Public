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
    public static class Authentication
    {
        public const string ENCODING = "iso-8859-1";
        public const char BASIC_AUTHORIZATION_SEPARATOR = ':';
        public const string AZURE_JWT_VALIDATOR_CONFIG_SECTION_NAME = "azureJwtValidationConfiguration";

        public static class Type
        {
            public const string BASIC = "Basic";
            public const string BEARER = "Bearer";
            public const string NEGOTIATE = "Negotiate";
            public const string INTERNAL = "Internal";
            public const string IMPERSONATED = "Impersonated";
        }

        public static class AuthenticationFilter
        {
            public const string SECTION_NAME = "authenticationManagerConfiguration";

            // we use lower camel case for these constants as they are used as XML attributes 
            // inside app.configconfiguration section

            // ReSharper disable InconsistentNaming
            public const bool basicAuthentication = true;
            public const bool negotiateAuthentication = true;
            public const bool bearerAuthentication = true;
            public const bool systemUserAuthentication = false;
            public const bool testAuthentication = false;
            // ReSharper restore InconsistentNaming
        }

        public static class Header
        {
            public const string AUTHORIZATION = "Authorization";
            public const string TENANT_ID = "TenantId";
        }

        public static class JwtKey
        {
            public const string UPN = "upn";
            public const string EXPIRATION = "exp";
            public const string ISSUER = "iss";
        }
    }
}
