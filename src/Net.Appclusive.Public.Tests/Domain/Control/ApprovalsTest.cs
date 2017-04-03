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
using System.Diagnostics.Contracts;
using biz.dfch.CS.Commons.Linq;

namespace Net.Appclusive.Public.Tests.Domain.Control
{
    [TestClass]
    public class ApprovalsTest
    {
        private const string SEPARATOR = ", ";

        private readonly DictionaryParameters map = new DictionaryParameters
        {
            {ApprovalAttributes.Approval0.RoleId, 1},
            {ApprovalAttributes.Approval0.RelativeExpiration, TimeSpan.FromDays(7).Ticks},
            {ApprovalAttributes.Approval0.Type, (long) ApprovalType.AutoDecline},

            {ApprovalAttributes.Approval1.UserId, 1},
            {ApprovalAttributes.Approval1.AbsoluteExpiration, DateTimeOffset.MaxValue.Ticks},

            {"Net.Appclusive.Arbitrary.AttributeName", 42L },
            {"Org.Sharedop.Arbitrary.AttributeName", "tralala" },
        };

        [TestMethod]
        public void ConvertFromMap0Succeeds()
        {
            // Arrange

            // Act
            var result = new ConvertibleBaseDtoConverter().Convert<Approvals.Approval0, ApprovalAttribute>(map);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid(), string.Join(SEPARATOR, result.GetErrorMessages()));
            Assert.AreEqual(1, result.RoleId);
            Assert.AreEqual(TimeSpan.FromDays(7).Ticks, result.RelativeExpiration);
            Assert.AreEqual((long)ApprovalType.AutoDecline, result.Type);
        }

        [TestMethod]
        public void ConvertFromMap1Succeeds()
        {
            // Arrange

            // Act
            var result = new ConvertibleBaseDtoConverter().Convert<Approvals.Approval1, ApprovalAttribute>(map);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid(), string.Join(SEPARATOR, result.GetErrorMessages()));
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual(DateTimeOffset.MaxValue.Ticks, result.AbsoluteExpiration);
            Assert.AreEqual((long)ApprovalType.AutoApprove, result.Type);
        }

        [TestMethod]
        public void MakeGenericMethodSucceeds()
        {
            // Act
            var converter = new ConvertibleBaseDtoConverter();
            var genericMethodInfo = converter.GetType().GetMethod(nameof(ConvertibleBaseDtoConverter.Convert), new [] {typeof(DictionaryParameters)});
            Contract.Assert(null != genericMethodInfo, nameof(ConvertibleBaseDtoConverter.Convert));

            var methodInfo = genericMethodInfo.MakeGenericMethod(typeof(Approvals.Approval0), typeof(ApprovalAttribute));
            var resultAsApprovalBase = methodInfo.Invoke(converter, new object[] { map }) as Approvals.ApprovalBase;

            // Assert
            Assert.IsNotNull(resultAsApprovalBase);
            Assert.IsTrue(resultAsApprovalBase.IsValid(), string.Join(SEPARATOR, resultAsApprovalBase.GetErrorMessages()));
            Assert.IsInstanceOfType(resultAsApprovalBase, typeof(Approvals.Approval0));

            var result = resultAsApprovalBase as Approvals.Approval0;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid(), string.Join(SEPARATOR, result.GetErrorMessages()));
            Assert.AreEqual(1, result.RoleId);
            Assert.AreEqual(TimeSpan.FromDays(7).Ticks, result.RelativeExpiration);
            Assert.AreEqual((long)ApprovalType.AutoDecline, result.Type);
        }

        [TestMethod]
        public void GettingErrorMessagesFromOverriddenMethodSucceeds()
        {
            var converter = new ConvertibleBaseDtoConverter();
            var genericMethodInfo = converter.GetType().GetMethod(nameof(ConvertibleBaseDtoConverter.Convert), new[] { typeof(DictionaryParameters) });
            Contract.Assert(null != genericMethodInfo, nameof(ConvertibleBaseDtoConverter.Convert));

            var emptyMap = new DictionaryParameters();

            var methodInfo = genericMethodInfo.MakeGenericMethod(typeof(Approvals.Approval0), typeof(ApprovalAttribute));
            var resultAsApprovalBase = methodInfo.Invoke(converter, new object[] { emptyMap }) as Approvals.ApprovalBase;

            // Assert
            Assert.IsNotNull(resultAsApprovalBase);
            Assert.IsFalse(resultAsApprovalBase.IsValid());
            Assert.IsInstanceOfType(resultAsApprovalBase, typeof(Approvals.Approval0));

            resultAsApprovalBase.GetErrorMessages().ForEach(e => System.Diagnostics.Trace.WriteLine(e));
        }
    }
}
