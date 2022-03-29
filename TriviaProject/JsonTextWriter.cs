using System.IO;

namespace TriviaProject
{
    internal class JsonTextWriter
    {
        private StreamWriter file;

        public JsonTextWriter(StreamWriter file)
        {
            this.file = file;
        }
    }
}