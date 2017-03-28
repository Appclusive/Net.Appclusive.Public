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
using biz.dfch.CS.Commons.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Domain.Control;
using ApprovalAttributes = Net.Appclusive.Public.Constants.Approval;

namespace Net.Appclusive.Public.Tests.Domain.Control
{
    [TestClass]
    public class ApprovalsTest
    {
        [TestMethod]
        public void ConvertFromMapSucceeds()
        {
            // Arrange
            var map = new DictionaryParameters
            {
                {ApprovalAttributes.Approval0.RoleId, 1},
                {ApprovalAttributes.Approval0.RelativeExpiration, TimeSpan.FromDays(7).Ticks},
                {ApprovalAttributes.Approval0.Type, (long) ApprovalType.AutoDecline},
                {ApprovalAttributes.Approval1.UserId, 1},
                {ApprovalAttributes.Approval1.AbsoluteExpiration, DateTimeOffset.MaxValue.Ticks}
            };

            // Act
            var result0 = new ConvertibleBaseDtoConverter().Convert<Approvals.Approval0, ApprovalAttribute>(map);
            var result1 = new ConvertibleBaseDtoConverter().Convert<Approvals.Approval1, ApprovalAttribute>(map);

            // Assert
            Assert.IsNotNull(result0);
            Assert.IsTrue(result0.IsValid(), string.Join(", ", result0.GetErrorMessages()));
            Assert.AreEqual(1, result0.RoleId);
            Assert.AreEqual(TimeSpan.FromDays(7).Ticks, result0.RelativeExpiration);
            Assert.AreEqual((long)ApprovalType.AutoDecline, result0.Type);

            Assert.IsNotNull(result1);
            Assert.IsTrue(result1.IsValid(), string.Join(", ", result1.GetErrorMessages()));
            Assert.AreEqual(1, result1.UserId);
            Assert.AreEqual(DateTimeOffset.MaxValue.Ticks, result1.AbsoluteExpiration);
            Assert.AreEqual((long)ApprovalType.AutoApprove, result1.Type);
        }
    }
}
