using System;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class SingleValueConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsPrimitives() {
            SerialiseAssertAndDeserialise(100, "<System.Int32>100</System.Int32>");
            SerialiseAssertAndDeserialise(new DateTime(1983, 2, 26, 11, 00, 12, 111), "<System.DateTime>26/02/198311:00:12</System.DateTime>");
            SerialiseAssertAndDeserialise(10.11, "<System.Double>10.11</System.Double>");
            SerialiseAssertAndDeserialise(100L, "<System.Int64>100</System.Int64>");
            SerialiseAssertAndDeserialise("something ddd7984289*((***('/<>", "<System.String>something ddd7984289*((***('/&lt;&gt;</System.String>");
        }
    }
}