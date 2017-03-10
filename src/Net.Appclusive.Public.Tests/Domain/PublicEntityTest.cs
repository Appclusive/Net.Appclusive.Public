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

namespace Net.Appclusive.Public.Tests.Domain
{
    [TestClass]
    public class PublicEntityTest
    {
        [TestMethod]
        public void SetBaseEntityDefaultsSetsDefaultValuesOnBaseEntity()
        {
            // Arrange
            var baseEntity = new PublicEntity();
            Assert.AreEqual(0, baseEntity.Id);
            Assert.AreEqual(Guid.Empty, baseEntity.Details.Tid);
            Assert.AreEqual(null, baseEntity.Name);
            Assert.AreEqual(null, baseEntity.Description);
            Assert.AreEqual(0, baseEntity.Details.CreatedById);
            Assert.AreEqual(0, baseEntity.Details.ModifiedById);
            Assert.AreEqual(DateTimeOffset.MinValue, baseEntity.Details.Created);
            Assert.AreEqual(DateTimeOffset.MinValue, baseEntity.Details.Modified);

            // Act
            baseEntity.SetBaseEntityDefaults();

            // Assert
            Assert.AreEqual(0, baseEntity.Id);
            Assert.AreEqual(Guid.Empty, baseEntity.Details.Tid);
            Assert.AreEqual(nameof(PublicEntity.Name), baseEntity.Name);
            Assert.AreEqual(nameof(PublicEntity.Description), baseEntity.Description);
            Assert.AreEqual(1, baseEntity.Details.CreatedById);
            Assert.AreEqual(1, baseEntity.Details.ModifiedById);
            Assert.AreEqual(DateTimeOffset.Now.Year, baseEntity.Details.Created.Year);
            Assert.AreEqual(DateTimeOffset.Now.Month, baseEntity.Details.Created.Month);
            Assert.AreEqual(DateTimeOffset.Now.Day, baseEntity.Details.Created.Day);
            Assert.AreEqual(DateTimeOffset.Now.Hour, baseEntity.Details.Created.Hour);
            Assert.AreEqual(DateTimeOffset.Now.Minute, baseEntity.Details.Created.Minute);
            Assert.AreEqual(DateTimeOffset.Now.Year, baseEntity.Details.Modified.Year);
            Assert.AreEqual(DateTimeOffset.Now.Month, baseEntity.Details.Modified.Month);
            Assert.AreEqual(DateTimeOffset.Now.Day, baseEntity.Details.Modified.Day);
            Assert.AreEqual(DateTimeOffset.Now.Hour, baseEntity.Details.Modified.Hour);
            Assert.AreEqual(DateTimeOffset.Now.Minute, baseEntity.Details.Modified.Minute);
        }
    }
}
