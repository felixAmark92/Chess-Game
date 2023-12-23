using System;
using MainProject.Components;
using MainProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainProject;

public class ChessGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private ChessBoard _chessboard;

    private RookPiece _rookPiece;
    private RookPiece _rookPiece2;

    public ChessGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _chessboard = new ChessBoard();
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Textures.InitializeTextures(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _chessboard.LoadTexture(Content);
        _chessboard.LoadBoard();


        _rookPiece = new RookPiece(Textures.WhiteRook, ChessColor.White, _chessboard, new Point(3, 3));
        _rookPiece2 = new RookPiece(Textures.WhiteRook, ChessColor.Black, _chessboard, new Point(3, 6));


        // TODO: use this.Content to load your game content here

        var moves = _rookPiece.GetMovableSquares();

        foreach (var square in moves)
        {
            square.GetComponent<Renderer>().Color = Color.Green;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _chessboard.Draw(_spriteBatch);
        _rookPiece.GetComponent<Renderer>().Draw(_spriteBatch);
        _rookPiece2.GetComponent<Renderer>().Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}