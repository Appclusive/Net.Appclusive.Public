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
using biz.dfch.CS.Commons;
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Domain;
using Net.Appclusive.Public.Validation;

namespace Net.Appclusive.Public.Tests.Validation
{
    [TestClass]
    public class PublicEntityValidatorTest
    {
        private readonly byte[] rowVersion = { 0x42 };
        private const long ENTITY_ID = 42;

        private PublicEntityValidator<PublicEntity> Sut { get; } = new PublicEntityValidator<PublicEntity>();

        [ExpectContractFailure(MessagePattern = "Precondition.+entityToBeCreated")]
        [TestMethod]
        public void ForCreateWithNullEntityToBeCreatedThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForCreate(null);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+entityToBeCreated\.Id")]
        [TestMethod]
        public void ForCreateWithEntityToBeCreatedHavingNonZeroIdThrowsContractException()
        {
            // Arrange
            var entity = new PublicEntity()
            {
                Id = ENTITY_ID
            };

            // Act
            Sut.ForCreate(entity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+entityToBeCreated\.Details\.Tid")]
        [TestMethod]
        public void ForCreateWithEntityToBeCreatedHavingNonDefaultGuidThrowsContractException()
        {
            // Arrange
            var entity = new PublicEntity()
            {
                Details = new PublicEntityDetails
                {
                    Tid = Guid.NewGuid()
                }
            };

            // Act
            Sut.ForCreate(entity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+entityToBeCreated.\Details\.RowVersion")]
        [TestMethod]
        public void ForCreateWithEntityToBeCreatedHavingNonDefaultRowVersionThrowsContractException()
        {
            // Arrange
            var entity = new PublicEntity()
            {
                Details = new PublicEntityDetails
                {
                    Tid = default(Guid),
                    RowVersion = rowVersion
                }
            };

            // Act
            Sut.ForCreate(entity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+entityToBeCreated\.IsValid")]
        [TestMethod]
        public void ForCreateWithInvalidEntityToBeCreatedThrowsContractException()
        {
            // Arrange
            var entity = new PublicEntity()
            {
                Details = new PublicEntityDetails
                {
                    Tid = default(Guid)
                }
            };

            // Act
            Sut.ForCreate(entity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = "Precondition.+modifiedEntity")]
        [TestMethod]
        public void ForUpdateWithNullModifiedEntityThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForUpdate(null, new PublicEntity());

            // Assert
        }

        [ExpectContractFailure(MessagePattern = "Precondition.+originalEntity")]
        [TestMethod]
        public void ForUpdateWithNullOriginalEntityThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForUpdate(new PublicEntity(), (PublicEntity)null);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+modifiedEntity\.Id.+originalEntity\.Id")]
        [TestMethod]
        public void ForUpdateWithNonMatchingIdsThrowsContractException()
        {
            // Arrange
            var modifiedEntity = new PublicEntity()
            {
                Id = ENTITY_ID
            };

            var originalEntity = new PublicEntity()
            {
                Id = ENTITY_ID + 1
            };

            // Act
            Sut.ForUpdate(modifiedEntity, originalEntity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+modifiedEntity.\Details\.Tid.+originalEntity\.Details\.Tid")]
        [TestMethod]
        public void ForUpdateWithNonMatchingTidsThrowsContractException()
        {
            // Arrange
            var modifiedEntity = new PublicEntity()
            {
                Id = ENTITY_ID
                ,
                Details = new PublicEntityDetails
                {
                    Tid = Guid.NewGuid()
                }
            };

            var originalEntity = new PublicEntity()
            {
                Id = ENTITY_ID
                ,
                Details = new PublicEntityDetails
                {
                    Tid = Guid.NewGuid()
                }
                ,
            };

            // Act
            Sut.ForUpdate(modifiedEntity, originalEntity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+modifiedEntity\.IsValid")]
        [TestMethod]
        public void ForUpdateWithInvalidModifiedEntityThrowsContractException()
        {
            // Arrange
            var modifiedEntity = new PublicEntity()
            {
                Id = ENTITY_ID
            };

            var originalEntity = new PublicEntity()
            {
                Id = ENTITY_ID
            };

            // Act
            Sut.ForUpdate(modifiedEntity, originalEntity);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = "Precondition.+entityToBeUpdated")]
        [TestMethod]
        public void ForUpdateWithNullEntityToBeUpdatedThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForUpdate(null, new DictionaryParameters());

            // Assert
        }

        [ExpectContractFailure(MessagePattern = "Precondition.+delta")]
        [TestMethod]
        public void ForUpdateWithNullDeltaThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForUpdate(new PublicEntity(), (DictionaryParameters)null);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+delta\.ContainsKey.+PublicEntity\.Id")]
        [TestMethod]
        public void ForUpdateWithDeltaContainingIdThrowsContractException()
        {
            // Arrange
            var delta = new DictionaryParameters()
            {
                { nameof(PublicEntity.Id), ENTITY_ID }
            };

            // Act
            Sut.ForUpdate(new PublicEntity(), delta);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+delta\.ContainsKey.+PublicEntityDetails\.Tid")]
        [TestMethod]
        public void ForUpdateWithDeltaContainingTidThrowsContractException()
        {
            // Arrange
            var delta = new DictionaryParameters()
            {
                { nameof(PublicEntityDetails.Tid), Guid.NewGuid() }
            };

            // Act
            Sut.ForUpdate(new PublicEntity(), delta);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+delta\.ContainsKey.+PublicEntityDetails\.CreatedById")]
        [TestMethod]
        public void ForUpdateWithDeltaContainingCreatedByIdThrowsContractException()
        {
            // Arrange
            var delta = new DictionaryParameters()
            {
                { nameof(PublicEntityDetails.CreatedById), Int64.MaxValue }
            };

            // Act
            Sut.ForUpdate(new PublicEntity(), delta);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+delta\.ContainsKey.+PublicEntityDetails\.Created")]
        [TestMethod]
        public void ForUpdateWithDeltaContainingCreatedDateThrowsContractException()
        {
            // Arrange
            var delta = new DictionaryParameters()
            {
                { nameof(PublicEntityDetails.Created), DateTimeOffset.Now }
            };

            // Act
            Sut.ForUpdate(new PublicEntity(), delta);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+delta\.ContainsKey.+PublicEntityDetails\.RowVersion")]
        [TestMethod]
        public void ForUpdateWithDeltaContainingRowVersionThrowsContractException()
        {
            // Arrange
            var delta = new DictionaryParameters()
            {
                { nameof(PublicEntityDetails.RowVersion), rowVersion }
            };

            // Act
            Sut.ForUpdate(new PublicEntity(), delta);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = "Precondition.+entityToBeDeleted")]
        [TestMethod]
        public void ForDeleteWithNullEntityToBeDeletedThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForDelete(null);

            // Assert
        }

        [ExpectContractFailure(MessagePattern = @"Precondition.+entityToBeDeleted\.Id")]
        [TestMethod]
        public void ForDeleteWithEntityToBeDeletedHavingZeroIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.ForDelete(new PublicEntity());

            // Assert
        }
    }
}
