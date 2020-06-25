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
            return typeToConvert.IsSubclassOf(typeof(RequestArgs));
        }

        public ArgsJsonConverter(JsonSerializerOptions options)
        {

        }

        public override RequestArgs Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {

        }

        public override void Write(
            Utf8JsonWriter writer,
            RequestArgs value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var kvp in value.GetType().GetProperties())
            {
                var prop = value.GetType().GetProperty(kvp.Name).GetValue(value);
                if (prop == null)
                {
                    continue;
                }

                if (value.GetType().GetProperty(kvp.Name).GetType() == typeof(bool))
                {
                    writer.WriteBoolean(kvp.Name, (bool)prop);
                }
                else
                {
                    writer.WriteString(kvp.Name, (string)prop);
                }
            }
            writer.WriteEndObject();
        }
    }
}
