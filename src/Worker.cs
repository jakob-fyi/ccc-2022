using System;

namespace teamVoid.CCC2022;

public class Worker
{
    public string[] Lines { get; set; } = { };
    public string Output { get; set; } = String.Empty;

    public string GetResult(string[] lines)
    {
        this.Lines = lines;
        return Run();
    }

    public string Run()
    {
        var gameboard = new Gameboard();
        gameboard.Fill(this.Lines);
        // gameboard.PrintBoard();
        bool res = gameboard.performMoves();
        gameboard.PrintBoard();
        this.Output = gameboard.Coins.ToString();
        this.Output = res ? " YES" : " NO";

        return this.Output;
    }
}