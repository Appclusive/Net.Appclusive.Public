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
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace Net.Appclusive.Public.Engine
{
    public class StateMachineTransition : IEnumerable
    {
        internal static string GetNodeTypeStateFieldName<TValue>(Expression<Func<TValue>> nodeTypeStateExpression)
        {
            Contract.Requires(null != nodeTypeStateExpression);
            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

            var expressionBody = nodeTypeStateExpression.Body as MemberExpression;
            Contract.Assert(null != expressionBody);
            
            var member = expressionBody.Member;
            Contract.Assert(MemberTypes.Field == member.MemberType);
            Contract.Assert(null != member.DeclaringType);
            Contract.Assert(typeof(BaseModelStates).IsAssignableFrom(member.DeclaringType.BaseType) || typeof(BaseModelStates).IsAssignableFrom(member.DeclaringType));

            return member.Name;
        }

        internal StateMachineTransition(string source, string action, string destination)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(source));
            Contract.Requires(!string.IsNullOrWhiteSpace(action));
            Contract.Requires(!string.IsNullOrWhiteSpace(destination));

            var frame = new StackTrace().GetFrame(1);
            Contract.Assert(null != frame);
            var methodBase = frame.GetMethod();
            Contract.Assert(null != methodBase);
            var declaringType = methodBase.DeclaringType;
            Contract.Assert(null != declaringType);
            Contract.Assert(declaringType.IsAssignableFrom(typeof(StateMachine)));
            
            Source = source;
            Action = action;
            Destination = destination;
        }

        public StateMachineTransition(Expression<Func<ModelState>> source, Type action, Expression<Func<ModelState>> destination)
        {
            Add(source, action, destination);
        }

        public void Add(Expression<Func<ModelState>> source, Type action, Expression<Func<ModelState>> destination)
        {
            Contract.Requires(null != source);
            Contract.Requires(null != action);
            Contract.Requires(action.IsNestedPublic);
            Contract.Requires(action.BaseType == typeof(BaseModelAction));
            Contract.Requires(null != destination);

            Source = GetNodeTypeStateFieldName(source);
            Action = action.Name;
            Destination = GetNodeTypeStateFieldName(destination);
        }

        private StateMachineTransition()
        {
            // we must not use the default parameterless constructor, 
            // as we would violate the ConctractInvariant method, 
            // when used inside object initialisers
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(Source));
            Contract.Invariant(!string.IsNullOrWhiteSpace(Action));
            Contract.Invariant(!string.IsNullOrWhiteSpace(Destination));
        }

        public string Source { get; internal set; }

        public string Action { get; internal set; }

        public string Destination { get; internal set; }

        public IEnumerator GetEnumerator()
        {
            yield return Source;
            yield return Action;
            yield return Destination;
        }
    }
}
