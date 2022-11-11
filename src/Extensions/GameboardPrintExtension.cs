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
                Console.Write(board.Fields[x, y]);
            }
            Console.WriteLine();
        }
    }
}