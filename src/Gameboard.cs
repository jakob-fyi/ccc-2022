using System;
using System.Linq;
using GameFigures;
using GameMoves;

namespace teamVoid.CCC2022;

public class Gameboard
{
    public int NumberOfRows { get; set; }
    public int NumberOfColumns { get; set; }
    public string[] Comands { get; set; } = { };
    public string[,] Fields { get; set; } = { };

    public string[] Moves {get; set;} = {};

    public int PackmanX { get; set; }
    public int PackmanY { get; set; }

    public int Coins { get; set; } = 0;

    public void Fill(string[] lines)
    {
        // Rows
        if (int.TryParse(lines.First(), out int num))
        {
            this.NumberOfRows = num;
        }

        // Columns
        this.NumberOfColumns = lines.Skip(1).First().Length;

        // Fields
        for (int y = 0; y < this.NumberOfRows; y++)
        {
            var elements = lines[y].ToCharArray().Select(c => c.ToString()).ToArray();

            for (int x = 0; x < this.NumberOfColumns; x++)
            {
                this.Fields[x, y] = elements[x];
            }
        }
    }

    public void move(string m)
    {
        // later check for legal move
        if ( m == GameMoves.Left ) 
        {
            PackmanX--;
        }
        else if (m == GameMoves.Right)
        {
            PackmanX++;
        }
        else if (m == GameMoves.Up)
        {
            PackmanY--;
        }
        else if (m == GameMoves.Down)
        {
            PackmanY++;
        }

        collectCoin();
    }

    public void collectCoin()
    {
        if (Fields[PackmanX, PackmanY] == Gamefigures.Coin)
        {
            Fields[PackmanX, PackmanY] = Gamefigures.Empty;
            Coins++;
        }
    }
}