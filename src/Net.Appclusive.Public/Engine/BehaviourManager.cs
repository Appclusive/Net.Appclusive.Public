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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Net.Appclusive.Public.Engine
{
    public class BehaviourManager
    {
        public static Type GetBehaviourDefinitionOf(Type behaviour)
        {
            Contract.Requires(null != behaviour);
            Contract.Requires(behaviour.IsInterface);
            Contract.Requires(typeof(BaseBehaviour).IsAssignableFrom(behaviour));
            Contract.Ensures(null != Contract.Result<Type>());

            var attribute = behaviour.GetCustomAttribute(typeof(BehaviourDefinitionAttribute)) as BehaviourDefinitionAttribute;
            if (null == attribute)
            {
                return typeof(BaseModel);
            }

            var behaviourDefinition = attribute.Model;
            Contract.Assert(typeof(BaseModel).IsAssignableFrom(behaviourDefinition), behaviour.FullName);

            return behaviourDefinition;
        }

        public static Type[] GetInheritedBehavioursOf(Type model)
        {
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));
            Contract.Ensures(null != Contract.Result<Type[]>());

            var behaviours = new List<Type>();

            behaviours.AddRange
            (
                model.GetInterfaces()
                    .Where(behaviour => typeof(BaseBehaviour).IsAssignableFrom(behaviour))
            );

            return behaviours.ToArray();
        }

        public static Type[] GetBehaviourDefinitionsOf(Type model)
        {
            Contract.Requires(null != model);
            Contract.Requires(typeof(BaseModel).IsAssignableFrom(model));
            Contract.Ensures(null != Contract.Result<Type[]>());

            return GetInheritedBehavioursOf(model)
                .Select(GetBehaviourDefinitionOf)
                .Distinct()
                .ToArray();
        }
    }
}
