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

using System.Linq;
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    [TestClass]
    public class ConnectionToAttributeTest
    {
        public interface IArbitraryBehaviour1 : BaseBehaviour
        {
            // N/A
        }
            
        public interface IArbitraryBehaviour2 : BaseBehaviour
        {
            // N/A
        }
            
        public interface IAmNotAValidBehaviour
        {
            // N/A
        }

        [ConnectionTo(typeof(IArbitraryBehaviour1))]
        [ConnectionTo(typeof(IArbitraryBehaviour1), typeof(IArbitraryBehaviour2))]
        public class ValidArbitraryModelWithConnectionToAttributes : BaseModel
        {
            public string ArbitraryStringProperty { get; set; }
        }

        [ConnectionTo(typeof(IArbitraryBehaviour1), typeof(IArbitraryBehaviour2))]
        [ConnectionTo(typeof(IAmNotAValidBehaviour))]
        public class InvalidArbitraryModelWithConnectionToAttributes : BaseModel
        {
            public string ArbitraryStringProperty { get; set; }
        }

        [TestMethod]
        public void ValidArbitraryModelWithConnectionToAttributesSucceeds()
        {
            var sut = new ValidArbitraryModelWithConnectionToAttributes
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionToAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionToAttribute), true);
            Assert.IsTrue(attributes.Any());

            foreach (var connectionConstraintForAttribute in attributes)
            {
                var behaviours = connectionConstraintForAttribute.Behaviours;
                Assert.IsNotNull(behaviours);
                Assert.IsTrue(behaviours.Any());
            }
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Invariant.+Behaviours.All.+BaseBehaviour.+behaviour.+IsInterface")]
        public void InvalidArbitraryModelWithConnectionToAttributesThrowsContractException()
        {
            var sut = new InvalidArbitraryModelWithConnectionToAttributes
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionToAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionToAttribute), true);
            Assert.IsTrue(attributes.Any());

            foreach (var connectionConstraintForAttribute in attributes)
            {
                var behaviours = connectionConstraintForAttribute.Behaviours;
                Assert.IsNotNull(behaviours);
                Assert.IsTrue(behaviours.Any());
            }
        }
    }
}
