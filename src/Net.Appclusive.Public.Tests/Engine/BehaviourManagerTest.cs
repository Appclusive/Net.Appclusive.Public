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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Examples.Engine.V001;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    [TestClass]
    public class BehaviourManagerTest
    {
        [TestMethod]
        public void ResolvingBehavioursOnlyReturnsInterfacesThatImplementIBehaviour()
        {
            var behaviours = BehaviourManager.GetInheritedBehavioursOf(typeof(Product3C));

            Assert.AreEqual(4, behaviours.Length);
            Assert.IsTrue(behaviours.Contains(typeof(BaseBehaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product1Behaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product2Behaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product3Behaviour)));
        }

        [TestMethod]
        public void ResolvingBehavioursSucceeds()
        {
            var behaviours = BehaviourManager.GetInheritedBehavioursOf(typeof(Product3D));

            Assert.AreEqual(5, behaviours.Length);
            Assert.IsTrue(behaviours.Contains(typeof(BaseBehaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product1Behaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product2Behaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(Product3Behaviour)));
            Assert.IsTrue(behaviours.Contains(typeof(IDotNotHaveABehaviourDefinition)));
        }

        [TestMethod]
        public void GettingBehaviourDefinitionsSucceeds()
        {
            var behaviourDefinitions = BehaviourManager.GetBehaviourDefinitionsOf(typeof(Product3D));

            Assert.AreEqual(4, behaviourDefinitions.Length);
            Assert.IsTrue(behaviourDefinitions.Contains(typeof(BaseModel)));
            Assert.IsTrue(behaviourDefinitions.Contains(typeof(Product1BehaviourDefinition)));
            Assert.IsTrue(behaviourDefinitions.Contains(typeof(Product2BehaviourDefinition)));
            Assert.IsTrue(behaviourDefinitions.Contains(typeof(Product3BehaviourDefinition)));
        }
    }
}
