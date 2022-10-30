using System;
using System.IO;
using System.Linq;

namespace teamVoid.CCC2022;

public class Program
{
    public static string? manualInput { get; set; } = null;

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================================");
        Console.WriteLine("Team Void - CCC 2022                                        ");
        Console.WriteLine("============================================================");
        Console.ForegroundColor = ConsoleColor.White;

        var worker = new Worker();

        if (args.Any())
        {
            string basePath = "../assets";
            var inputFullPath = Path.GetFullPath(Path.Combine(basePath, args[0]));

            if (!File.Exists(inputFullPath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            var fileName = Path.GetFileNameWithoutExtension(inputFullPath);
            var outputFullPath = inputFullPath.Replace(fileName, $"{fileName}-result");

            if (File.Exists(outputFullPath))
            {
                File.Delete(outputFullPath);
            }

            File.Create(outputFullPath).Dispose();
            var results = File.ReadLines(inputFullPath).Select(line => worker.GetResult(line)).ToList();

            // results = results.SelectMany(result => new[] { result, "------------------------------------------------------------" }).ToList();

            File.WriteAllLines(outputFullPath, results);
        }
        else
        {
            Console.WriteLine("Single Run");
            Console.WriteLine("------------------------------------------------------------");

            string? input = null;

            if (manualInput is not null)
            {
                input = manualInput;
            }
            else
            {
                input = Console.ReadLine() ?? throw new ArgumentNullException("Input is null!");
                ClearConsoleLine(1);
            }

            Console.WriteLine($"Input: {input}");

            var result = worker.GetResult(input);

            Console.WriteLine($"Output: {result}");
            Console.WriteLine("============================================================");
        }
    }

    public static void ClearConsoleLine(int offset = 0)
    {
        int currentLineCursor = Console.CursorTop - offset;
        Console.SetCursorPosition(0, Console.CursorTop - offset);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
}
