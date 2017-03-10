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

using biz.dfch.CS.Commons;
using Net.Appclusive.Public.Domain;

namespace Net.Appclusive.Public.Validation
{
    public static class PublicEntityValidatorExtensions
    {
        public static TPublicEntity ValidateForCreate<TPublicEntity>(this TPublicEntity entityToBeCreated)
            where TPublicEntity : PublicEntity
        {
            return new PublicEntityValidator<TPublicEntity>().ForCreate(entityToBeCreated);
        }

        public static T ValidateForUpdate<T>(this T modifiedEntity, T originalEntity)
            where T : PublicEntity
        {
            return new PublicEntityValidator<T>().ForUpdate(modifiedEntity, originalEntity);
        }

        public static T ValidateForUpdate<T>(this T entitytoBeUpdated, DictionaryParameters delta)
            where T : PublicEntity
        {
            return new PublicEntityValidator<T>().ForUpdate(entitytoBeUpdated, delta);
        }

        public static T ValidateForDelete<T>(this T entityToBeDeleted)
            where T : PublicEntity
        {
            return new PublicEntityValidator<T>().ForDelete(entityToBeDeleted);
        }
    }
}
