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
    /// <summary>
    ///   This class represents the arguments to the CreateHistoryStream
    ///   procedure call
    /// </summary>
    [JsonConverter(typeof(ArgsJsonConverter))]
    public class HistoryStreamRequest : RequestArgs
    {
        /// <summary>
        ///   The id of the feed
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        ///   If specified, return messages starting at this number, else,
        ///   return from the beginning
        /// </summary>
        [JsonPropertyName("seq")]
        public string Seq { get; set; }

        /// <summary>
        ///   Maximum number of messages to retrieve, will always retrieve the
        ///   earliest, defaults to unlimited
        /// </summary>
        [JsonPropertyName("limit")]
        public string Limit { get; set; }

        /// <summary>
        ///   Keep the stream open to receive new messages as posted, defaults
        ///   to false, closing the stream as the messages are sent
        /// </summary>
        [JsonPropertyName("live")]
        public Nullable<bool> Live { get; set; }

        /// <summary>
        ///   If true starts sending messages already posted by the current
        ///   feed, else, only sends new ones. Defaults to true
        /// </summary>
        [JsonPropertyName("old")]
        public Nullable<bool> Old { get; set; }

        /// <summary>
        ///   If true also send the key and timestamp of the messages. Defaults
        ///   to true
        /// </summary>
        [JsonPropertyName("keys")]
        public Nullable<bool> Keys { get; set; }

        /// <summary>
        ///   Builds the CreateHistoryStream arguments only specifiying the
        ///   required argument, id, leaving the rest empty
        /// </summary>
        /// <param name="id">
        ///   The id in the shape of an @ plus the public key of
        ///   the feed plus .ed25519
        /// <param>
        public HistoryStreamRequest(string id)
        {
            this.Id = id;
        }

        public override RequestArgs Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {

            var obj = new HistoryStreamRequest("0");
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        {
                            switch (reader.GetString())
                            {
                                case "id":
                                    {
                                        obj.Id = reader.GetString();
                                        break;
                                    }
                                case "seq":
                                    {
                                        obj.Seq = reader.GetString();
                                        break;
                                    }
                                case "limit":
                                    {
                                        obj.Limit = reader.GetString();
                                        break;
                                    }
                                case "live":
                                    {
                                        obj.Live = reader.GetBoolean();
                                        break;
                                    }
                                case "old":
                                    {
                                        obj.Old = reader.GetBoolean();
                                        break;
                                    }
                                case "keys":
                                    {
                                        obj.Keys = reader.GetBoolean();
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

                var propType = value.GetType().GetProperty(kvp.Name).PropertyType;

                if (propType == typeof(bool))
                {
                    writer.WriteBoolean(kvp.Name.ToCamelCase(), (bool)prop);
                }
                else
                {
                    writer.WriteString(kvp.Name.ToCamelCase(), (string)prop);
                }
            }

            writer.WriteEndObject();
        }
    }
}
