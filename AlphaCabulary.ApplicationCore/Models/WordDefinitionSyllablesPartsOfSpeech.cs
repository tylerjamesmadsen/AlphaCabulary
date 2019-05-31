using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlphaCabulary.ApplicationCore.Models
{
    public class WordDefinitionSyllablesPartsOfSpeech
    {
        public string Word { get; }
        public int NumSyllables { get; }
        [JsonProperty("tags")]
        public IList<string> PartsOfSpeech { get; }
        [JsonProperty("defs")]
        public IList<string> Definitions { get; }

        public WordDefinitionSyllablesPartsOfSpeech(string word, int numSyllables, IList<string> partsOfSpeech, IList<string> definitions)
        {
            Word = word;
            NumSyllables = numSyllables;
            PartsOfSpeech = partsOfSpeech;
            Definitions = definitions;
        }
    }
}
