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

using System.ComponentModel.DataAnnotations;
using biz.dfch.CS.Commons;
using Net.Appclusive.Public.Engine;
using System.Diagnostics.Contracts;

namespace Net.Appclusive.Public.Executor
{
    public class ModelInformation : BaseDto, IModelInformation
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string BehaviourDefinitionFor { get; set; }

        [Required]
        public string[] Behaviours { get; set; }

        [Required]
        public string Parent { get; set; }

        [Required]
        public string SerialisedStateMachine { get; set; }

        [Required]
        public string SerialisedConfiguration { get; set; }

        [Required]
        public string[] ActionNames { get; set; }

        [Required]
        public string[] Actions { get; set; }

        public string[] Attributes { get; set; }

        // additional properties

        [Required]
        public StateMachine StateMachine { get; set; }

        [Required]
        public DictionaryParameters Configuration { get; set; }

        public ModelInformation()
        {
            // N/A
        }

        public ModelInformation(string serialisedStateMachine, string serialisedConfiguration)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(serialisedStateMachine));
            Contract.Requires(!string.IsNullOrWhiteSpace(serialisedConfiguration));

            StateMachine = StateMachine.Create(serialisedStateMachine);
            SerialisedStateMachine = serialisedStateMachine;
            Configuration = new DictionaryParameters(serialisedConfiguration);
            SerialisedConfiguration = serialisedConfiguration;
        }
    }
}
