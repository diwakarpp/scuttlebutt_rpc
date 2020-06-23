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

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scuttlebutt.RPC
{
    public enum ProcedureType
    {
        Source,
        Async
    }

    public class RPC
    {
        [JsonPropertyName("name")]
        public List<string> Name { get; set; }
        [JsonPropertyName("type")]
        public ProcedureType Type { get; set; }
        [JsonPropertyName("args")]
        public List<RequestArgs> Args { get; set; }

        public RPC()
        {
            this.Name = new List<string>();
            this.Args = new List<RequestArgs>();
        }

        public RPC(List<string> name, ProcedureType type, List<RequestArgs> args)
        {
            this.Name = name;
            this.Type = type;
            this.Args = args;
        }

        public string Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            options.Converters.Add(
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            );
            options.Converters.Add(
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            );

            return JsonSerializer.Serialize(this, options);
        }

        public static RPC CreateHistoryStream(string id)
        {
            var args = new List<RequestArgs> { new HistoryStreamRequest(id) };
            var name = new List<string> { "createHistoryStream" };

            var rpc = new RPC(
                name, ProcedureType.Source, args
            );

            return rpc;
        }
    }
}
