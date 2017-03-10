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
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Types;

namespace Net.Appclusive.Public.Tests.Types
{
    [TestClass]
    public class BoxedLambdaExpressionTest
    {
        public class SomeObject
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }

        // ReSharper disable once InconsistentNaming
        private static readonly Expression<Func<SomeObject, bool>> VALUE = e => e.Id != 0;

        [TestMethod]
        public void BoxingSucceeds()
        {
            BoxedLambdaExpression boxedLambda = VALUE;

            Assert.AreEqual(VALUE, boxedLambda.Value);
        }

        [TestMethod]
        public void UnboxingSucceeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression unboxed = boxed;

            Assert.AreEqual(VALUE, unboxed);
        }

        [TestMethod]
        public void ImplicitCast1Succeeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression expression = boxed;

            Assert.AreEqual(VALUE, expression);
        }

        [TestMethod]
        public void ImplicitCast2Succeeds()
        {
            var boxed = VALUE;

            Expression expression = boxed;

            Assert.AreEqual(VALUE, expression);
        }

        [TestMethod]
        public void OperatorAddSucceeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed + expression;

            var someObject = new SomeObject
            {
                Id = 42,
                Name = "tralala"
            };

            var value = result.Compile().DynamicInvoke(someObject);

            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void OperatorAndSucceeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed & expression;

            var someObject = new SomeObject
            {
                Id = 0,
                Name = "tralala"
            };

            var value = result.Compile().DynamicInvoke(someObject);

            Assert.AreEqual(false, value);
        }

        [TestMethod]
        public void OperatorOrSucceeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed | expression;

            var someObject = new SomeObject
            {
                Id = 0,
                Name = string.Empty
            };

            var value = result.Compile().DynamicInvoke(someObject);

            Assert.AreEqual(false, value);
        }

        [TestMethod]
        public void ToString1Succeeds()
        {
            var expected = @"$it => (($it.Id != 0) AndAlso Not(IsNullOrWhiteSpace($it.Name)))";
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed + expression;

            var value = result.ToString();
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void ToString2Succeeds()
        {
            var expected = @"$it => (($it.Id != 0) AndAlso Not(IsNullOrWhiteSpace($it.Name)))";
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed & expression;

            var value = result.ToString();
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void ToString3Succeeds()
        {
            var expected = @"$it => (($it.Id != 0) OrElse Not(IsNullOrWhiteSpace($it.Name)))";
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            Expression<Func<SomeObject, bool>> expression = e => !string.IsNullOrWhiteSpace(e.Name);
            var result = boxed | expression;

            var value = result.ToString();
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ChangeTypeSucceeds()
        {
            var boxed = new BoxedLambdaExpression
            {
                Value = VALUE
            };

            object valueAsObject = Convert.ChangeType(boxed, typeof(bool));
        }
    }
}
