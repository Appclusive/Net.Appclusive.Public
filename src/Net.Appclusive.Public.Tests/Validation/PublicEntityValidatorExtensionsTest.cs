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
using Net.Appclusive.Public.Domain;
using Net.Appclusive.Public.Validation;

namespace Net.Appclusive.Public.Tests.Validation
{
    [TestClass]
    public class PublicEntityValidatorExtensionsTest
    {
        [TestMethod]
        public void ValidateForPostSucceeds()
        {
            // Arrange
            var sut = new PublicEntity()
            {
                Id = default(long),
                Details = new PublicEntityDetails
                {
                    Tid = default(Guid),
                },
                Name = "arbitrary-name",
            };

            // Act
            var entity = sut.ValidateForCreate();

            // Assert
        }

        [TestMethod]
        public void ValidateForUpdatePutSucceeds()
        {
            // Arrange
            var originalEntity = new PublicEntity()
            {
                Id = 42L,
                Details = new PublicEntityDetails
                {
                Tid = Guid.NewGuid(),
                },
                Name = "arbitrary-name",
            };

            var sut = new PublicEntity()
            {
                Id = originalEntity.Id,
                Details = new PublicEntityDetails
                {
                    Tid = originalEntity.Details.Tid,
                },
                Name = "new-name",
            };

            // Act
            var entity = sut.ValidateForUpdate(originalEntity);

            // Assert
        }
    }
}
