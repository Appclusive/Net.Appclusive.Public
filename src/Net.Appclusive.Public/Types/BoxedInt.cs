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
    public class BoxedInt : Boxed<int>
    {
        public static implicit operator BoxedInt(int value)
        {
            return new BoxedInt
            {
                Value = value
            };
        }

        public static implicit operator int(BoxedInt boxed)
        {
            return boxed.Value;
        }

        public override TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public override object ToType(Type conversionType, IFormatProvider provider)
        {
            // most commonly used types first
            if (typeof(int) == conversionType)
            {
                return ToInt32(provider);
            }
            if (typeof(string) == conversionType)
            {
                return ToString(provider);
            }

            if (typeof(short) == conversionType)
            {
                return ToInt16(provider);
            }
            if (typeof(ushort) == conversionType)
            {
                return ToUInt16(provider);
            }
            if (typeof(uint) == conversionType)
            {
                return ToUInt32(provider);
            }
            if (typeof(long) == conversionType)
            {
                return ToInt64(provider);
            }
            if (typeof(ulong) == conversionType)
            {
                return ToUInt64(provider);
            }
            if (typeof(float) == conversionType)
            {
                return ToSingle(provider);
            }
            if (typeof(double) == conversionType)
            {
                return ToDouble(provider);
            }
            if (typeof(decimal) == conversionType)
            {
                return ToDecimal(provider);
            }
            if (typeof(DateTime) == conversionType)
            {
                return ToDateTime(provider);
            }
            if (typeof(bool) == conversionType)
            {
                return ToBoolean(provider);
            }
            return Value;
        }

        public override short ToInt16(IFormatProvider provider)
        {
            return (short) Value;
        }

        public override ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort) Value;
        }

        public override int ToInt32(IFormatProvider provider)
        {
            return Value;
        }

        public override uint ToUInt32(IFormatProvider provider)
        {
            return (uint) Value;
        }

        public override long ToInt64(IFormatProvider provider)
        {
            return Value;
        }

        public override ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong) Value;
        }

        public override float ToSingle(IFormatProvider provider)
        {
            return Value;
        }

        public override double ToDouble(IFormatProvider provider)
        {
            return Value;
        }

        public override decimal ToDecimal(IFormatProvider provider)
        {
            return Value;
        }

        public override DateTime ToDateTime(IFormatProvider provider)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(Value);
        }

        public override bool ToBoolean(IFormatProvider provider)
        {
            return null != provider
                ? Convert.ToBoolean(Value, provider)
                : Convert.ToBoolean(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
