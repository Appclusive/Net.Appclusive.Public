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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoxedType = Net.Appclusive.Public.Types.BoxedLong;

namespace Net.Appclusive.Public.Tests.Types
{
    [TestClass]
    public class BoxedLongTest
    {
        public const long VALUE = 42L;

        [TestMethod]
        public void BoxingSucceeds()
        {
            BoxedType boxed = VALUE;

            Assert.AreEqual(VALUE, boxed.Value);
        }

        [TestMethod]
        public void UnboxingSucceeds()
        {
            var boxed = new BoxedType
            {
                Value = VALUE
            };

            long unboxed = boxed;

            Assert.AreEqual(VALUE, unboxed);
        }

        [TestMethod]
        public void ToObjectViaCastSucceeds()
        {
            var boxed = new BoxedType
            {
                Value = VALUE
            };

            object @object = (long)boxed;

            Assert.AreEqual(VALUE, @object);
        }

        [TestMethod]
        public void ChangeTypeSucceeds()
        {
            var boxed = new BoxedType
            {
                Value = VALUE
            };

            object valueAsObject = Convert.ChangeType(boxed, typeof(long));

            Assert.AreEqual(VALUE, valueAsObject);
        }
    }
}
