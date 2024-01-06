using System;
using System.Linq;
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

        if (Keyboard.GetState().IsKeyDown(Keys.F))
        {
            Console.WriteLine(ChessManager.ChessBoard.Get_FEN_String());
        }

        foreach (var piece in ChessManager.WhitePieces)
        {
            piece.Entity.GetComponent<Renderer>().Color = piece.IsGuarded ? Color.Blue : Color.White;
        }
        foreach (var piece in ChessManager.BlackPieces)
        {
            piece.Entity.GetComponent<Renderer>().Color = piece.IsGuarded ? Color.Blue : Color.White;
        }

        if (ChessManager.PlayerTurn == ChessColor.Black)
        {
            var computerMove = StockFish.GetCommand(ChessManager.ChessBoard.Get_FEN_String());
        
            char[] startingSquareString = { computerMove[0], computerMove[1] }; 
            char[] targetSquareString = { computerMove[2], computerMove[3] };
        
            var piece = ChessManager.ChessBoard.NotationToSquare(new string(startingSquareString)).OccupyingChessPiece;
            var target = ChessManager.ChessBoard.NotationToSquare(new string(targetSquareString));
        
            ChessManager.SelectedPiece = piece;
            ChessManager.MoveSelectedPiece(target);
        
        }
        


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