using NUnit.Framework;
using Rhino.Mocks;

namespace XStream.Converters
{
    [TestFixture]
    public class IntConverterTest
    {
        private IntConverter converter = new IntConverter();

        [Test]
        public void Marshals()
        {
            MockRepository mocks = new MockRepository();
            XStreamWriter writer = mocks.CreateMock<XStreamWriter>();
            writer.SetValue(100.ToString());
            mocks.ReplayAll();
            converter.ToXml(100, writer, new MarshallingContext(writer));
            mocks.VerifyAll();
        }

        [Test]
        public void Unmarshals()
        {
            MockRepository mocks = new MockRepository();
            XStreamReader reader = mocks.CreateMock<XStreamReader>();
            Expect.Call(reader.GetValue()).Return(100.ToString());
            mocks.ReplayAll();
            Assert.AreEqual(100, converter.FromXml(reader, new UnmarshallingContext(reader)));
            mocks.VerifyAll();
        }
    }
}