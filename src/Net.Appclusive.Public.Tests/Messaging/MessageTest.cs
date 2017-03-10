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
using Net.Appclusive.Public.Engine;
using Net.Appclusive.Public.Messaging;

namespace Net.Appclusive.Public.Tests.Messaging
{
    [TestClass]
    public class MessageTest
    {
        private const string HEADER_TYPE = "ArbitraryType";
        private const string HEADER_VERSION = "1.0";

        [TestMethod]
        public void CreateNewMessageSetsHeaderAndBodyType()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var body = CreateBody();
            var message = new Public.Messaging.Message(header, body);

            Assert.AreEqual(typeof(DefaultMessageHeader).FullName, message.HeaderType);
            Assert.AreEqual(typeof(BuildModelMessageBody).FullName, message.BodyType);
        }

        [TestMethod]
        public void SerializeAndDeserializeMessageSucceeds()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var body = CreateBody();
            var message = new Public.Messaging.Message(header, body);

            var serializedMsg = message.SerializeObject();

            var deserializedMsg = BaseDto.DeserializeObject<Public.Messaging.Message>(serializedMsg);

            var deserializedHeader = (DefaultMessageHeader)deserializedMsg.Header;
            var deserializedBody = (BuildModelMessageBody)deserializedMsg.Body;

            Assert.AreEqual(message.BodyType, deserializedMsg.BodyType);
            Assert.AreEqual(message.HeaderType, deserializedMsg.HeaderType);

            Assert.AreEqual(header.Type, deserializedHeader.Type);
            Assert.AreEqual(header.Version, deserializedHeader.Version);
            Assert.AreEqual(body.ParentItemId, deserializedBody.ParentItemId);
            Assert.AreEqual(body.ModelName, deserializedBody.ModelName);
            Assert.AreEqual(body.Configuration.Count, deserializedBody.Configuration.Count);
        }

        [TestMethod]
        public void IsValidForMessageWithNullBodyReturnsFalse()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var message = new Public.Messaging.Message();
            message.Header = header;
            message.HeaderType = typeof(DefaultMessageHeader).FullName;

            Assert.IsFalse(message.IsValid());
        }

        [TestMethod]
        public void IsValidForMessageWithNullHeaderReturnsFalse()
        {
            var body = CreateBody();
            var message = new Public.Messaging.Message();
            message.BodyType = typeof(BuildModelMessageBody).FullName;
            message.Body = body;

            Assert.IsFalse(message.IsValid());
        }

        [TestMethod]
        public void IsValidForValidMessageReturnsTrue()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var body = CreateBody();
            var message = new Public.Messaging.Message(header, body);

            Assert.IsTrue(message.IsValid());
        }

        [TestMethod]
        public void GetValidationResultsForValidMessageReturnsEmptyResult()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var body = CreateBody();
            var message = new Public.Messaging.Message(header, body);

            Assert.AreEqual(0, message.GetValidationResults().Count);
        }

        [TestMethod]
        public void GetValidationResultsForMessageWithoutBodyReturnsValidationResult()
        {
            var header = new DefaultMessageHeader(HEADER_TYPE, HEADER_VERSION);
            var message = new Public.Messaging.Message();
            message.Header = header;
            message.HeaderType = typeof(DefaultMessageHeader).FullName;

            Assert.AreEqual(2, message.GetValidationResults().Count);
        }

        [TestMethod]
        public void GetValidationResultsForMessageWithoutHeaderReturnsValidationResult()
        {
            var body = CreateBody();
            var message = new Public.Messaging.Message();
            message.BodyType = typeof(BuildModelMessageBody).FullName;
            message.Body = body;

            Assert.AreEqual(2, message.GetValidationResults().Count);
        }

        [TestMethod]
        public void GetValidationResultsForMessageWithoutHeaderAndBodyReturnsValidationResults()
        {
            var message = new Public.Messaging.Message();

            Assert.AreEqual(4, message.GetValidationResults().Count);
        }

        [TestMethod]
        public void CreatingMessageReturnsNonEmptyGuid()
        {
            // Act
            var message = new Public.Messaging.Message();

            // Assert
            Assert.AreNotEqual(default(Guid), message.Id);
        }

        [TestMethod]
        [ExpectContractFailure]
        public void CreatingMessageWithEmptyGuidThrowsContractException()
        {
            var message = new Public.Messaging.Message();

            // Act
            message.Id = default(Guid);
        }

        private BuildModelMessageBody CreateBody()
        {
            return new BuildModelMessageBody()
            {
                ParentItemId = 42L,
                ModelName = typeof(BaseModel).FullName,
                Configuration = new DictionaryParameters()
            };
        }
    }
}
