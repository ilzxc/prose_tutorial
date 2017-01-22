using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compiler;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using prosest.Learning;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace prosest
{
    class Program
    {
        public static void Main(string[] args)
        {
            var grammar = DSLCompiler.ParseGrammarFromFile("prosest.grammar").Value;
            Console.WriteLine(grammar.ToString());

            // Parse a proram in this grammar:
            var ast = ProgramNode.Parse("Substring(inp, PositionPair(AbsolutePosition(inp, 0), AbsolutePosition(inp, 5)))",
                                        grammar,
                                        ASTSerializationFormat.HumanReadable);

            // Create an input state to the program. It contains one binding: a variable 'inp' (DSL input) is bound to the string "PROSE Rocks".
            var input = State.Create(grammar.InputSymbol, "PROSE Rocks");

            // Execute the program on the input state.
            var output = (string)ast.Invoke(input);
            Console.WriteLine(output);
            if(output == "PROSE") { Console.WriteLine("output is PROSE!"); }
            else { Console.WriteLine("output is NOT prose, shit's baroque");  }

            string desiredOutput = "PROSE";
            var spec = new ExampleSpec(new Dictionary<State, object> { [input] = desiredOutput });
            var engine = new SynthesisEngine(grammar);
            ProgramSet learned = engine.LearnGrammar(spec);
            if (learned.Size > 0) { Console.WriteLine("POSITIVE LEARNED SIZE!"); }
            else { Console.WriteLine("LEARNED SIZE IS ZERO... :("); }

            System.Threading.Thread.Sleep(5000);
        }
    }
}
