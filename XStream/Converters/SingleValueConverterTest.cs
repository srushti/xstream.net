using System;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class SingleValueConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsPrimitives() {
            SerialiseAssertAndDeserialise(100, "<Int32 class=\"System.Int32\">100</Int32>");
            SerialiseAssertAndDeserialise(new DateTime(1983, 2, 26, 11, 00, 12, 111), "<DateTime class=\"System.DateTime\">26/02/198311:00:12</DateTime>");
            SerialiseAssertAndDeserialise(10.11, "<Double class=\"System.Double\">10.11</Double>");
            SerialiseAssertAndDeserialise(100L, "<Int64 class=\"System.Int64\">100</Int64>");
            SerialiseAssertAndDeserialise("something ddd7984289*((***('/<>", "<String class=\"System.String\">something ddd7984289*((***('/&lt;&gt;</String>");
            SerialiseAssertAndDeserialise(new decimal(111), "<Decimal class=\"System.Decimal\">111</Decimal>");
            SerialiseAssertAndDeserialise(typeof (int), "<RuntimeType class=\"System.RuntimeType\">System.Int32</RuntimeType>");
            SerialiseAssertAndDeserialise(true, "<Boolean class=\"System.Boolean\">True</Boolean>");
            SerialiseAssertAndDeserialise(byte.MaxValue, "<Byte class=\"System.Byte\">255</Byte>");
            Guid guid = Guid.NewGuid();
            SerialiseAssertAndDeserialise(guid, "<Guid class=\"System.Guid\">" + guid + "</Guid>");
            SerialiseAssertAndDeserialise(new UInt16(), "<UInt16 class=\"System.UInt16\">0</UInt16>");
            SerialiseAssertAndDeserialise(new UInt32(), "<UInt32 class=\"System.UInt32\">0</UInt32>");
            SerialiseAssertAndDeserialise(new UInt64(), "<UInt64 class=\"System.UInt64\">0</UInt64>");
            SerialiseAssertAndDeserialise('s', "<Char class=\"System.Char\">s</Char>");
        }

        [Test]
        public void HandlesNullablePrimitives() {
            int? nullableInt = new int?(111);
            SerialiseAndDeserialise(nullableInt);
            nullableInt = null;
            SerialiseAndDeserialise(nullableInt);
        }
    }
}