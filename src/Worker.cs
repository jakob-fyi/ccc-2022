using System;

namespace teamVoid.CCC2022;

public class Worker
{
    public string Input { get; set; } = String.Empty;
    public string Output { get; set; } = String.Empty;

    public string GetResult(string input)
    {
        this.Input = input;
        return Run();
    }

    public string Run()
    {
        this.Output = "Result from Input";

        return this.Output;
    }
}