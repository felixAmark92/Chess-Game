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
        { ChessType.Knight, (point, color) => new KnightPiece(color, ChessBoard, point) },
        { ChessType.King, (point, color) => new KingPiece(color, ChessBoard, point) },
        { ChessType.Queen, (point, color) => new QueenPiece(color, ChessBoard, point) },
        { ChessType.Pawn, (point, color) => new PawnPiece(color, ChessBoard, point) },

    };

    private static Dictionary<(ChessType, ChessColor), Texture2D> _chessPieceTextures = new()
    {
        { (ChessType.Rook, ChessColor.White), ChessTextures.WhiteRook  },
        { (ChessType.Rook, ChessColor.Black), ChessTextures.BlackRook  },
        { (ChessType.Bishop, ChessColor.White), ChessTextures.WhiteBishop  },
        { (ChessType.Bishop, ChessColor.Black), ChessTextures.BlackBishop  },
        { (ChessType.Knight, ChessColor.White), ChessTextures.WhiteKnight  },
        { (ChessType.Knight, ChessColor.Black), ChessTextures.BlackKnight  },
        { (ChessType.Queen, ChessColor.White), ChessTextures.WhiteQueen  },
        { (ChessType.Queen, ChessColor.Black), ChessTextures.BlackQueen  },
        { (ChessType.King, ChessColor.White), ChessTextures.WhiteKing  },
        { (ChessType.King, ChessColor.Black), ChessTextures.BlackKing  },
        { (ChessType.Pawn, ChessColor.White), ChessTextures.WhitePawn  },
        { (ChessType.Pawn, ChessColor.Black), ChessTextures.BlackPawn  },

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