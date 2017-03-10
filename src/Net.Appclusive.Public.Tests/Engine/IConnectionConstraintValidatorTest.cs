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
using System.Reflection;
using biz.dfch.CS.Commons;
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    public interface IArbitraryBehaviour3 : BaseBehaviour
    {
        // N/A
    }

    public class ArbitraryModel : BaseModel, IArbitraryBehaviour3
    {
        // N/A
    }

    public class ArbitraryModelThatDoesNotInheritFromIBehaviour
    {
        // N/A
    }
        
    public class ConnectionConstraintValidatorClass : IConnectionConstraintValidator
    {
        public bool IsValid(BaseModel itemToBeValidated, BaseModel[] existingChildren, Type modelToBeAdded, DictionaryParameters modelParameters)
        {
            return true;
        }

        public string[] GetErrorMessages(BaseModel itemToBeValidated, BaseModel[] existingChildren, Type modelToBeAdded, DictionaryParameters modelParameters)
        {
            return new string[] { };
        }
    }
        
    [ConnectionConstraintFor(typeof(IArbitraryBehaviour3), MaxOccurs = 42, Validator = typeof(ConnectionConstraintValidatorClass))]
    public class ValidConnectionConstraintAttributeTestClassWithValidator : BaseModel
    {
        public string ArbitraryStringProperty { get; set; }
    }

    [ConnectionConstraintFor(typeof(IArbitraryBehaviour3), MaxOccurs = 42, Validator = typeof(IArbitraryBehaviour3))]
    public class InvalidConnectionConstraintAttributeTestClassWithValidator : BaseModel
    {
        public string ArbitraryStringProperty { get; set; }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class IConnectionConstraintValidatorTest
    {
        [TestMethod]
        public void ConstraintWithValidatorSucceeds()
        {
            var sut = new ValidConnectionConstraintAttributeTestClassWithValidator
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionConstraintForAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionConstraintForAttribute), true);
            Assert.IsTrue(1 == attributes.Length);

            var connectionConstraintForAttribute = attributes[0];
            var validatorType = connectionConstraintForAttribute.Validator;

            var validator = Activator.CreateInstance(validatorType) as IConnectionConstraintValidator;
            Assert.IsNotNull(validator);

            var result = validator.IsValid(new ArbitraryModel(), new BaseModel[] { new ArbitraryModel() }, typeof(ArbitraryModel), new DictionaryParameters());
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectException(typeof(CustomAttributeFormatException), "Validator")]
        public void ConstraintWithValidatorThrowsCustomAttributeFormatException()
        {
            var sut = new InvalidConnectionConstraintAttributeTestClassWithValidator
            {
                ArbitraryStringProperty = Resource.ArbitraryStringWithDash
            };

            var attributes = (ConnectionConstraintForAttribute[]) sut.GetType()
                .GetCustomAttributes(typeof(ConnectionConstraintForAttribute), true);
            Assert.IsTrue(1 == attributes.Length);

            var connectionConstraintForAttribute = attributes[0];
            var validatorType = connectionConstraintForAttribute.Validator;
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+BaseModel.+IsAssignableFrom.+modelToBeAdded")]
        public void ValidatorIsValidWithNullModelToBeAddedThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.IsValid(new ArbitraryModel(), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModelThatDoesNotInheritFromIBehaviour), new DictionaryParameters());
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+existingChildren")]
        public void ValidatorIsValidWithNullChildrenThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.IsValid(new ArbitraryModel(), default(BaseModel[]), typeof(ArbitraryModel), new DictionaryParameters());
        }
        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itemToBeValidated")]
        public void ValidatorIsValidWithNullItemToBeValidatedThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.IsValid(default(BaseModel), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModel), new DictionaryParameters());
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+modelParameters")]
        public void ValidatorIsValidWithNulModelParametersThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.IsValid(new ArbitraryModel(), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModel), default(DictionaryParameters));
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+BaseModel.+IsAssignableFrom.+modelToBeAdded")]
        public void ValidatorGetErrorMessagesWithNullModelToBeAddedThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.GetErrorMessages(new ArbitraryModel(), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModelThatDoesNotInheritFromIBehaviour), new DictionaryParameters());
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+existingChildren")]
        public void ValidatorGetErrorMessagesWithNullChildrenThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.GetErrorMessages(new ArbitraryModel(), default(BaseModel[]), typeof(ArbitraryModel), new DictionaryParameters());
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itemToBeValidated")]
        public void ValidatorGetErrorMessagesWithNullItemToBeValidatedThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.GetErrorMessages(default(BaseModel), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModel), new DictionaryParameters());
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+modelParameters")]
        public void ValidatorGetErrorMessagesWithNullModelParametersThrowsContractException()
        {
            var sut = new ConnectionConstraintValidatorClass();
            sut.GetErrorMessages(new ArbitraryModel(), new BaseModel[] {new ArbitraryModel()}, typeof(ArbitraryModel), default(DictionaryParameters));
        }
    }
}
