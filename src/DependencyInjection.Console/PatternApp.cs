namespace DependencyInjection.Console
{
    internal class PatternApp : IPatternApp
    {
        private readonly IPatternWriter _patternWriter;
        private readonly IPatternGenerator _patternGenerator;

        public PatternApp(IPatternWriter patternWriter, IPatternGenerator patternGenerator)
        {
            _patternWriter = patternWriter;
            _patternGenerator = patternGenerator;
        }

        public void Run(int width, int height)
        {
            var pattern = _patternGenerator.Generate(width, height);
            _patternWriter.Write(pattern);
        }
    }
}