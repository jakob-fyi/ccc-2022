using System;
using System.Linq;

namespace teamVoid.CCC2022;

public static class GameboardprintExtension
{
    public static void PrintBoard(this Gameboard board)
    {
        for (int y = 0; y < board.NumberOfRows; y++)
        {
            for (int x = 0; x < board.NumberOfColumns; x++)
            {
                PrintField(board.Fields[x, y]);
            }

            Console.WriteLine();
        }
    }

    public static void PrintBoard(this GameboardSolveable board)
    {
        for (int y = 0; y < board.NumberOfRows; y++)
        {
            for (int x = 0; x < board.NumberOfColumns; x++)
            {
                PrintField(board.Fields[x, y]);
            }

            Console.WriteLine();
        }
    }

    private static void PrintField(string value)
    {
        if (value == Gamefigures.Wall)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (value == Gamefigures.Coin)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else if (value == Gamefigures.Packman)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        Console.Write(value);
        Console.ResetColor();
    }
}