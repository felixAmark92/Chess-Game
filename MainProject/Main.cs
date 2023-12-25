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

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Main()
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
        ChessTextures.InitializeTextures(Content);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        ChessManager.LoadPieces();



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

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);
        RenderingSystem.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}