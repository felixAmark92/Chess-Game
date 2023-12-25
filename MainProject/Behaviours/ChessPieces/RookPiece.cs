using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public class RookPiece : ChessPiece
{
    public RookPiece(ChessColor color, ChessBoard chessBoard, Point pos) 
        : base(color, chessBoard, pos)
    {
        ChessMovement = new RookMovement(chessBoard, this);
    }
    
}