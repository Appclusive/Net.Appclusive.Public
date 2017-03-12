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
    public static class Logging
    {
        public enum EventId : int
        {
            Default = 0,

            Stop = int.MaxValue -2,
            Start = int.MaxValue -1,

            Exception = int.MaxValue
        }

        public enum CacheManagerEventId : int
        {
            Default = 0,

            IntrinsicModelRetrieval,

            Stop = int.MaxValue - 2,
            Start = int.MaxValue - 1,

            Exception = int.MaxValue
        }

        public enum ProductEngineEventId : int
        {
            Default = 0,

            StateMachineRetrieval,

            Stop = int.MaxValue -2,
            Start = int.MaxValue -1,

            Exception = int.MaxValue
        }
            
        public enum ODataEventId : int
        {
            Default = 0,
            InvalidState,

            Stop = int.MaxValue - 2,
            Start = int.MaxValue - 1,

            Exception = int.MaxValue
        }

        // DFTODO - create extension method for Logger with Type parameter to pass typeof actual class
        // DFTODO - based on passed type (FullName?) the trace source name will be looked up in a dictionary. Dictionary could be initialized via a configuration section
        public static class TraceSourceName
        {
            public const string APPCLUSIVE_CORE = "Net.Appclusive.Core";
            public const string CACHE_MANAGER = "Net.Appclusive.Core.Cache.CacheManager";
            public const string WORKFLOW_ENGINE = "Net.Appclusive.Internal.Workflow";
            public const string ENGINE = "Net.Appclusive.Public.Engine";
            public const string WEB_API = "Net.Appclusive.WebApi";
            public const string ACCESS_MANAGER = "Net.Appclusive.Core.Security.AccessManager";
        }
    }
}
