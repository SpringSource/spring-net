﻿#region License

/*
 * Copyright © 2010-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using System.Linq;

namespace Spring.Context.Attributes.TypeFilters
{

    /// <summary>
    /// A simple filter which matches classes that are assignable to a given type.
    /// </summary>
    public class AssignableTypeFilter : AbstractLoadTypeFilter
    {

        /// <summary>
        /// Create a Type Filter with required type
        /// </summary>
        /// <param name="expression">type name including assembly name</param>
        public AssignableTypeFilter(string expression)
        {
            GetRequiredType(expression);
        }

        /// <summary>
        /// Determine a match based on the given type object.
        /// </summary>
        /// <param name="type">Type to compare against</param>
        /// <returns>true if there is a match; false is there is no match</returns>
        public override bool Match(Type type)
        {
            if (RequiredType == null)
                return false;

            return (type.GetInterfaces().Any(i => i.Equals(RequiredType)) || RequiredType.Equals(type.BaseType));
        }

    }
}
