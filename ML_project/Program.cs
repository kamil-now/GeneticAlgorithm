using CommandLine;
using Common;
using GeneticAlgorithm;
using GeneticAlgorithm.Abstractions;
using GeneticAlgorithm.CrossoverMethods;
using GeneticAlgorithm.MutationMethods;
using GeneticAlgorithm.SelectionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_project
{
    enum Function
    {
        SinXCosY = 0,
        Schwefel = 1

    }
    class Options
    {
        [Option('f', "function", Required = true, Default = "sin(x) * cos(x)", HelpText = "\ndescribes function to be processed:\n 0: sin(x) * cos(x)\n 1: schwefel function")]
        public Function Function { get; set; }

        [Option('o', "output file path")]
        public string OutputFile { get; set; }
        [Option('p', "population size", Default = 100)]
        public int PopulationSize { get; set; }
        [Option('i', "iteration to print", Default = 10000)]
        public ulong IterationToPrint { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
#if (DEBUG)
            var options = new Options()
            {
                Function = Function.SinXCosY,
                PopulationSize = 100,
                IterationToPrint = 10000
            };
#else
            var options = EvaluateCommandLineArguments(args);
#endif
            var function = GetFunction(options.Function);

            var algorithm = GetAlgorithm(function);
            var population = GetPopulation(options.PopulationSize, 2);

            algorithm.IterationCompleted += (p, i) =>
             {
                 if (i % options.IterationToPrint != 0)
                     return;
                 Console.SetCursorPosition(0, 0);
                 Console.WriteLine(i);
                 var best = p.Elements.OrderBy(x => x.Fitness).First();
                 var worst = p.Elements.OrderByDescending(x => x.Fitness).First();

                 Console.WriteLine("BEST:");
                 Console.WriteLine($"\tValue: {Math.Round(best.Value, 4)}, Fitness: {Math.Round(best.Fitness, 4)}");
                 Console.WriteLine($"\tData: {string.Join(", ", best.Data.Select(x => Math.Round(x, 4)))}");
             };
            algorithm.RUN(population);
            WaitForKey(ConsoleKey.Escape);
            algorithm.STOP();
            Console.ReadKey();
        }
        static Func<double[], double> GetFunction(Function opt)
        {
            switch (opt)
            {

                case Function.Schwefel:
                    return (double[] tab) =>
                    {
                        var x = tab[0];
                        var y = tab[1];
                        return (-x * Math.Sin(Math.Sqrt(Math.Abs(x)))) + (-y * Math.Sin(Math.Sqrt(Math.Abs(y))));
                    };
                default:
                case Function.SinXCosY:
                    return (double[] tab) =>
                    {
                        var x = tab[0];
                        var y = tab[1];

                        return Math.Sin(x) * Math.Cos(y);
                    };
            }
        }
        static SimpleGeneticAlgorithm GetAlgorithm(Func<double[], double> function)
        {
            return new SimpleGeneticAlgorithm
                (
                    new TournamentSelection(0.6, 100, 5),
                    new PMXCrossover(0.6),
                    new UniformMutation(0.01, -0.1, 0.1),
                    x =>
                    {
                        foreach (var element in x.Elements)
                        {
                            element.Value = function(element.Data);
                            element.Fitness = -1 * element.Value * 10000;
                        }
                    }
                );
        }
        static Population GetPopulation(int count, int dataSize)
        {
            var elements = new List<Element>();
            for (int i = 0; i < count; i++)
            {
                elements.Add(new Element(GetRandomData(dataSize)));
            }
            return new Population(elements);
        }
        static void WaitForKey(ConsoleKey consoleKey)
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != consoleKey);
        }
        static IEnumerable<double> GetRandomData(int size)
        {
            var data =
                Enumerable
                .Range(0, size)
                .Select(x => Utils.RandomDouble(-100, 100))
                .ToArray();

            Utils.Shuffle(data);

            return data;
        }
        static Options EvaluateCommandLineArguments(string[] args)
        {
            Options opt = null;

            var result = Parser.Default.ParseArguments<Options>(args)
             .WithParsed(op =>
             {
                 ValidateOptions(op);
                 opt = op;
             })
             .WithNotParsed((errs) =>
             {
                 Console.ReadKey();
                 Environment.Exit(-1);
             });
            Console.WriteLine("PRESS ENTER TO RUN...");
            Console.ReadKey();
            Console.Clear();
            return opt;
        }
        static void ValidateOptions(Options opts)
        {
            // TODO
        }
    }

}
