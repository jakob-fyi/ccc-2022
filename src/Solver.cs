
using System.Collections.Generic;
using System.Linq;

namespace teamVoid.CCC2022;
public class Solver
{
    public bool [,] Visited { get; set; } = {};
    public List<string>[,] possibleMoves { get; set; } = {};
    public OurPath p;

    GameboardSolveable gameboard;


    public void initialize(GameboardSolveable board)
    {
        gameboard = board;
        p = new OurPath();
        Visited = new bool[gameboard.NumberOfColumns, gameboard.NumberOfRows];
        p.Positions.Add(new Position(gameboard.PackmanX, gameboard.PackmanY));
        possibleMoves = new List<string>[gameboard.NumberOfColumns, gameboard.NumberOfRows];
        for (int y = 0; y < gameboard.NumberOfRows; y++)
        {
            for (int x = 0; x < gameboard.NumberOfColumns; x++)
            {   

                possibleMoves[x, y] = new List<string>();
                if ( x > 0 && ( gameboard.Fields[x-1, y] == "C" || gameboard.Fields[x-1, y] == " " ) )
                {
                    possibleMoves[x, y].Add("L");
                }
                if ( x < gameboard.NumberOfColumns - 1 && ( gameboard.Fields[x+1, y] == "C" || gameboard.Fields[x+1, y] == " "  ) )
                {
                    possibleMoves[x, y].Add("R");
                }
                if ( y > 0 && ( gameboard.Fields[x, y-1] == "C" || gameboard.Fields[x, y-1] == " "  ))
                {
                    possibleMoves[x, y].Add("U");
                }
                if ( y < gameboard.NumberOfRows - 1 && ( gameboard.Fields[x, y+1] == "C" || gameboard.Fields[x, y+1] == " "  ) )
                {
                    possibleMoves[x, y].Add("D");
                }
            }
        }

    
    }
    
    public static Position Move(Position position, string move)
    {
        switch (move)
        {
            case "L":
                return new Position(position.X - 1, position.Y);
            case "R":
                return new Position(position.X + 1, position.Y);
            case "U":
                return new Position(position.X, position.Y - 1);
            case "D":
                return new Position(position.X, position.Y + 1);
            default:
                return position;
        }
    }

    public string haveGoodMove(List<string> moves, Position curPos)
    {
        foreach( var move in moves )
        {
            var pos = Move(curPos, move);
            if (!Visited[pos.X, pos.Y] )
            {
                // p.Positions.Add(pos);
                // continue;
                return move;
            }
        }
        return "";
    }

    public static string oppositeMove(string move)
    {
        switch (move)
        {
            case "L":
                return "R";
            case "R":
                return "L";
            case "U":
                return "D";
            case "D":
                return "U";
            default:
                return "";
        }
    }

    public OurPath computePath()
    {

        while(true)
        {
            var coins = gameboard.countCoins();
            if ( coins == 12)
            {
                gameboard.PrintBoard();
            }
            if ( coins == 0 )
            {
                break;
            }

            var possibleMovesHere = possibleMoves[p.Positions.Last().X, p.Positions.Last().Y];
            Visited[p.Positions.Last().X, p.Positions.Last().Y] = true;
            var curPos = p.Positions.Last();

            var goodMove = haveGoodMove(possibleMovesHere, curPos);

            if (goodMove != "")
            {
                p.Positions.Add(Move(curPos, goodMove));
                p.Moves.Add(goodMove);
                gameboard.Fields[curPos.X, curPos.Y] = " ";                
                continue;
            }

            int i = 0;
            while(true)
            {  
                
                var movesHere = possibleMoves[curPos.X, curPos.Y];
                if (haveGoodMove(movesHere, curPos) != "")
                {
                    break;
                }

                p.Moves.Add(oppositeMove(p.Moves[p.Moves.Count - 1 - i]));
                if (p.Positions.Count > 1)
                {
                    p.Positions.RemoveAt(p.Positions.Count - 1);
                    curPos = p.Positions.Last();
                }
                else
                {
                    curPos.X = gameboard.PackmanX;
                    curPos.Y = gameboard.PackmanY;
                    break;
                    
                }
                
                ++i;
            }
        }
        return p;
    }
}