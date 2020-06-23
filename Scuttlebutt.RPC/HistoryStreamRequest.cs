// Copyright © 2020 Pedro Gómez Martín <zentauro@riseup.net>
//
// This file is part of the library Scuttlebutt.RPC which
// is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this library. If not, see <http://www.gnu.org/licenses/>.

using System.Text.Json.Serialization;

namespace Scuttlebutt.RPC
{
    public class HistoryStreamRequest : RequestArgs
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("seq")]
        public string Seq { get; set; }
        [JsonPropertyName("limit")]
        public string Limit { get; set; }
        [JsonPropertyName("live")]
        public string Live { get; set; }
        [JsonPropertyName("old")]
        public string Old { get; set; }
        [JsonPropertyName("keys")]
        public string Keys { get; set; }

        public HistoryStreamRequest(string id)
        {
            this.Id = id;
        }
    }
}