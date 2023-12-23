using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public class RookPiece : ChessPiece
{
    public RookPiece(Texture2D texture, ChessColor color, ChessBoard chessBoard, Point pos) 
        : base(color, chessBoard, pos)
    {
        ChessMovement = new RookMovement(chessBoard, this);
        GetComponent<Renderer>().Texture = texture;
    }
    
}