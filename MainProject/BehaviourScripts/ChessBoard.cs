using System;
using System.Collections.Generic;
using System.Text;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;
using MainProject.Factories;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts;

public class ChessBoard
{
    public Square[,] Squares { get; } = new Square[8, 8];

    public int SquaresSize { get; set; } = 100;
    
    public void LoadBoard()
    {

        var chessColor = ChessColor.Black;
        
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                var squareEntity = SquareFactory.CreateSquare(chessColor, new Point(j, i), SquaresSize);
                Squares[i, j] = squareEntity.GetBehaviour<Square>();
                chessColor = ColorSwap(chessColor);
            }
            chessColor = ColorSwap(chessColor);
        }
    }

    public bool GetIfInsideChessBoard(Point point)
    {
        return point.X >= 0 &&
               point.X < Squares.GetLength(1) &&
               point.Y >= 0 &&
               point.Y < Squares.GetLength(0);
    }
    
    private ChessColor ColorSwap(ChessColor chessColor)
    {
        return chessColor == ChessColor.Black ? ChessColor.White : ChessColor.Black;
    }
    
    public string Get_FEN_String()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(GetBoardPosition());
        stringBuilder.Append(ChessManager.PlayerTurn == ChessColor.Black ? " b " : " w ");

        var canCastleStringBuilder = new StringBuilder();
        
        canCastleStringBuilder.Append(GetIfCanCastle(Squares[7, 4], Squares[7, 7], SquareState.OccupiedByWhite ,"K"));
        canCastleStringBuilder.Append(GetIfCanCastle(Squares[7, 4], Squares[7, 0], SquareState.OccupiedByWhite, "Q"));
        canCastleStringBuilder.Append(GetIfCanCastle(Squares[0, 4], Squares[0, 7], SquareState.OccupiedByBlack, "k"));
        canCastleStringBuilder.Append(GetIfCanCastle(Squares[0, 4], Squares[0, 0], SquareState.OccupiedByBlack, "q"));

        var canCastleString = canCastleStringBuilder.ToString();

        stringBuilder.Append(string.IsNullOrEmpty(canCastleString) ? "-" : canCastleString);

        stringBuilder.Append(" - 1 11");
        Console.WriteLine(stringBuilder.ToString());
        return stringBuilder.ToString();
    }

    private string GetBoardPosition()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            int j = 0;
            for (int k = 0; k < Squares.GetLength(1); k++)
            {
                if (Squares[i,k].SquareState != SquareState.NotOccupied)
                {
                    if (j != 0)
                    {
                        stringBuilder.Append(j);
                    }
                    j = 0;
                    stringBuilder.Append(Squares[i, k].OccupyingChessPiece.Get_FEN_Notation());
                }
                else
                {
                    j++;
                }
            }
            if (j != 0)
            {
                stringBuilder.Append(j);
            }
            if (i != Squares.GetLength(0) - 1)
            {
                stringBuilder.Append('/');
            }
        }

        return stringBuilder.ToString();
    }

    private string GetIfCanCastle(Square kingSquare, Square rookSquare, SquareState squareState, string castleIdentifier)
    {
        if (kingSquare.SquareState != squareState ||
            rookSquare.SquareState != squareState) return string.Empty;
        
        if (kingSquare.OccupyingChessPiece is KingPiece {HasMoved: false} &&
            rookSquare.OccupyingChessPiece is RookPiece {HasMoved: false})
        {
            return castleIdentifier;
        }
        return string.Empty;
    }

    public Square NotationToSquare(string notation)
    {
        if (notation.Length != 2)
        {
            throw new Exception("notation was in wrong format");
        }

        var row = notation[0] - 'a';
        var column = 8 - (notation[1] - '0');

        return Squares[column, row];
    }
    
}