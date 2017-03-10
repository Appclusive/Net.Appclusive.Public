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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Net.Appclusive.Public.Engine
{
    public static class StateMachineExtensions
    {
        public static bool IsValid<T>(this StateMachine stateMachine)
            where T : BaseModel
        {
            Contract.Requires(null != stateMachine);

            return new StateMachineValidator(typeof(T)).IsValid();
        }

        public static IList<ValidationResult> GetValidationResults<T>(this StateMachine stateMachine)
            where T : BaseModel
        {
            Contract.Requires(null != stateMachine);

            return new StateMachineValidator(typeof(T)).GetValidationResults();
        }

        public static IList<string> GetErrorMessages<T>(this StateMachine stateMachine)
            where T : BaseModel
        {
            Contract.Requires(null != stateMachine);

            return new StateMachineValidator(typeof(T)).GetErrorMessages();
        }
    }
}
