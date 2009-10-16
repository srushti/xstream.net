using System;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class DynamicInstanceBuilderTest {
        [Test]
        public void InstantiatesClassWithParameterlessConstructor() {
            Assert.AreEqual(false, typeof (House).IsPublic);
            Assert.AreNotEqual(null, DynamicInstanceBuilder.CreateInstance(typeof (House)));
        }

        [Test]
        public void InstantiatesPublicClassWithNoParameterlessConstructor() {
            Assert.AreEqual(null, typeof (MarshallingContext).GetConstructor(Constants.BINDINGFlags, null, new Type[0], null));
            Assert.AreNotEqual(null, DynamicInstanceBuilder.CreateInstance(typeof (MarshallingContext)));
        }
    }
}