using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;

namespace MainProject;

public static class CheckCalculator
{
    public static bool KingIsChecked;

    public static List<Square> AttackerSquares;

    public static List<List<Square>> AttackerPaths;


    public static void CalculateChecks(List<ChessPiece> pieces)
    {
        KingIsChecked = false;
        AttackerSquares = new List<Square>();
        AttackerPaths = new List<List<Square>>();
        
        foreach (var piece in pieces)
        {
            Console.WriteLine(piece.GetType());
            piece.GetMovableSquares();
        }
    }
    
    
}