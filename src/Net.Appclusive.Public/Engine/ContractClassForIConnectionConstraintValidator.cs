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
using System.Diagnostics.Contracts;
using biz.dfch.CS.Commons;

namespace Net.Appclusive.Public.Engine
{
    [ContractClassFor(typeof(IConnectionConstraintValidator))]
    public abstract class ContractClassForIConnectionConstraintValidator : IConnectionConstraintValidator
    {
        // DFTODO - is this really correct ? BaseBehaviour ? should not it be IModelBase ?
        public bool IsValid(BaseModel itemToBeValidated, BaseModel[] existingChildren, Type modelToBeAdded, DictionaryParameters modelParameters)
        {
            Contract.Requires(null != itemToBeValidated);
            Contract.Requires(null != existingChildren);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(modelToBeAdded));
            Contract.Requires(null != modelParameters);

            return default(bool);
        }

        // DFTODO - is this really correct ? BaseBehaviour ? should not it be IModelBase ?
        public string[] GetErrorMessages(BaseModel itemToBeValidated, BaseModel[] existingChildren, Type modelToBeAdded, DictionaryParameters modelParameters)
        {
            Contract.Requires(null != itemToBeValidated);
            Contract.Requires(null != existingChildren);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(modelToBeAdded));
            Contract.Requires(null != modelParameters);
            Contract.Ensures(null != Contract.Result<string[]>());

            return default(string[]);
        }
    }
}
