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
using Net.Appclusive.Public.Domain;

namespace Net.Appclusive.Public.Validation
{
    [ContractClassFor(typeof(IPublicEntityValidator<>))]
    public abstract class ContractClassForIPublicEntityValidator<T> : IPublicEntityValidator<T>
        where T : PublicEntity
    {
        public T ForCreate(T entityToBeCreated)
        {
            Contract.Requires(null != entityToBeCreated);
            Contract.Requires(0 == entityToBeCreated.Id);
            Contract.Requires(default(Guid) == entityToBeCreated.Details.Tid);
            Contract.Requires(default(byte[]) == entityToBeCreated.Details.RowVersion);
            Contract.Requires(entityToBeCreated.IsValid());
            Contract.Ensures(null != Contract.Result<T>());

            return default(T);
        }

        public T ForUpdate(T modifiedEntity, T originalEntity)
        {
            Contract.Requires(null != modifiedEntity);
            Contract.Requires(null != originalEntity);
            Contract.Requires(modifiedEntity.Id == originalEntity.Id);
            Contract.Requires(modifiedEntity.Details.Tid == originalEntity.Details.Tid);
            Contract.Requires(modifiedEntity.Details.CreatedById == originalEntity.Details.CreatedById);
            Contract.Requires(modifiedEntity.Details.ModifiedById == originalEntity.Details.ModifiedById);
            Contract.Requires(modifiedEntity.Details.Created == originalEntity.Details.Created);
            Contract.Requires(modifiedEntity.Details.Modified == originalEntity.Details.Modified);
            Contract.Requires(modifiedEntity.IsValid());
            Contract.Ensures(null != Contract.Result<T>());

            return default(T);
        }

        public T ForUpdate(T entityToBeUpdated, DictionaryParameters delta)
        {
            Contract.Requires(null != entityToBeUpdated);
            Contract.Requires(null != delta);
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntity.Id)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.Tid)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.CreatedById)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.ModifiedById)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.Created)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.Modified)));
            Contract.Requires(!delta.ContainsKey(nameof(PublicEntityDetails.RowVersion)));
            Contract.Ensures(null != Contract.Result<T>());

            return default(T);
        }

        public T ForDelete(T entityToBeDeleted)
        {
            Contract.Requires(null != entityToBeDeleted);
            Contract.Requires(0 < entityToBeDeleted.Id);
            Contract.Ensures(null != Contract.Result<T>());

            return default(T);
        }
    }
}
