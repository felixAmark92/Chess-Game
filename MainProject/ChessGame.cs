using System;
using MainProject.Behaviours;
using MainProject.Components;
using MainProject.Enums;
using MainProject.Factories;
using MainProject.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainProject;

public class ChessGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private ChessBoard _chessboard;

    public ChessGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.ApplyChanges();
        Textures.InitializeTextures(Content);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _chessboard = new ChessBoard();
        _chessboard.LoadBoard();
        ChessPieceFactory.ChessBoard = _chessboard;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.White, new Point(5, 5));
        ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.White, new Point(1, 5));

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        InteractiveSystem.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        RenderingSystem.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}