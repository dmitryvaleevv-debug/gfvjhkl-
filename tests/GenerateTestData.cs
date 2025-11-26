using System.Globalization;
using System.Text;

namespace GenerateTestData
{
    public class Test
    {
        static void Main()
        {
            Gen();
        }
        static public void Gen()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(currentDirectory, "test_cases.txt");

            using var writer = new StreamWriter(path, false, Encoding.UTF8);


            Variantgenerator[] array = [
                new GenerateVariant1(),
                new GenerateVariant3(),
                new GenerateVariant4(),
                new GenerateVariant5(),
            ];

            foreach (var item in array)
            {
                var el = item.Generate();

                foreach (var testCase in el)
                {
                    string input = string.Join(" ", testCase.InputArray.Select(x => x.ToString("G17", CultureInfo.InvariantCulture)));
                    string output = string.Join(" ", testCase.ExpectedOutput.Select(x => x.ToString("G17", CultureInfo.InvariantCulture)));
                    string error = string.Join(" ", testCase.ExpectedErrors);
                    writer.WriteLine($"{input},{testCase.Variant},{testCase.Set},{testCase.Path}");
                    writer.WriteLine($"{output},{error},{testCase.Variant},{testCase.Set},{testCase.Path}");
                }



            }
        }
    }

    abstract class Variantgenerator
    {
        public abstract TestCase[] Generate();

        public abstract string Variant { get; }

        public abstract string Path { get; }

        public IEnumerable<double> GenerateArray(int length, double min, double max)
        {
            if (double.IsNaN(min) || double.IsNaN(max))
                throw new ArgumentException("min и max должны быть валидными");

            if (min > max)
                throw new ArgumentException("min больше чем max");

            var rnd = new Random();

            for (int i = 0; i < length; i++)
            {

                yield return NextDouble(rnd, min, max);
            }
        }

        private ulong NextUInt64(Random random) =>
                 ((ulong)(uint)random.Next(1 << 22)) |
                (((ulong)(uint)random.Next(1 << 22)) << 22) |
                (((ulong)(uint)random.Next(1 << 20)) << 44);


        static double NextDouble(Random random, double min, double max)
        {
            if(max > int.MaxValue){
                max = int.MaxValue;
            }

            if(min < int.MinValue){
                max = int.MinValue;
            }

            double range = max - min;



            return random.NextDouble() * (max - min) + min;
        }
    }

    class ValueCounter
    {
        private const int step = 52;

        static private int counter = 1;

        static public int next()
        {
            return step * counter++;
        }
    }
    class GenerateVariant1 : Variantgenerator
    {

        public override string Variant
        {
            get { return "Вариант 1"; }
        }

        public override string Path
        {
            get { return "1-2-3-11"; }
        }

        public override TestCase[] Generate()
        {


            var result = new TestCase[4];

            result[0] = new TestCase(
                    InputArray: new double[0],
                    ExpectedOutput: new double[0],
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 1"

                );

            result[1] = new TestCase(
                InputArray: [-1],
                ExpectedOutput: [-1],
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 2"
            );


            var input1 = GenerateArray(ValueCounter.next(), -1, double.MaxValue).ToArray();

            result[2] = new TestCase(
                    InputArray: input1,
                    ExpectedOutput: input1,
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 3"
                );

            var input2 = GenerateArray(ValueCounter.next(), -1, double.MaxValue).ToArray();

            input2[0] = double.MaxValue;

            result[3] = new TestCase(
                    InputArray: input2,
                    ExpectedOutput: input2,
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 4"
                );

            return result;
        }
    }

    class GenerateVariant3 : Variantgenerator
    {

        public override string Variant
        {
            get { return "Вариант 3"; }
        }

        public override string Path
        {
            get { return "1-4-5-6-7-8-11"; }
        }
        public override TestCase[] Generate()
        {
            var result = new TestCase[4];

            result[0] = new TestCase(
                InputArray: [double.MinValue],
                ExpectedOutput: new double[0],
                ExpectedErrors: ["Error: -Infinity"],
                Variant: Variant,
                Path: Path,
                Set: "Набор 1"
            );

            var input1 = GenerateArray(ValueCounter.next(), -1, double.MaxValue).ToArray();

            input1[0] = double.MinValue;

            result[1] = new TestCase(
                InputArray: input1,
                ExpectedOutput: new double[0],
                ExpectedErrors: ["Error: -Infinity"],
                Variant: Variant,
                Path: Path,
                Set: "Набор 2"
            );


            var input2 = GenerateArray(ValueCounter.next(), -1, double.MaxValue).ToArray();

            input2[input2.Length / 2] = double.MinValue;

            result[2] = new TestCase(
                InputArray: input2,
                ExpectedOutput: new double[0],
                ExpectedErrors: ["Error: -Infinity"],
                Variant: Variant,
                Path: Path,
                Set: "Набор 3"
            );

            var input3 = GenerateArray(ValueCounter.next(), -1, double.MaxValue).ToArray();

            input3[input3.Length - 1] = double.MinValue;

            result[3] = new TestCase(
                InputArray: input3,
                ExpectedOutput: new double[0],
                ExpectedErrors: ["Error: -Infinity"],
                Variant: Variant,
                Path: Path,
                Set: "Набор 4"
            );
            return result;
        }
    }

    class GenerateVariant4 : Variantgenerator
    {
        public override string Variant
        {
            get { return "Вариант 4"; }
        }

        public override string Path
        {
            get { return "1-4-5-6-7-10-4-11"; }
        }
        public override TestCase[] Generate()
        {
            var result = new TestCase[6];

            result[0] = new TestCase(
                    InputArray: [-3],
                    ExpectedOutput: [-27],
                    ExpectedErrors: new string[0],
                     Variant: Variant,
                    Path: Path,
                    Set: "Набор 1"
                );

            result[1] = new TestCase(
                    InputArray: [-Math.Pow(-double.MinValue, 1.0 / 3)],
                    ExpectedOutput: [Math.Pow(-Math.Pow(-double.MinValue, 1.0 / 3), 3)],
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 2"
                );

            result[2] = new TestCase(
                    InputArray: [-1 - double.Epsilon],
                    ExpectedOutput: [Math.Pow(-1 - double.Epsilon, 3)],
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 3"
                );

            var input1 = GenerateArray(ValueCounter.next(), -1, 0).ToArray();

            input1[0] = -4;
            var output1 = pow(input1);

            result[3] = new TestCase(
                    InputArray: input1,
                    ExpectedOutput: output1,
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 4"
                );

            var input2 = GenerateArray(ValueCounter.next(), -1, 0).ToArray();
            input2[input2.Length / 2] = -5;

            var output2 = pow(input2);

            result[4] = new TestCase(
                    InputArray: input2,
                    ExpectedOutput: output2,
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 5"
                );

            var input3 = GenerateArray(ValueCounter.next(), -1, 0).ToArray();
            input3[input3.Length - 1] = -2;

            var output3 = pow(input3);

            result[5] = new TestCase(
                    InputArray: input3,
                    ExpectedOutput: output3,
                    ExpectedErrors: new string[0],
                    Variant: Variant,
                    Path: Path,
                    Set: "Набор 6"
                );
            return result;
        }

        private double[] pow(double[] array)
        {
            var result = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = Math.Pow(array[i], 3);
            }
            return result;
        }
    }
    class GenerateVariant5 : Variantgenerator
    {

        public override string Variant
        {
            get { return "Вариант 5"; }
        }

        public override string Path
        {
            get { return "1-4-5-6-7-10-4-5-9-10-4-11"; }
        }
        public override TestCase[] Generate()
        {
            var result = new TestCase[6];

            result[0] = new TestCase(
                InputArray: [-3, 25],
                ExpectedOutput: [-27, 25],
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 1"
            );

            result[1] = new TestCase(
                InputArray: [-3, 0],
                ExpectedOutput: [-27, 0],
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 2"
            );

            result[2] = new TestCase(
                InputArray: [-3, double.MaxValue],
                ExpectedOutput: [-27, double.MaxValue],
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 3"
            );

            var input1 = GenerateArray(ValueCounter.next(), 0, double.MaxValue).ToArray();
            var output1 = new double[input1.Length];
            Array.Copy(input1, output1, input1.Length);
            input1[0] = -4;
            output1[0] = -64;

            result[3] = new TestCase(
                InputArray: input1,
                ExpectedOutput: output1,
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 4"
            );

            var input2 = GenerateArray(ValueCounter.next(), 0, double.MaxValue).ToArray();
            var output2 = new double[input2.Length];
            Array.Copy(input2, output2, input2.Length);
            input2[input2.Length / 2] = -5;
            output2[input2.Length / 2] = -125;

            result[4] = new TestCase(
                InputArray: input2,
                ExpectedOutput: output2,
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 5"
            );

            var input3 = GenerateArray(ValueCounter.next(), 0, double.MaxValue).ToArray();
            var output3 = new double[input3.Length];
            Array.Copy(input3, output3, input3.Length);
            input3[input3.Length - 1] = -2;
            output3[input3.Length - 1] = -8;

            result[5] = new TestCase(
                InputArray: input3,
                ExpectedOutput: output3,
                ExpectedErrors: new string[0],
                Variant: Variant,
                Path: Path,
                Set: "Набор 6"
            );
            return result;
        }
    }
}
