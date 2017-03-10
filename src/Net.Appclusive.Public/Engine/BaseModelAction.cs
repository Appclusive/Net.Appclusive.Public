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

namespace Net.Appclusive.Public.Engine
{
    public abstract class BaseModelAction : AttributeBaseDto, IBaseModelAction
    {
        public sealed override bool IsValid()
        {
            return base.IsValid();
        }

        public sealed override bool IsValid(string propertyName)
        {
            return base.IsValid(propertyName);
        }

        public sealed override bool IsValid(string propertyName, object value)
        {
            return base.IsValid(propertyName, value);
        }

        public sealed override IList<string> GetErrorMessages()
        {
            return base.GetErrorMessages();
        }

        public sealed override IList<string> GetErrorMessages(string propertyName)
        {
            return base.GetErrorMessages(propertyName);
        }

        public sealed override IList<string> GetErrorMessages(string propertyName, object value)
        {
            return base.GetErrorMessages(propertyName, value);
        }

        // DFTODO - why do not override we GetValidationResult() methods ?
    }
}
