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

    public bool InsideChessBoard(Point point)
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
        stringBuilder.Append(ChessManager.PlayerTurn == ChessColor.Black ? " b " : " w ");

        if (Squares[7, 4].SquareState == SquareState.OccupiedByWhite &&
            Squares[7, 7].SquareState == SquareState.OccupiedByWhite)
        {
            if (Squares[7, 4].OccupyingChessPiece is KingPiece {HasMoved: false} &&
                Squares[7, 7].OccupyingChessPiece is RookPiece {HasMoved: false})
            {
                stringBuilder.Append('K');
            }

            
        }
        if (Squares[7, 4].SquareState == SquareState.OccupiedByWhite &&
            Squares[7, 0].SquareState == SquareState.OccupiedByWhite)
        {
            if (Squares[7, 4].OccupyingChessPiece is KingPiece {HasMoved: false} &&
                Squares[7, 0].OccupyingChessPiece is RookPiece {HasMoved: false})
            {
                stringBuilder.Append('Q');
            }
        }
        
        if (Squares[0, 4].SquareState == SquareState.OccupiedByBlack &&
            Squares[0, 7].SquareState == SquareState.OccupiedByBlack)
        {
            if (Squares[0, 4].OccupyingChessPiece is KingPiece {HasMoved: false} &&
                Squares[0, 7].OccupyingChessPiece is RookPiece {HasMoved: false})
            {
                stringBuilder.Append('k');
            }

            
        }
        if (Squares[0, 4].SquareState == SquareState.OccupiedByBlack &&
            Squares[0, 0].SquareState == SquareState.OccupiedByBlack)
        {
            if (Squares[0, 4].OccupyingChessPiece is KingPiece {HasMoved: false} &&
                Squares[0, 0].OccupyingChessPiece is RookPiece {HasMoved: false})
            {
                stringBuilder.Append('q');
            }
        }
        
        return stringBuilder.ToString();
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