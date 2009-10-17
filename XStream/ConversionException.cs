using System;

namespace xstream {
    public class ConversionException : Exception {
        public ConversionException(string message) : base(message) {}

        public ConversionException(string message, Exception innerException) : base(message, innerException) {}
    }
}