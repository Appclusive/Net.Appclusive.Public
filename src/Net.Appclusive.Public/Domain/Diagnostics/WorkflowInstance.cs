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

namespace Net.Appclusive.Public.Domain.Diagnostics
{
    public class WorkflowInstance : PublicEntity
    {
        [Required]
        public Guid WorkflowInstanceId { get; set; }

        [Required]
        public long SurrogateInstanceId { get; set; }

        public long SurrogateLockOwnerId { get; set; }

        public string PrimitiveDataProperties { get; set; }

        public string ComplexDataProperties { get; set; }

        public string WriteOnlyPrimitiveDataProperties { get; set; }

        public string WriteOnlyComplexDataProperties { get; set; }

        public string MetadataProperties { get; set; }

        public int DataEncodingOption { get; set; }

        public int MetadataEncodingOption { get; set; }

        [Required]
        public long Version { get; set; }

        public DateTimeOffset PendingTimer { get; set; }

        public Guid WorkflowHostType { get; set; }

        public long ServiceDeploymentId { get; set; }

        public string SuspensionExceptionName { get; set; }

        public string SuspensionReason { get; set; }

        public string BlockingBookmarks { get; set; }

        public string LastMachineRunOn { get; set; }

        public bool IsInitialized { get; set; }

        public bool IsSuspended { get; set; }

        public bool IsReadyToRun { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public long SurrogateIdentityId { get; set; }
    }
}
