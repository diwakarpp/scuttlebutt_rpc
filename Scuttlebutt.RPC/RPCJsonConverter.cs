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
    public class RPCJsonConverter : JsonConverter<RPC>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            var isConvert = typeToConvert == typeof(RequestArgs);

            return isConvert;
        }

        public RPCJsonConverter() { }

        public override RPC Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var obj = new RPC();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        {
                            switch (reader.GetString())
                            {
                                case "name":
                                    {
                                        reader.Read();
                                        if (reader.TokenType != JsonTokenType.StartArray)
                                            throw new InvalidOperationException("Expected start array token");

                                        obj.Name = new List<string>();

                                        try {
                                            while (true) obj.Name.Add(reader.GetString());
                                        }
                                        catch (InvalidOperationException) { }

                                        if (reader.TokenType != JsonTokenType.StartArray)
                                            throw new InvalidOperationException("Expected start array token");

                                        InitializeArgsSubclassFromName(obj);

                                        break;
                                    }
                                case "type":
                                    {
                                        switch (reader.GetString())
                                        {
                                            case "source":
                                                {
                                                    obj.Type = ProcedureType.Source;
                                                    break;
                                                }
                                            case "async":
                                                {
                                                    obj.Type = ProcedureType.Async;
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case "args":
                                    {
                                        // TODO: Dispatch deserializers according to the class
                                        break;
                                    }

                                default: throw new InvalidCastException("Unsupported JSON value");
                            }
                            break;
                        }

                    default:
                        {
                            throw new InvalidCastException("Unsupported JSON value");
                        }
                }
            }

            return obj;
        }

        void InitializeArgsSubclassFromName(RPC rpc)
        {
            switch (rpc.Name[0])
            {
                case "createHistoryStream":
                    {
                        rpc.Args = new List<RequestArgs> {
                            new HistoryStreamRequest("0")
                        };

                        break;
                    }

                default:
                    throw new NotImplementedException("Other methods have not been implemented");
            }
        }

        public override void Write(
            Utf8JsonWriter writer,
            RPC value,
            JsonSerializerOptions options)
        {
            // Hopefully this doesn't recurse and serializes using the default
            // serializer
            JsonSerializer.Serialize(value);
        }
    }
}