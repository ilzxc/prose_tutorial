using System;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using System.Collections.Generic;

namespace prosest.Learning
{
    public class WitnessFunctions : DomainLearningLogic
    {
        public WitnessFunctions(Grammar grammar) : base(grammar) {}

        // Your custom learning logic here (for example, witness functions)
        [WitnessFunction("Substring", parameterSymbolIndex: 1)]
        DisjunctiveExamplesSpec WitnessPositionPair(GrammarRule rule, ExampleSpec spec)
        {
            var result = new Dictionary<State, IEnumerable<object>>();
            foreach (var example in spec.Examples)
            {
                State inputState = example.Key;
                // the first parameter is the variable symbol inp
                // we extract its currently bound value from the given input state:
                var inp = (string)inputState[rule.Body[0]];
                var substring = (string)example.Value;
                var occurrences = new List<Tuple<int?, int?>>();
                // now iterate over all of the occurrences of substring in inp
                // and store the values for where they occur in our result.
                for (int i = inp.IndexOf(substring);
                     i >= 0;
                     i = inp.IndexOf(substring, i + 1))
                {
                    occurrences.Add(Tuple.Create((int?)i, (int?)i + substring.Length));
                }

                if (occurrences.Count == 0) return null;

                result[inputState] = occurrences;
            }
            return new DisjunctiveExamplesSpec(result);
        }
    }
}
