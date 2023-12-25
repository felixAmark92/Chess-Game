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
    public static ChessBoard ChessBoard { get; set; }

    private static Dictionary<ChessType, Func<Point, ChessColor,  ChessPiece>> _chessClasses = new()
    {
        { ChessType.Rook, (point, color) => new RookPiece(color, ChessBoard, point) },
        { ChessType.Bishop, (point, color) => new BishopPiece(color, ChessBoard, point) }

    };

    private static Dictionary<(ChessType, ChessColor), Texture2D> _chessPieceTextures = new()
    {
        { (ChessType.Rook, ChessColor.White), Textures.WhiteRook  },
        { (ChessType.Bishop, ChessColor.White), Textures.WhiteBishop  }

    };
    
    public static Entity.Entity CreateChessPiece(ChessType chessType, ChessColor chessColor, Point pos)
    {
        // var entity = EntityManager.CreateEntity();
        // Console.WriteLine($"entityid: {entity.Id}");
        //
        // var piece = _chessClasses[chessType](pos, chessColor);
        //
        // entity.GetComponent<Transform>().Position = 
        //     new Vector2(pos.X * ChessBoard.SquaresSize, pos.Y * ChessBoard.SquaresSize);
        //
        // entity.GetComponent<Renderer>().Texture = _chessPieceTextures[(chessType, chessColor)];
        //
        // entity.AddComponent(new Interactive(entity.GetComponent<Renderer>(), entity.GetComponent<Transform>()));
        //
        // entity.GetComponent<Interactive>().OnLeftClick += () =>
        // {
        //     Console.WriteLine(entity.Id);
        //
        //     var list = piece.GetMovableSquares().Select(s => s.Entity);
        //     
        //     Highlighter.HighlightEntities(list);
        // };
        //
        // entity.AddComponent(piece);
        // piece.Entity = entity;
        //
        // return entity;

        return EntityBuilder.Create()
            .WithComponent<Interactive>()
            .AddTexture(_chessPieceTextures[(chessType, chessColor)])
            .AddBehaviour(_chessClasses[chessType](pos, chessColor))
            .Build();


    }


}