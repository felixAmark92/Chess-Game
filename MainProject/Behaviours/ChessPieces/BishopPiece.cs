using System;
using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public class BishopPiece : ChessPiece
{
    public BishopPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new BishopMovement(chessBoard, this);
    }
}