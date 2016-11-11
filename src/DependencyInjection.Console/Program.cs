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
            var pattern = "circle"; // TODO: Hook this up

            var optionSet = new OptionSet
            {
                {"c|colors", value => useColors = value != null},
                {"w|width=", value => width = int.Parse(value)},
                {"h|height=", value => height = int.Parse(value)},
                {"p|pattern=", value => pattern = value}
            };
            optionSet.Parse(args);

            var app = GetPatternApp(useColors);
            app.Run(width, height);
        }

        private static IPatternApp GetPatternApp(bool useColors)
        {
            var patternWriter = GetPatternWriter(useColors);
            var patternGenerator = GetPatternGenerator();

            return new PatternApp(patternWriter, patternGenerator);
        }

        private static IPatternGenerator GetPatternGenerator()
        {
            return new PatternGenerator(new CircleSquarePainter());
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
