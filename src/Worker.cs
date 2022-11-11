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
        var gameboard = new GameboardSolveable();
        gameboard.Fill(this.Lines);
        bool res = gameboard.performMoves();
        gameboard.PrintBoard();
        
        var solver = new Solver();
        solver.initialize(gameboard);
        var p = solver.computePath();
        this.Output = string.Join("", p.Moves);

        return this.Output;
    }
}