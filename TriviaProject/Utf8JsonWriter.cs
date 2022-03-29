using System;
using System.IO;

namespace TriviaProject
{
    internal class Utf8JsonWriter
    {
        private FileStream stream;
        private JsonWriterOptions jsonOptions;

        public Utf8JsonWriter(FileStream stream, JsonWriterOptions jsonOptions)
        {
            this.stream = stream;
            this.jsonOptions = jsonOptions;
        }

        internal void WriteStringValue(object jsonData)
        {
            throw new NotImplementedException();
        }
    }
}