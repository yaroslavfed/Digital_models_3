using System;

namespace Digital_models_3
{
    internal class Program
    {
        static int x_min, x_max, sum_fx = 0, max_fx, chromasome_count = 4;

        static List<int> chromasome_x = new List<int>();
        //static List<int> chromasome_binary = new List<int>();
        static List<int> primary_population = new List<int>();
        static List<int> objective_function = new List<int>();
        static List<double> PiOP = new List<double>();
        static List<double> expected_copies = new List<double>();
        static List<double> received_copies = new List<double>();
        //static List<double> objective_function_percent = new List<double>();

        static List<int> genotype = new List<int>();

        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                x_min = int.Parse(sr.ReadLine());
                x_max = int.Parse(sr.ReadLine());
            }

            Random rnd = new Random();
            for (int ctr = 0; ctr < chromasome_count; ctr++)
            {
                int x = rnd.Next(x_min, x_max);
                chromasome_x.Add(x);
                primary_population.Add(int.Parse(FuncTo2(x)));

                int y = x * x;
                objective_function.Add(y);

                sum_fx += y;
            }
            max_fx = objective_function.Max(x => x);

            for(int i = 0; i < chromasome_count; i++)
            {
                PiOP.Add((double)objective_function[i] / (double)sum_fx);
                expected_copies.Add(PiOP[i] * chromasome_count);
                received_copies.Add(Math.Round(expected_copies[i], MidpointRounding.ToEven));
            }

            output();

            FirstGeneration firstGeneration = new FirstGeneration();
        }

        private static string FuncTo2(int x)
        {
            return Convert.ToString(x, 2);
        }

        private static void output()
        {
            //Console.WriteLine("№ \t начальная популяция \t x \t f(x) \t значение fi(x)/sum[f(x)] \t ожидаемое число копий \t полученных копий");
            for (int i = 0; i < chromasome_count; i++)
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4:F2} \t {5:F2} \t {6:F0}", (i + 1), primary_population[i], chromasome_x[i], objective_function[i], PiOP[i], expected_copies[i], received_copies[i]);
            }
            Console.WriteLine("sum_fx: {0}", sum_fx);
            Console.WriteLine("max_fx: {0}", max_fx);
        }
    }
}