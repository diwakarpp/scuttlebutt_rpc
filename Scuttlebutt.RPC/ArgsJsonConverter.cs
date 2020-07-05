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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scuttlebutt.RPC
{
    public class ArgsJsonConverter : JsonConverter<RequestArgs>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            var isConvert = typeToConvert == typeof(RequestArgs);

            return isConvert;
        }

        public ArgsJsonConverter() { }

        public override RequestArgs Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            // TODO: Implement it correctly
            var obj = new HistoryStreamRequest("0");
            return obj.Read(ref reader, typeToConvert, options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RequestArgs value,
            JsonSerializerOptions options)
        {
            value.Write(writer, value, options);
        }
    }
}
