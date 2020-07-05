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
using Xunit;
using Scuttlebutt.RPC;

namespace Scuttlebutt.RPC.Tests
{
    public class ScRPC
    {
        [Fact]
        public void ItSerializes()
        {
            var rpc = RPC.CreateHistoryStream("1");
            var expected = "{\n  \"name\": [\n    \"createHistoryStream\"\n  ],\n  \"type\": \"source\",\n  \"args\": [\n    {\n      \"id\": \"1\"\n    }\n  ]\n}";

            Assert.Equal(expected, rpc.Serialize());
        }
    }
}
