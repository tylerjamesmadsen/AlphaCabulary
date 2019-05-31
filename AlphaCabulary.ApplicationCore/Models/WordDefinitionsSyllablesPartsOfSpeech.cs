using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlphaCabulary.ApplicationCore.Models
{
    public class WordDefinitionsSyllablesPartsOfSpeech
    {
        public string Word { get; }
        [JsonProperty("defs")]
        public IList<string> Definitions { get; }
        public int NumSyllables { get; }
        [JsonProperty("tags")]
        public IList<string> PartsOfSpeech { get; }

        public WordDefinitionsSyllablesPartsOfSpeech(string word, IList<string> definitions, int numSyllables, IList<string> partsOfSpeech)
        {
            Word = word;
            Definitions = definitions;
            NumSyllables = numSyllables;
            PartsOfSpeech = partsOfSpeech;
        }
    }
}
