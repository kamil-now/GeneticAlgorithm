using CommandLine;
using Common;
using GeneticAlgorithm;
using GeneticAlgorithm.Abstractions;
using GeneticAlgorithm.CrossoverMethods;
using GeneticAlgorithm.MutationMethods;
using GeneticAlgorithm.SelectionMethods;
using ML_project.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_project
{
    enum Function
    {
        SinXCosY = 0,
        Schwefel = 1,
        Rastring = 2

    }
    class Options
    {
        [Option('f', "function", Default = 0, HelpText = "\ndescribes function to be processed:\n 0: sin(x) * cos(y)\n 1: schwefel function\n 2: rastring function")]
        public Function Function { get; set; }
        [Option('p', "population size", Default = 100)]
        public int PopulationSize { get; set; }
        [Option('m', "mutation chance", Default = 0.01)]
        public double MutationChance { get; set; }
        [Option('s', "selection chance", Default = 0.6)]
        public double SelectionChance { get; set; }
        [Option('c', "crossover chance", Default = 0.6)]
        public double CrossoverChance { get; set; }
        [Option('i', "iteration to print", Default = 10000ul)]
        public ulong IterationToPrint { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
#if (DEBUG)
            var options = new Options()
            {
                Function = Function.Rastring,
                PopulationSize = 100,
                IterationToPrint = 10000,
                MutationChance = 0.01,
                SelectionChance = 0.6,
                CrossoverChance = 0.6
            };
#else
            var options = EvaluateCommandLineArguments(args);
#endif
            var function = GetFunction(options.Function);

            var algorithm = GetAlgorithm(function, options);
            var population = GetPopulation(options.PopulationSize, function);

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
        static IFitnessFunction GetFunction(Function opt)
        {
            switch (opt)
            {
                default:
                case Function.SinXCosY: return new SinCosFunction();
                case Function.Schwefel: return new SchwefelFunction();
                case Function.Rastring: return new RastringFunction();
            }
        }
        static SimpleGeneticAlgorithm GetAlgorithm(IFitnessFunction function, Options options)
        {
            return new SimpleGeneticAlgorithm
                (
                    new TournamentSelection(options.SelectionChance, options.PopulationSize, 5),
                    new PMXCrossover(options.CrossoverChance),
                    new UniformMutation(options.MutationChance, function.MinValue, function.MaxValue),
                    function
                );
        }
        static Population GetPopulation(int count, IFitnessFunction function)
        {
            var elements = new List<Element>();
            for (int i = 0; i < count; i++)
            {
                elements.Add(new Element(GetRandomData(function)));
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
        static IEnumerable<double> GetRandomData(IFitnessFunction function)
        {
            var data =
                Enumerable
                .Range(0, function.DataSize)
                .Select(x => Utils.RandomDouble(function.MinValue, function.MaxValue))
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
                 if(!ValidateOptions(op))
                 {
                     Environment.Exit(-1);
                 }
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
        static bool ValidateOptions(Options opts)
        {
            if (opts.PopulationSize < 0)
            {
                Console.WriteLine("INVALID POPULATION SIZE VALUE");
                return false;
            }
            if (opts.SelectionChance > 1 || opts.SelectionChance < 0)
            {
                Console.WriteLine("INVALID SELECTION CHANCE VALUE");
                return false;
            }
            if (opts.CrossoverChance > 1 || opts.CrossoverChance < 0)
            {
                Console.WriteLine("INVALID CROSSOVER CHANCE VALUE");
                return false;
            }
            if (opts.MutationChance > 1 || opts.MutationChance< 0)
            {
                Console.WriteLine("INVALID MUTATION CHANCE VALUE");
                return false;
            }
            return true;
        }
    }

}
