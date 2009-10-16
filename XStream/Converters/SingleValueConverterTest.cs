using System;
using NUnit.Framework;

namespace xstream.Converters {
    [TestFixture]
    public class SingleValueConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsPrimitives() {
            SerialiseAndDeserialise(100);
            SerialiseAndDeserialise(new DateTime(1983, 2, 26, 11, 00, 12, 111));
            SerialiseAndDeserialise(10.11);
            SerialiseAndDeserialise(100L);
            SerialiseAndDeserialise("something ddd7984289*((***('/<>");
            SerialiseAndDeserialise(new decimal(111));
            SerialiseAndDeserialise(typeof (int));
            SerialiseAndDeserialise(true);
            SerialiseAndDeserialise(byte.MaxValue);
            Guid guid = Guid.NewGuid();
            SerialiseAndDeserialise(guid);
            SerialiseAndDeserialise(new UInt16());
            SerialiseAndDeserialise(new UInt32());
            SerialiseAndDeserialise(new UInt64());
            SerialiseAndDeserialise('s');
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