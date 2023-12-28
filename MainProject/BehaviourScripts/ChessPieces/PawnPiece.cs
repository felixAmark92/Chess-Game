using System.Collections.Generic;
using System.Linq;
using MainProject.ChessMovements;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class PawnPiece : ChessPiece
{
    private bool _hasMoved;

    public bool IsMoved()
    {
        return _hasMoved;
    }

    public void SetHasMoved()
    {
        _hasMoved = true;
    }
    public PawnPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new PawnMovement(chessBoard, this);
    }

    public override void RemoveSquaresThatAreAttacked(List<Square> kingSquares)
    {
        Point direction = ChessColor == ChessColor.Black 
            ? new Point(0, 1) : new Point(0, -1);

        var thisSquares = new List<Square>();
        
        var rightSide = Pos + direction + new Point(1, 0);
        if (ChessManager.ChessBoard.InsideChessBoard(rightSide))
        {
            if (ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X].SquareState == SquareState.NotOccupied)
            {
                thisSquares.Add(ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X]);
            }
        }
        
        var leftSide = Pos + direction + new Point(-1, 0);
        if (ChessManager.ChessBoard.InsideChessBoard(leftSide))
        {
            if (ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X].SquareState == SquareState.NotOccupied)
            {
                thisSquares.Add(ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X]);

            }
        }
        var squares = kingSquares.Where(s => thisSquares.Contains(s)).ToList();

        foreach (var square in squares)
        {
            kingSquares.Remove(square);
        }
        
    }
}