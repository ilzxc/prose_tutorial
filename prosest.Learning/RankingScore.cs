using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;

namespace prosest.Learning
{
    public class RankingScore : Feature<double>
    {
        public RankingScore(Grammar grammar) : base(grammar, "Score", isComplete: true) {}

        protected override double GetFeatureValueForVariable(VariableNode variable) => 0;

        // Your ranking functions here
        [FeatureCalculator("RegexPosition", Method = CalculationMethod.FromChildrenFeatureValues)]
        double ScoreRegexPosition(double inScore, double rrScore, double kScore) => rrScore * kScore;

        [FeatureCalculator("AbsolutePosition", Method = CalculationMethod.FromChildrenNodes)]
        double ScoreAbsolutePosition(VariableNode inp, LiteralNode k)
        {
            double score = inp.GetFeatureValue(this) + k.GetFeatureValue(this);
            int kValue = (int)(k.Value);
            if (Math.Abs(kValue) <= 1) score *= 10;
            return score;

        }

        [FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
        double KScore(int k) => 1.0 / (1 + Math.Abs(k));

        [FeatureCalculator("Substring")]
        double SubstringScore(double x, double pp) => -Math.Log(pp);

        [FeatureCalculator("PositionPair")]
        double PositionPairScore(double pp1, double pp2) => pp1 * pp2;

        [FeatureCalculator("RegexPair")]
        double RegexPairScore(double pp1, double pp2) => pp1 * pp2;

        [FeatureCalculator("r")]
        double RScore(double pp) => 0.0;
    }
}
