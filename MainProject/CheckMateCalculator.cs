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

    public static List<Square> AttackingPieceSquare;

    public static List<List<Square>> AttackerPaths;

    public static void SetKingIsChecked(ChessColor kingChessColor)
    {
        KingIsChecked = true;
        KingChessColor = kingChessColor;

    }


    public static void CalculateChecks(List<ChessPiece> pieces)
    {
        KingIsChecked = false;
        AttackingPieceSquare = new List<Square>();
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
        
        foreach (var piece in pieces)
        {
            piece.IsGuarded = false;

        }
        foreach (var piece in pieces)
        {
            
            piece.RemoveSquaresThatAreAttacked(kingPiece, squares);
        }
    }

    public static void CalculateCheckMate(List<ChessPiece> pieces)
    {
        var availableMoves = new List<Square>();

        foreach (var piece in pieces)
        {
            availableMoves.AddRange(piece.GetMovableSquares(false));
        }

        if (availableMoves.Count < 1)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE CHECKMATE ");
            }
        }
        
    }
    
    public static void CalculatePins(List<ChessPiece> attacker, List<ChessPiece> defender)
    {
        foreach (var piece in attacker)
        {
            piece.IsPinned = false;
        }
        foreach (var piece in defender)
        {
            piece.CalculatePin();
        }
    }
}