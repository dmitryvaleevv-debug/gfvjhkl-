public class TestCase{

    public TestCase(
        double[] InputArray,
        double[] ExpectedOutput,
        string[] ExpectedErrors,
        string Variant,
        string Path,
        string Set
    ){
        this.InputArray = InputArray;
        this.ExpectedOutput = ExpectedOutput;
        this.ExpectedErrors = ExpectedErrors;
        this.Variant = Variant;
        this.Set = Set;
        this.Path = Path;
    }

    public double[] InputArray { get; set; }
    public double[] ExpectedOutput { get; set; }
    public string[] ExpectedErrors { get; set; }
    public string Variant { get; set; }
    public string Set { get; set; }
    public string Path { get; set; }
}

    public class TestResult
    {

        public TestResult(
            string Variant,
            string Set,
            string Path,
            double[] Input,
            double[] ExpectedOutput,
            string[] ExpectedErrors,
            double[] ActualOutput,
            string[] ActualErrors
        ){
            this.Variant = Variant;
            this.Set = Set;
            this.Path = Path;
            this.Input = Input;
            this.ExpectedOutput = ExpectedOutput;
            this.ExpectedErrors = ExpectedErrors;
            this.ActualOutput = ActualOutput;
            this.ActualErrors = ActualErrors;
        }
        public string Variant { get; set; }
        public string Set { get; set; }
        public string Path { get; set; }
        public double[] Input { get; set; }
        public double[] ExpectedOutput { get; set; }
        public string[] ExpectedErrors { get; set; }
        public double[] ActualOutput { get; set; }
        public string[] ActualErrors { get; set; }

        public bool IsPassed =>
            ExpectedOutput.SequenceEqual(ActualOutput) &&
            ExpectedErrors.SequenceEqual(ActualErrors);

        public override string ToString()
        {

            return string.Join("\n", [
                $"Тестовый вариант: {Variant}",
                $"Набор: {Set}",
                $"Путь в потоковом графе: {Path}",
                $"Исходные данные: {string.Join(" ", Input)}",
                $"Ожидаемые результаты: {string.Join(" ", ExpectedOutput)}",
                $"Результат, полученный программой: {string.Join(" ", ActualOutput)}",
                $"Вывод по проверке результата теста: {(IsPassed ? "Тест пройден" : "Тест не пройден")}"
            ]);
        }
    }