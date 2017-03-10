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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using biz.dfch.CS.Commons;
using Newtonsoft.Json;

namespace Net.Appclusive.Public.Messaging
{
    public class Message : BaseDto
    {
        public Guid Id { get; set; } = System.Diagnostics.Trace.CorrelationManager.ActivityId == Guid.Empty ? Guid.NewGuid() : System.Diagnostics.Trace.CorrelationManager.ActivityId;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(default(Guid) != Id);
        }

        [Required]
        public string HeaderType { get; set; }

        private object header;
        [Required]
        public object Header
        {
            get
            {
                if (null == HeaderType || null == Type.GetType(HeaderType))
                {
                    return header;
                }
                var serializedHeader = JsonConvert.SerializeObject(header);
                return DeserializeObject(serializedHeader, Type.GetType(HeaderType));
            }
            set
            {
                header = value;
            }
        }

        [Required]
        public string BodyType { get; set; }

        private object body;
        [Required]
        public object Body
        {
            get
            {
                if (null == BodyType || null == Type.GetType(BodyType))
                {
                    return body;
                }
                var serializedBody = JsonConvert.SerializeObject(body);
                return DeserializeObject(serializedBody, Type.GetType(BodyType));
            }
            set
            {
                body = value;
            }
        }

        public Message()
        {
            // N/A
        }

        public Message(object header, object body)
        {
            Contract.Requires(null != header);
            Contract.Requires(null != body);

            Header = header;
            HeaderType = header.GetType().FullName;
            Body = body;
            BodyType = body.GetType().FullName;
        }
    }
}