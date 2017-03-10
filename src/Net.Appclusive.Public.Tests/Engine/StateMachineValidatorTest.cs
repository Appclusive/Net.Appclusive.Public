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
using biz.dfch.CS.Commons.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Examples.Engine.V001;
using Net.Appclusive.Public.Constants;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    [TestClass]
    public class StateMachineValidatorTest
    {
        [TestMethod]
        public void StateMachineValidatorForModelBaseSucceeds()
        {
            var sut = new StateMachineValidator(typeof(BaseModel));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void StateMachineValidatorForProduct1ASucceeds()
        {
            var sut = new StateMachineValidator(typeof(Product1A));
                        
            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void StateMachineValidatorForProduct1BSucceeds()
        {
            var sut = new StateMachineValidator(typeof(Product1B));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct2A()
        {
            var sut = new StateMachineValidator(typeof(Product2A));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct2B()
        {
            var sut = new StateMachineValidator(typeof(Product2B));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct3A()
        {
            var sut = new StateMachineValidator(typeof(Product3A));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct3C()
        {
            var sut = new StateMachineValidator(typeof(Product3C));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct3DHavingABehaviourWithoutDefinitionSucceeds()
        {
            var sut = new StateMachineValidator(typeof(Product3D));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct3EWithAdditionalBehaviourAndModelBase()
        {
            var sut = new StateMachineValidator(typeof(Product3E));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct3F()
        {
            var sut = new StateMachineValidator(typeof(Product3F));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

        [TestMethod]
        public void TestingProduct4A()
        {
            var sut = new StateMachineValidator(typeof(Product4A));

            var errorMessages = sut.GetErrorMessages();
            if(errorMessages.Any())
            {
                foreach (var errorMessage in errorMessages)
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(!errorMessages.Any());
        }

    }
}
