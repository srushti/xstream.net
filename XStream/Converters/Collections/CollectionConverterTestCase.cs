using System.Collections;
using NUnit.Framework;
using Rhino.Mocks;

namespace XStream.Converters.Collections {
    public abstract class CollectionConverterTestCase : ConverterTestCase {
        protected static void ReadNode(XStreamReader reader, int value) {
            Expect.Call(reader.MoveNext()).Return(true);
            Expect.Call(reader.GetNodeName()).Return("System.Int32");
            Expect.Call(reader.GetValue()).Return(value.ToString());
        }

        protected static void WriteNode(XStreamWriter writer, string nodeName, object value) {
            writer.StartNode(nodeName);
            writer.SetValue(value.ToString());
            writer.EndNode();
        }

        public static void ListAsserter(object first, object second) {
            IList firstList = (IList) first;
            IList secondList = (IList) second;
            Assert.AreEqual(firstList.Count, secondList.Count);
            for (int i = 0; i < firstList.Count; i++)
                Assert.AreEqual(firstList[i], secondList[i]);
        }
    }
}