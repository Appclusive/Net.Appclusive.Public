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

using System.Runtime.Serialization;
using biz.dfch.CS.Commons;
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    public interface IArbitraryBehaviour1 : BaseBehaviour
    {
        // N/A
    }
        
    public interface IArbitraryBehaviour2 : BaseBehaviour
    {
        // N/A
    }
        
    public interface IArbitraryBehaviourThatDoesNotInheritIBehaviour
    {
        // N/A
    }
        
    public class ThisIsAClassAndNotAModelBase : BaseBehaviour
    {
        public StateMachine GetStateMachine()
        {
            throw new System.NotImplementedException();
        }

        public DictionaryParameters GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
        
    [ConnectionConstraintFor(typeof(ThisIsAClassAndNotAModelBase), MaxOccurs = 42)]
    public class InvalidConnectionConstraintAttributeTestClass1 : BaseModel
    {
        public string ArbitraryStringProperty { get; set; }
    }

    [ConnectionConstraintFor(typeof(IArbitraryBehaviourThatDoesNotInheritIBehaviour), MaxOccurs = 42)]
    public class InvalidConnectionConstraintAttributeTestClass2 : BaseModel
    {
        public string ArbitraryStringProperty { get; set; }
    }

    [ConnectionConstraintFor(typeof(IArbitraryBehaviour1), MaxOccurs = 5)]
    [ConnectionConstraintFor(typeof(IArbitraryBehaviour2), MaxOccurs = 42)]
    public class ValidConnectionConstraintAttributeTestClass : BaseModel
    {
        public string ArbitraryStringProperty { get; set; }
    }
        
    [TestClass]
    public class ConnectionConstraintForAttributeTest
    {
        [TestMethod]
        public void TestWithMultipleConstraintsSucceeds()
        {
            var sut = new ValidConnectionConstraintAttributeTestClass
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionConstraintForAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionConstraintForAttribute), true);
            Assert.IsTrue(0 < attributes.Length);

            foreach (var connectionConstraintForAttribute in attributes)
            {
                var behvaiour = connectionConstraintForAttribute.Behaviour;
            }
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Invariant.+Behaviour.IsInterface")]
        public void TestWithInvalidConstraintClassThrowsContractException()
        {
            var sut = new InvalidConnectionConstraintAttributeTestClass1
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionConstraintForAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionConstraintForAttribute), true);
            Assert.IsTrue(0 < attributes.Length);

            foreach (var connectionConstraintForAttribute in attributes)
            {
                var behvaiour = connectionConstraintForAttribute.Behaviour;
            }
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Invariant.+IsAssignableFrom.Behaviour")]
        public void TestWithInvalidConstraintInterfaceThrowsContractException()
        {
            var sut = new InvalidConnectionConstraintAttributeTestClass2
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionConstraintForAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionConstraintForAttribute), true);
            Assert.IsTrue(0 < attributes.Length);

            foreach (var connectionConstraintForAttribute in attributes)
            {
                var behvaiour = connectionConstraintForAttribute.Behaviour;
            }
        }
    }
}
