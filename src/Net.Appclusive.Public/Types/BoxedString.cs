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

namespace Net.Appclusive.Public.Types
{
    public class BoxedString : Boxed<string>
    {
        public static implicit operator BoxedString(string value)
        {
            return new BoxedString
            {
                Value = value
            };
        }

        public static implicit operator string(BoxedString boxed)
        {
            return boxed.Value;
        }

        public static explicit operator BoxedString(Enum @enum)
        {
            return new BoxedString
            {
                Value = @enum.ToString()
            };
        }

        public override TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        public override object ToType(Type conversionType, IFormatProvider provider)
        {
            return Value;
        }

        public override bool ToBoolean(IFormatProvider provider)
        {
            return null != provider
                ? Convert.ToBoolean(Value, provider)
                : Convert.ToBoolean(Value);
        }
        
        public override string ToString(IFormatProvider provider)
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
