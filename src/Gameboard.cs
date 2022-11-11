using System;
using System.Collections.Generic;
using System.Linq;

namespace teamVoid.CCC2022;

public class Gameboard
{
    public int NumberOfRows { get; set; }
    public int NumberOfColumns { get; set; }
    public string[] Comands { get; set; } = { };
    public string[,] Fields { get; set; } = { };

    public bool[,] Visited { get; set; } = { };

    public Ghost[] Ghosts { get; set; } = { };

    public string[] Moves { get; set; } = { };

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
        lines = lines.Skip(1).ToArray();

        Fields = new string[this.NumberOfColumns, this.NumberOfRows];
        // Fields
        for (int y = 0; y < this.NumberOfRows; y++)
        {
            var elements = lines[y].ToCharArray().Select(c => c.ToString()).ToArray();

            for (int x = 0; x < this.NumberOfColumns; x++)
            {
                this.Fields[x, y] = elements[x];
            }
        }
        List<int> numbers = new List<int>(Array.ConvertAll(lines[this.NumberOfRows].Split(' '), int.Parse));
        PackmanX = numbers[1] - 1;
        PackmanY = numbers[0] - 1;
        List<int> numMoves = new List<int>(Array.ConvertAll(lines[this.NumberOfRows + 1].Split(' '), int.Parse));
        Moves = new string[numMoves[0]];
        Moves = lines[this.NumberOfRows + 2].ToCharArray().Select(c => c.ToString()).ToArray();

        // Ghosts
        var indexOfGhostsStart = this.NumberOfRows + 3;
        var numberOfGhosts = int.Parse(lines[indexOfGhostsStart]);

        this.Ghosts = new Ghost[numberOfGhosts];

        for (int i = 0; i < numberOfGhosts; i++)
        {
            var ghostStartpositionIndex = indexOfGhostsStart + 1 + (i * 3);
            var ghostNumberOfMovesIndex = indexOfGhostsStart + 2 + (i * 3);
            var ghostMovesIndex = indexOfGhostsStart + 3 + (i * 3);

            var pos = new List<int>(Array.ConvertAll(lines[ghostStartpositionIndex].Split(' '), int.Parse));

            var ghost = new Ghost
            {
                posX = pos[1] - 1,
                posY = pos[0] - 1
            };

            var ghostNumMoves = new List<int>(Array.ConvertAll(lines[ghostNumberOfMovesIndex].Split(' '), int.Parse));
            ghost.Moves = new string[ghostNumMoves[0]];
            ghost.Moves = lines[ghostMovesIndex].ToCharArray().Select(c => c.ToString()).ToArray();

            this.Ghosts[i] = ghost;
        }
    }

    static public Tuple<int, int> move(int x, int y, string move)
    {
        switch (move)
        {
            case "U":
                y--;
                break;
            case "D":
                y++;
                break;
            case "L":
                x--;
                break;
            case "R":
                x++;
                break;
        }
        return new Tuple<int, int>(x, y);
    }

    public bool move(int i)
    {
        var m = Moves[i];
        var res = move(PackmanX, PackmanY, m);
        PackmanX = res.Item1;
        PackmanY = res.Item2;

        foreach (Ghost g in Ghosts)
        {
            m = g.Moves[i];
            res = move(g.posX, g.posY, m);
            g.posX = res.Item1;
            g.posY = res.Item2;
        }

        if (checkCollision() )
        {
            return false;
        }
        
        collectCoin();

        return true;
    }

    public void collectCoin()
    {
        if (Fields[PackmanX, PackmanY] == Gamefigures.Coin)
        {
            Fields[PackmanX, PackmanY] = Gamefigures.Empty;
            Coins++;
        }
    }



    bool checkCollision()
    {
        foreach (Ghost g in Ghosts)
        {
            if (g.posX == PackmanX && g.posY == PackmanY)
            {
                return true;
            }
        }

        if( Fields[PackmanX, PackmanY] == Gamefigures.Wall )
        {
            return true;
        }

        return false;
    }

    public bool performMoves()
    {
        int i = 0;
        for (i = 0; i < Moves.Length; i++)
        {
            if ( !move(i) )
            {
                return false;
            }
        }

        return true;
    }

    public void isLegalMove(string move)
    {

    }

    public void print()
    {
        for (int y = 0; y < this.NumberOfRows; y++)
        {
            for (int x = 0; x < this.NumberOfColumns; x++)
            {
                Console.Write(this.Fields[x, y]);
                Console.Write("(");
                Console.Write(x);
                Console.Write(",");
                Console.Write(y);
                Console.Write(")");
            }
            Console.WriteLine();
        }
    }

}