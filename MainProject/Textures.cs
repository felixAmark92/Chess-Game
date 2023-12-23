using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public static class Textures
{
    public static Texture2D WhiteSquare;
    public static Texture2D BlackSquare;

    public static Texture2D WhiteRook;


    public static void InitializeTextures(ContentManager contentManager)
    {
        WhiteSquare = contentManager.Load<Texture2D>(nameof(WhiteSquare));
        BlackSquare = contentManager.Load<Texture2D>(nameof(BlackSquare));
        WhiteRook = contentManager.Load<Texture2D>(nameof(WhiteRook));
    }




}