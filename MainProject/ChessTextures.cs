using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public static class ChessTextures
{
    public static Texture2D WhiteSquare;
    public static Texture2D BlackSquare;

    public static Texture2D WhiteRook;
    public static Texture2D BlackRook;

    public static Texture2D WhiteBishop;

    public static Texture2D WhiteKing;
    



    public static void InitializeTextures(ContentManager contentManager)
    {
        WhiteSquare = contentManager.Load<Texture2D>(nameof(WhiteSquare));
        BlackSquare = contentManager.Load<Texture2D>(nameof(BlackSquare));
        
        WhiteRook = contentManager.Load<Texture2D>(nameof(WhiteRook));
        BlackRook = contentManager.Load<Texture2D>(nameof(BlackRook));

        WhiteBishop = contentManager.Load<Texture2D>(nameof(WhiteBishop));

        WhiteKing = contentManager.Load<Texture2D>(nameof(WhiteKing));
    }




}