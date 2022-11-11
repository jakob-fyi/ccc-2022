using System;
using System.Linq;

namespace teamVoid.CCC2022;

public class Gameboard
{
    public int NumberOfRows { get; set; }
    public int NumberOfColumns { get; set; }
    public string[] Comands { get; set; } = { };
    public string[,] Fields { get; set; } = { };

    public int PackmanX { get; set; }
    public int PackmanY { get; set; }

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
}