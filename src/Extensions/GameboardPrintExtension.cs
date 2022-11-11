using System;
using System.Linq;

namespace teamVoid.CCC2022;

public static class GameboardprintExtension
{
    public static void PrintBoard(this Gameboard board)
    {
        foreach (var row in board.Fields)
        {
            Console.WriteLine(String.Join("", row));
        }
    }
}