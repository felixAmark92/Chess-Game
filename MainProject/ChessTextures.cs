using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public static class ChessTextures
{
    public static Texture2D WhiteSquare { get; private set; } 
    public static Texture2D BlackSquare { get; private set; }

    public static Texture2D WhiteRook { get; private set; }
    public static Texture2D BlackRook { get; private set; }
    
    public static Texture2D WhiteKnight { get; private set; }
    public static Texture2D BlackKnight { get; private set; }

    public static Texture2D WhiteBishop { get; private set; }
    public static Texture2D BlackBishop { get; private set; }
    
    public static Texture2D WhiteQueen { get; private set; }
    public static Texture2D BlackQueen { get; private set; }

    public static Texture2D WhiteKing { get; private set; }
    public static Texture2D BlackKing { get; private set; }
    
    public static Texture2D BlackPawn { get; private set; }
    public static Texture2D WhitePawn { get; private set; }

    public static void InitializeTextures(ContentManager contentManager)
    {
        WhiteSquare = contentManager.Load<Texture2D>(nameof(WhiteSquare));
        BlackSquare = contentManager.Load<Texture2D>(nameof(BlackSquare));
        
        WhiteRook = contentManager.Load<Texture2D>(nameof(WhiteRook));
        BlackRook = contentManager.Load<Texture2D>(nameof(BlackRook));

        WhiteKnight = contentManager.Load<Texture2D>(nameof(WhiteKnight));
        BlackKnight = contentManager.Load<Texture2D>(nameof(BlackKnight));

        WhiteBishop = contentManager.Load<Texture2D>(nameof(WhiteBishop));
        BlackBishop = contentManager.Load<Texture2D>(nameof(BlackBishop));
        
        WhiteQueen = contentManager.Load<Texture2D>(nameof(WhiteQueen));
        BlackQueen = contentManager.Load<Texture2D>(nameof(BlackQueen));
        
        WhiteKing = contentManager.Load<Texture2D>(nameof(WhiteKing));
        BlackKing = contentManager.Load<Texture2D>(nameof(BlackKing));
        
        BlackPawn = contentManager.Load<Texture2D>(nameof(BlackPawn));
        WhitePawn = contentManager.Load<Texture2D>(nameof(WhitePawn));
    }
}