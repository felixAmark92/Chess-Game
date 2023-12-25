using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using MainProject.Builders;
using MainProject.ChessMovements;
using MainProject.Components;
using MainProject.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Factories;

public static class ChessPieceFactory
{
    private static ChessBoard ChessBoard => ChessManager.ChessBoard;

    private static Dictionary<ChessType, Func<Point, ChessColor,  ChessPiece>> _chessClasses = new()
    {
        { ChessType.Rook, (point, color) => new RookPiece(color, ChessBoard, point) },
        { ChessType.Bishop, (point, color) => new BishopPiece(color, ChessBoard, point) },
        { ChessType.King, (point, color) => new KingPiece(color, ChessBoard, point) },

    };

    private static Dictionary<(ChessType, ChessColor), Texture2D> _chessPieceTextures = new()
    {
        { (ChessType.Rook, ChessColor.White), ChessTextures.WhiteRook  },
        { (ChessType.Rook, ChessColor.Black), ChessTextures.BlackRook  },
        { (ChessType.Bishop, ChessColor.White), ChessTextures.WhiteBishop  },
        { (ChessType.King, ChessColor.White), ChessTextures.WhiteKing  },

    };
    
    public static Entity.Entity CreateChessPiece(ChessType chessType, ChessColor chessColor, Point pos)
    {
        return EntityBuilder.Create()
            .WithComponent<Interactive>()
            .AddTexture(_chessPieceTextures[(chessType, chessColor)])
            .AddBehaviour(_chessClasses[chessType](pos, chessColor))
            .Build();
    }


}