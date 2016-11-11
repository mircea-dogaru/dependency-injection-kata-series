using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;
using NDesk.Options;

namespace DependencyInjection.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var useColors = false;
            var width = 25;
            var height = 15;
            var pattern = "circle";

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);

            var app = GetPatternApp(useColors, pattern);
            app.Run(width, height);
        }

        private static IPatternApp GetPatternApp(bool useColors, string pattern)
        {
            var patternWriter = GetPatternWriter(useColors);
            var patternGenerator = GetPatternGenerator(pattern);

            return new PatternApp(patternWriter, patternGenerator);
        }

        private static IPatternGenerator GetPatternGenerator(string pattern)
        {
            return new PatternGenerator(GetPatternPainter(pattern));
        }

        private static ISquarePainter GetPatternPainter(string pattern)
        {
            switch (pattern)
            {
                case "square":
                    return new WhiteSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                default:
                    return new CircleSquarePainter();
            }
        }

        private static IPatternWriter GetPatternWriter(bool useColors)
        {
            ICharacterWriter characterWriter = new AsciiWriter();
            if (useColors)
            {
                characterWriter = new ColorWriter(characterWriter);
            }

            return new PatternWriter(characterWriter);
        }
    }
}
