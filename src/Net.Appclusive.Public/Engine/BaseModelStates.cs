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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection;

// ReSharper disable UnassignedReadonlyField

namespace Net.Appclusive.Public.Engine
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    // this class defines the required states for an 
    // Appclusive NodeTypeBase state machine
    public abstract class BaseModelStates
    {
        protected BaseModelStates()
        {
            // ensure that our derived class only contains
            // fields of type NodeTypeState

            var fieldInfos = GetType().GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public);
            Contract.Assert(null != fieldInfos);
            
            foreach (var fieldInfo in fieldInfos)
            {
                Contract.Assert(typeof(ModelState) == fieldInfo.FieldType);
            }
        }

        public static readonly ModelState InitialState;
        public static readonly ModelState DecommissionedState;
        public static readonly ModelState FinalState;
        public static readonly ModelState ErrorState;
    }
}
