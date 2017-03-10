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

using System.Diagnostics;
using System.Linq;
using biz.dfch.CS.Commons.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Examples.Geometry.V001;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    public class TestValidation : DataTypeValidationBaseAttribute
    {

    }

    [TestClass]
    public class ModelValidatorTest
    {
        [TestMethod]
        public void SquareStateMachineIsValid()
        {
            // Arrange
            var sut = new Square();

            // Act
            var stateMachine = sut.GetStateMachine();

            // Assert
            var errorMessages = stateMachine.GetErrorMessages();
            errorMessages.ToList().ForEach(e => Trace.WriteLine(e));
            Assert.IsFalse(errorMessages.Any());
        }

        [TestMethod]
        public void SquareModelIsValid()
        {
            // Arrange
            var sut = new ModelValidator(typeof(Square));

            // Act
            var errorMessages = sut.GetErrorMessages();

            // Assert
            errorMessages.ToList().ForEach(e => Trace.WriteLine(e));
            Assert.IsFalse(errorMessages.Any());
        }


        [TestMethod]
        public void SquareIsValid()
        {
            // Arrange
            var sut = new Square();

            // Act
            var errorMessages = sut.GetErrorMessages();

            // Assert
            errorMessages.ToList().ForEach(e => Trace.WriteLine(e));
            Assert.IsFalse(errorMessages.Any());
        }

        [TestMethod]
        public void SquareIsInvalid()
        {
            // Arrange
            var sut = new Square
            {
                LocationName = "invalid-location"
            };

            // Act
            var errorMessages = sut.GetErrorMessages();

            // Assert
            errorMessages.ToList().ForEach(e => Trace.WriteLine(e));
            Assert.IsTrue(errorMessages.Any());
        }
    }
}
