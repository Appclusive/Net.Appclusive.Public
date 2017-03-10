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

namespace Net.Appclusive.Public.Domain.Security
{
    public enum AceType
    {
        //
        // DO NOT USE 0 as AceTypeEnum value !!
        // 

        // make sure you ALWAYS have MIN set correctly to the smallest/first enum value
        Min = Audit
        ,
        Audit = 1
        ,
        Alarm
        ,
        // make sure DENY is SMALLER than ALLOW as we evaluate ACEs by the ordered values of AceType
        Deny
        ,
        Allow
        ,
        Ingress
        ,
        Egress
        ,
        // make sure you ALWAYS have MAX set correctly to the largest/last enum value
        Max = Egress
    }
}
