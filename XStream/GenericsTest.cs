using System;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class GenericsTest : ConverterTestCase {
        [Test]
        public void WorksWithAGenericClass() {
            SerialiseAndDeserialise(new GenericObject<int>(1));
        }

        [Test]
        public void SerialisesWhenFieldIsAmbiguousGeneric() {
            SerialiseAndDeserialise(new AmbiguousReferenceHolder(new GenericObject<int>(1)));
        }

        [Test]
        public void FiguresOutNestedGenerics() {
            SerialiseAndDeserialise(new GenericObject<GenericObject<int>>(new GenericObject<int>(100)));
        }

        [Test]
        public void HandlesGenericArray() {
            SerialiseAndDeserialise(new GenericObject<int>[] {new GenericObject<int>(100),});
        }
    }

    internal class GenericObject<T> : DoubleGenericObject<T, int> {
        public T t2;
        protected GenericObject() : base() {}

        public GenericObject(T t) : base(t, 100) {
            t2 = t;
        }

        public bool Equals(GenericObject<T> genericObject) {
            if (genericObject == null) return false;
            if (!base.Equals(genericObject)) return false;
            return Equals(t2, genericObject.t2);
        }

        public override bool Equals(object obj) {
            if (!base.Equals(obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as GenericObject<T>);
        }

        public override int GetHashCode() {
            return base.GetHashCode() + 29*(t2 != null ? t2.GetHashCode() : 0);
        }

        public override string ToString() {
            return "t2 = " + t2 + " " + base.ToString();
        }
    }

    internal class DoubleGenericObject<T, U> : IEquatable<DoubleGenericObject<T, U>> {
        public T t;
        public U u;

        public DoubleGenericObject() {}

        public DoubleGenericObject(T t, U u) {
            this.t = t;
            this.u = u;
        }

        public bool Equals(DoubleGenericObject<T, U> genericObject) {
            if (genericObject == null) return false;
            return Equals(t, genericObject.t) && Equals(u, genericObject.u);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as DoubleGenericObject<T, U>);
        }

        public override string ToString() {
            return "value t = " + t + ", u = " + u + "\tgeneric class: " + typeof (T).FullName + ", " + typeof (U).FullName;
        }

        public override int GetHashCode() {
            return t != null ? t.GetHashCode() : 0;
        }
    }
}