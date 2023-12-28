using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;

namespace MainProject;

public static class CheckMateCalculator
{
    public static bool KingIsChecked { get; private set; }
    public static ChessColor KingChessColor { get; private set; }

    public static List<Square> AttackerSquares;

    public static List<List<Square>> AttackerPaths;

    public static void SetKingIsChecked(ChessColor kingChessColor)
    {
        KingIsChecked = true;
        KingChessColor = kingChessColor;

    }


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
            piece.RemoveSquaresThatAreThreats(squares);
        }
    }
}