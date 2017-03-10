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
    public abstract class Boxed : IConvertible
    {
        public virtual TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public virtual bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual string ToString(IFormatProvider provider)
        {
            return null != provider
                ? string.Format(provider, base.ToString())
                : base.ToString();
        }

        public abstract object ToType(Type conversionType, IFormatProvider provider);
    }
}
