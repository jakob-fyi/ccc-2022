using System;
using System.IO;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace teamVoid.CCC2022;

public class Program
{
    public static string? manualInput { get; set; } = null;

    static void Main(string[] args)
    {
        var fileOption = new Option<string?>(name: "--file");
        var inputOption = new Option<string?>(name: "--input");

        var rootCommand = new RootCommand("CCC App");
        rootCommand.AddOption(fileOption);
        rootCommand.AddOption(inputOption);

        rootCommand.SetHandler<string?, string?>((file, input) => HandleOptions(file, input), fileOption, inputOption);
        rootCommand.Invoke(args);
    }

    static void HandleOptions(string? file, string? input)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================================");
        Console.WriteLine("Team Void - CCC 2022                                        ");
        Console.WriteLine("============================================================");
        Console.ForegroundColor = ConsoleColor.White;

        var worker = new Worker();

        if (!string.IsNullOrWhiteSpace(file))
        {
            string basePath = "../assets";
            var inputFullPath = Path.GetFullPath(Path.Combine(basePath, file));

            if (!File.Exists(inputFullPath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            var fileName = Path.GetFileNameWithoutExtension(inputFullPath);
            var outputFullPath = inputFullPath.Replace(".in", ".out");

            if (File.Exists(outputFullPath))
            {
                File.Delete(outputFullPath);
            }

            File.Create(outputFullPath).Dispose();
            var lines = File.ReadLines(inputFullPath).ToArray();
            var result = worker.GetResult(lines);

            // results = results.SelectMany(result => new[] { result, "------------------------------------------------------------" }).ToList();

            File.WriteAllLines(outputFullPath, new string[] { result });
        }
        // else if (!string.IsNullOrWhiteSpace(input))
        // {
        //     Console.WriteLine("Single Run");
        //     Console.WriteLine("------------------------------------------------------------");
        //     Console.WriteLine($"Input: {input}");

        //     var result = worker.GetResult(input);

        //     Console.WriteLine($"Output: {result}");
        //     Console.WriteLine("============================================================");
        // }
        // else
        // {
        //     Console.WriteLine("Single Run");
        //     Console.WriteLine("------------------------------------------------------------");

        //     input = Console.ReadLine() ?? throw new ArgumentNullException("Input is null!");
        //     ClearConsoleLine(1);

        //     Console.WriteLine($"Input: {input}");

        //     var result = worker.GetResult(input);

        //     Console.WriteLine($"Output: {result}");
        //     Console.WriteLine("============================================================");
        // }
    }

    public static void ClearConsoleLine(int offset = 0)
    {
        int currentLineCursor = Console.CursorTop - offset;
        Console.SetCursorPosition(0, Console.CursorTop - offset);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
}
