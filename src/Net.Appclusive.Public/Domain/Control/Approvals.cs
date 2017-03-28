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
using System.ComponentModel.DataAnnotations;
using biz.dfch.CS.Commons.Converters;

namespace Net.Appclusive.Public.Domain.Control
{
    public class Approvals
    {
        public abstract class ApprovalBase : ConvertibleBaseDto
        {
            [Approval(Constants.Approval.Approval0.RoleId)]
            [Range(0, long.MaxValue)]
            public virtual long RoleId { get; set; }

            [Approval(Constants.Approval.Approval0.UserId)]
            [Range(0, long.MaxValue)]
            public virtual long UserId { get; set; }

            [Approval(Constants.Approval.Approval0.Type)]
            public virtual long Type { get; set; }

            [Approval(Constants.Approval.Approval0.AbsoluteExpiration)]
            public virtual long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval0.RelativeExpiration)]
            public virtual long RelativeExpiration { get; set; }
        }

        public sealed class Approval0 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval0.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval0.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval0.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval0.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval0.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval1 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval1.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval1.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval1.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval1.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval1.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval2 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval2.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval2.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval2.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval2.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval2.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval3 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval3.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval3.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval3.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval3.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval3.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval4 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval4.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval4.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval4.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval4.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval4.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval5 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval5.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval5.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval5.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval5.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval5.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval6 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval6.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval6.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval6.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval6.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval6.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval7 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval7.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval7.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval7.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval7.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval7.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval8 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval8.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval8.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval8.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval8.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval8.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class Approval9 : ApprovalBase
        {
            [Approval(Constants.Approval.Approval9.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.Approval9.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.Approval9.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.Approval9.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.Approval9.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalA : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalA.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalA.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalA.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalA.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalA.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalB : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalB.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalB.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalB.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalB.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalB.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalC : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalC.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalC.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalC.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalC.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalC.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalD : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalD.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalD.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalD.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalD.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalD.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalE : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalE.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalE.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalE.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalE.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalE.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }

        public sealed class ApprovalF : ApprovalBase
        {
            [Approval(Constants.Approval.ApprovalF.RoleId)]
            [Range(0, long.MaxValue)]
            public override long RoleId { get; set; }

            [Approval(Constants.Approval.ApprovalF.UserId)]
            [Range(0, long.MaxValue)]
            public override long UserId { get; set; }

            [Approval(Constants.Approval.ApprovalF.Type)]
            public override long Type { get; set; }

            [Approval(Constants.Approval.ApprovalF.AbsoluteExpiration)]
            public override long AbsoluteExpiration { get; set; }

            [Approval(Constants.Approval.ApprovalF.RelativeExpiration)]
            public override long RelativeExpiration { get; set; }
        }
    }
}
