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

using Scuttlebutt.RPC.Codec;

namespace Scuttlebutt.RPC.Tests
{
    public class HeaderCodecTest
    {
        // Header Flags

        [Fact]
        /// <summary>
        ///   Given that we have a JSON body type, it is encoded correctly
        /// </summary>
        public void JSONBodyTypeWorks()
        {
            var header = new Header();
            header.SetBodyType(BodyType.JSON);

            var masked = header.Inner[0] & 0x03;

            Assert.Equal(BodyType.JSON, header.GetBodyType());
            Assert.Equal(0x02, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that we have a binary body type, it is encoded correctly
        /// </summary>
        public void BinaryBodyTypeWorks()
        {
            var header = new Header();
            header.SetBodyType(BodyType.Binary);

            var masked = header.Inner[0] & 0x03;

            Assert.Equal(BodyType.Binary, header.GetBodyType());
            Assert.Equal(0x00, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that we have a UTF-8 String body type, it is encoded
        ///   correctly
        /// </summary>
        public void StringBodyTypeWorks()
        {
            var header = new Header();
            header.SetBodyType(BodyType.UTFStr);

            var masked = header.Inner[0] & 0x03;

            Assert.Equal(BodyType.UTFStr, header.GetBodyType());
            Assert.Equal(0x01, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that there are more messages and that there is no error,
        ///   it is encoded correctly
        /// </summary>
        public void MoreInStream()
        {
            var header = new Header();
            header.SetEndErr(true);
            header.SetEndErr(false);

            var masked = header.Inner[0] & 0x04;

            Assert.False(header.IsEndErr());
            Assert.Equal(0x00, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that there are no more messages and that there is an error,
        ///   it is encoded correctly
        /// </summary>
        public void EndOrError()
        {
            var header = new Header();
            header.SetEndErr(false);
            header.SetEndErr(true);

            var masked = header.Inner[0] & 0x04;

            Assert.True(header.IsEndErr());
            Assert.Equal(0x04, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that this message is not part of a stream, it is encoded
        ///   correctly
        /// </summary>
        public void IsNotStream()
        {
            var header = new Header();
            header.SetStreamBit(true);
            header.SetStreamBit(false);

            var masked = header.Inner[0] & 0x08;

            Assert.False(header.IsStream());
            Assert.Equal(0x00, masked);
        }

        [Fact]
        /// <summary>
        ///   Given that this message is part of a stream, it is encoded
        ///   correctly
        /// </summary>
        public void IsStream()
        {
            var header = new Header();
            header.SetStreamBit(false);
            header.SetStreamBit(true);

            var masked = header.Inner[0] & 0x08;

            Assert.True(header.IsStream());
            Assert.Equal(0x08, masked);
        }

        // Length
        [Fact]
        public void BodyLength()
        {
            var header = new Header();
            // Cast to int then convert to uint, conversion operator looks
            // the same as cast operator
            var length = (uint) (int) "hello world!".Length;

            header.SetBodyLength(length);

            Assert.Equal(length, header.GetBodyLength());
            // TODO: Check inner values
        }

        [Fact]
        public void RequestNumber()
        {
            var header = new Header();
            int reqnum = 49323;

            header.SetRequestNumber(reqnum);

            Assert.Equal(reqnum, header.GetRequestNumber());
            // TODO: Check inner values
        }
    }
}
