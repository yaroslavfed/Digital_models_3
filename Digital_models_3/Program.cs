namespace Digital_models_3
{
    internal class Program
    {
        static int x_min, x_max, chromasome_count, break_points = 1;
        static double max_fx, avg_fx, sum_fx = 0/*, new_copies_count*/;

        static List<int> chromasome_x = new List<int>();
        //static List<string> primary_population = new List<string>();
        static List<string> chromasome_binary = new List<string>();     // передать
        static List<double> objective_function = new List<double>();
        static List<double> PiOP = new List<double>();
        static List<double> expected_copies = new List<double>();
        static List<double> received_copies = new List<double>();       // передать
        //static List<double> objective_function_percent = new List<double>();

        static List<int> genotype = new List<int>();

        private static double Function(double x)
        {
            return x * x;
        }

        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                chromasome_count = int.Parse(sr.ReadLine());
                x_min = int.Parse(sr.ReadLine());
                x_max = int.Parse(sr.ReadLine());
            }

            Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            RandomPoints(rand.Next(), x_min, x_max);
            max_fx = objective_function.Max(x => x);
            avg_fx = objective_function.Sum(x => x) / chromasome_count;

            for(int i = 0; i < chromasome_count; i++)
            {
                PiOP.Add((double)objective_function[i] / (double)sum_fx);
                expected_copies.Add(PiOP[i] * chromasome_count);
                received_copies.Add(Math.Round(expected_copies[i], MidpointRounding.ToEven));
            }

            //new_copies_count = received_copies.Sum(x => x);
            Output();

            CrossingOver.CreatingPairs(received_copies);
        }

        private static void RandomPoints(int seed, int lower, int upper)
        {
            Console.WriteLine("Seed = {0}, lower = {1}, upper = {2}:\n", seed, lower, upper);
            Random randObj = new Random(seed);

            // Generate 4 random integers from the lower to upper bounds.
            for (int j = 0; j < chromasome_count; j++)
            {
                int x = randObj.Next(lower, upper);

                chromasome_x.Add(x);
                chromasome_binary.Add(FuncTo2(x));

                double y = Function(x);
                objective_function.Add(y);

                sum_fx += y;
            }
            EqualizingBinarity();
        }

        private static string FuncTo2(int x)
        {
            return Convert.ToString(x, 2);
        }

        private static void EqualizingBinarity()
        {
            int length_count = MaxLength(chromasome_binary);
            for (int i = 0; i < chromasome_count; i++)
            {
                while (chromasome_binary[i].Length < length_count)
                {
                    chromasome_binary[i] = chromasome_binary[i].Insert(0, "0");
                }
            }
        }

        static int MaxLength(List<string> a)
        {
            int n = a.Count;
            int max = a[0].Length;
            for (int i = 0; i < n; i++)
            {
                if (a[i].Length > max)
                    max = a[i].Length;
            }
            return max;

        }

        private static void Output()
        {
            for (int i = 0; i < chromasome_count; i++)
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4:F2} \t {5:F2} \t {6:F0}", (i + 1), chromasome_binary[i], chromasome_x[i], objective_function[i], PiOP[i], expected_copies[i], received_copies[i]);
            }
            Console.WriteLine("sum_fx: {0}", sum_fx);
            Console.WriteLine("avg_fx: {0:F1}", avg_fx);
            Console.WriteLine("max_fx: {0}", max_fx);
            //Console.WriteLine("new_copies: {0}", new_copies_count);
        }
    }
}