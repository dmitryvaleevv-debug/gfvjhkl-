using MainProgram;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace tests
{
    public class TestRunner: IDisposable
    {

        private static string ProjectDir
        {
            get
            {
             var currentDir = Directory.GetCurrentDirectory();
var parent1 = Directory.GetParent(currentDir);
var parent2 = parent1?.Parent;
var parent3 = parent2?.Parent;

return parent3?.FullName ?? throw new InvalidOperationException("Не удалось определить путь");
            }
        }

        private static string TestFilePath {
            get
            {
                return Path.Combine(ProjectDir, "test_cases.txt");
            }
        }
        private static string ResultsFilePath {
            get
            {
                return Path.Combine(ProjectDir, "test_results.txt");
            }
        }

        public TestRunner()
        {
          
            this.writer = new StreamWriter(ResultsFilePath, true, Encoding.UTF8);
        }

        private readonly StreamWriter writer;

         public void Dispose()
        {
            writer.Dispose();
            GC.SuppressFinalize(this);
        }

        [Theory]
        [MemberData(nameof(ReadTestCases))]
        public void RunTests(TestCase testCase)
        {
            var (outputArray, errors) = Program.ProcessArray(testCase.InputArray);

            Assert.Equal(testCase.ExpectedErrors, errors);
            Assert.Equal(testCase.ExpectedOutput, outputArray);

            var testResult = new TestResult(
                Variant: testCase.Variant,
                Set: testCase.Set,
                Path: testCase.Path,
                Input: testCase.InputArray,
                ExpectedOutput: testCase.ExpectedOutput,
                ExpectedErrors: testCase.ExpectedErrors,
                ActualOutput: outputArray,
                ActualErrors: errors
            );
            writer.WriteLine(testResult.ToString());
        }

        public static IEnumerable<object[]> ReadTestCases()
        {
            if (File.Exists(ResultsFilePath))
            {
                File.Delete(ResultsFilePath);
            }

            using var reader = new StreamReader(TestFilePath, Encoding.UTF8);
            string? line;

            while((line = reader.ReadLine()) != null){
                var inputLine = line.Split(',');
                if(inputLine.Length == 0) break;

                var outputLine = reader.ReadLine()!.Split(',');
                yield return [
                    new TestCase(
                        Variant: inputLine[1].Trim(),
                        Set: inputLine[2].Trim(),
                        Path: inputLine[3].Trim(),
                        InputArray: ParseDoubleArray(inputLine[0]),
                        ExpectedOutput: ParseDoubleArray(outputLine[0]),
                        ExpectedErrors: outputLine[1].Trim() == "" ? [] : [ outputLine[1].Trim() ]
                    )
                ];
            }
        }

        private static double[] ParseDoubleArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return new double[0];
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                       .Select(e => double.Parse( e, CultureInfo.InvariantCulture))
                       .ToArray();
        }
    }
}