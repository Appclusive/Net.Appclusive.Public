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
using System.Collections.Concurrent;

namespace Net.Appclusive.Public.Messaging
{
    public class MessageQueue : IMessagingClient
    {
        private static readonly ConcurrentQueue<Message> _concurrentQueue = new ConcurrentQueue<Message>();

        public void SendMessage(string facility, Message message)
        {
            _concurrentQueue.Enqueue(message);
        }

        public Message ReceiveMessage(string facility, int waitTimeoutMs)
        {
            Message message = default(Message);

            var startTime = DateTime.UtcNow;
            do
            {
                if (_concurrentQueue.TryDequeue(out message))
                {
                    return message;
                }
            } while (DateTime.UtcNow - startTime < TimeSpan.FromMilliseconds(waitTimeoutMs));

            return message;
        }

        public Message PeekMessage(string facility, int waitTimeoutMs)
        {
            Message message = default(Message);

            var startTime = DateTime.UtcNow;
            do
            {
                if (_concurrentQueue.TryPeek(out message))
                {
                    return message;
                }
            } while (DateTime.UtcNow - startTime < TimeSpan.FromMilliseconds(waitTimeoutMs));

            return message;
        }
    }
}
