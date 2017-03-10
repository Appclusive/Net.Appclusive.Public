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
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using BoxedType=System.Linq.Expressions.LambdaExpression;

namespace Net.Appclusive.Public.Types
{
    public sealed class BoxedLambdaExpression : Boxed<BoxedType>
    {
        private const int PARAMETER_COUNT = 1;
        private const int PARAMETER_INDEX = 0;
        private const string PARAMETER_NAME = "$it";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BoxedType Combine(BoxedLambdaExpression expression1, BoxedType expression2, bool isOrElse)
        {
            Contract.Assert(PARAMETER_COUNT == expression1?.Value.Parameters.Count);
            Contract.Assert(PARAMETER_COUNT == expression2?.Parameters.Count);
            Contract.Assert(expression1.Value.Parameters[PARAMETER_INDEX].Type == expression2.Parameters[PARAMETER_INDEX].Type);

            var combinedExpression = isOrElse
                ? Expression.OrElse(expression1.Value.Body, expression2.Body)
                : Expression.AndAlso(expression1.Value.Body, expression2.Body);
            var visitor = new MemberExpressionVisitor(Expression.Parameter(expression1.Value.Parameters[PARAMETER_INDEX].Type, PARAMETER_NAME));
            var replacedExpression = visitor.Visit(combinedExpression);

            var lambdaType = typeof(Func<,>).MakeGenericType(visitor.Parameter.Type, expression1.Value.ReturnType);
            Contract.Assert(null != lambdaType);

            var lambdaExpression = Expression.Lambda(lambdaType, replacedExpression, visitor.Parameter);
            return lambdaExpression;
        }

        public static implicit operator BoxedType(BoxedLambdaExpression lambdaExpression)
        {
            return lambdaExpression.Value;
        }

        public static implicit operator BoxedLambdaExpression(BoxedType boxedExpression)
        {
            return new BoxedLambdaExpression
            {
                Value = boxedExpression
            };
        }

        public static BoxedType operator +(BoxedLambdaExpression expression1, BoxedType expression2)
        {
            return Combine(expression1, expression2, false);
        }

        public static BoxedType operator &(BoxedLambdaExpression expression1, BoxedType expression2)
        {
            return Combine(expression1, expression2, false);
        }

        public static BoxedType operator |(BoxedLambdaExpression expression1, BoxedType expression2)
        {
            return Combine(expression1, expression2, true);
        }

        public override object ToType(Type conversionType, IFormatProvider provider)
        {
            // DFTODO - determine if and how we should implement a type conversion
            throw new NotImplementedException();
        }

        private class MemberExpressionVisitor : ExpressionVisitor
        {
            public readonly ParameterExpression Parameter;

            public MemberExpressionVisitor(ParameterExpression parameter)
            {
                Contract.Requires(null != parameter);

                Parameter = parameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                return Expression.Property
                (
                    Parameter,
                    node.Member.Name
                );
            }
        }

    }
}
