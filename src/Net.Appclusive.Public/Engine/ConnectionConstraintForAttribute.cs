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
using System.Diagnostics.Contracts;

namespace Net.Appclusive.Public.Engine
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class ConnectionConstraintForAttribute : Attribute
    {
        public Type Behaviour { get; }

        public int MaxOccurs { get; set; } = int.MaxValue;

        public Type Validator { get; set; }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(typeof(BaseBehaviour).IsAssignableFrom(Behaviour));
            Contract.Invariant(Behaviour.IsInterface);
            Contract.Invariant(0 < MaxOccurs);
            Contract.Invariant(null == Validator || typeof(IConnectionConstraintValidator).IsAssignableFrom(Validator));
        }

        public ConnectionConstraintForAttribute(Type behaviour)
        {
            Behaviour = behaviour;
        }
    }
}
