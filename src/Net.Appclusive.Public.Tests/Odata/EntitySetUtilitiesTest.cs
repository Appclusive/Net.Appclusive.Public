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

using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Domain.Configuration;
using Net.Appclusive.Public.Odata;

namespace Net.Appclusive.Public.Tests.Odata
{
    [TestClass]
    public class EntitySetUtilitiesTest
    {
        private const string KNV_ENTITY_SET_NAME = "KeyNameValues";

        [ExpectContractFailure(MessagePattern = "Precondition.+type")]
        [TestMethod]
        public void ResolveEntitySetNameWithNullTypeThrowsContractException()
        {
            // Arrange

            // Act
            EntitySetUtilities.ResolveEntitySetName(null);

            // Assert
        }

        [TestMethod]
        public void ResolveEntitySetNameForKeyNameValueReturnsKeyNameValues()
        {
            // Arrange

            // Act
            var resolverResult = EntitySetUtilities.ResolveEntitySetName(typeof(KeyNameValue));

            // Assert
            Assert.AreEqual(KNV_ENTITY_SET_NAME, resolverResult);
        }

        [TestMethod]
        public void ResolveGenericForKeyNameValueReturnsKeyNameValues()
        {
            // Arrange

            // Act
            var resolverResult = EntitySetUtilities.ResolveEntitySetName<KeyNameValue>();

            // Assert
            Assert.AreEqual(KNV_ENTITY_SET_NAME, resolverResult);
        }
    }
}
