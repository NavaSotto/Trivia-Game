using System;

namespace TriviaProject
{
    internal class JObject
    {
        private string v;

        public JObject(string v)
        {
            this.v = v;
        }

        internal void WriteTo(JsonTextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}