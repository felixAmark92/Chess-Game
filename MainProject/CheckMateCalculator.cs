using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;

namespace MainProject;

public static class CheckMateCalculator
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
            piece.GetMovableSquares(true);
        }
    }

    public static void CalculateThreats( KingPiece kingPiece, List<Square> squares)
    {
        var pieces = 
            kingPiece.ChessColor == ChessColor.Black ? ChessManager.WhitePieces : ChessManager.BlackPieces;
        
        var threats = new List<Square>();
        foreach (var piece in pieces)
        {
            if (piece is KingPiece )
            {
                //TODO: will cause a stackoverflow if a king is checking since it references this. add a manual king check or something 
                continue;
            }
            piece.RemoveSquaresThatAreThreats(squares);
        }
    }
}