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

using System.Collections.Generic;
using System.Linq;

namespace Net.Appclusive.Public.Formatter
{
    public static class Format
    {
        private const string ERROR_MESSAGE_SEPARATOR = ", ";

        public static string Message(string value, string description)
        {
            return string.Concat(value, description);
        }

        public static string Message(params string[] args)
        {
            return string.Join(ERROR_MESSAGE_SEPARATOR, args);
        }

        public static string Message(IList<string> args)
        {
            return string.Join(ERROR_MESSAGE_SEPARATOR, args.ToArray());
        }

        public static string Message(string value, params string[] args)
        {
            return string.Concat(value, ERROR_MESSAGE_SEPARATOR, string.Join(ERROR_MESSAGE_SEPARATOR, args));
        }

        public static string Message(string value, IList<string> args)
        {
            return string.Concat(value, ERROR_MESSAGE_SEPARATOR, string.Join(ERROR_MESSAGE_SEPARATOR, args.ToArray()));
        }
    }
}
