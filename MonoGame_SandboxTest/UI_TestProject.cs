using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_SandboxTest.CardRenderSystem;
using MonoGame_SandboxTest.Options;
using MonoGame_SandboxTest.Utilities;

namespace MonoGame_SandboxTest
{
    public class UI_TestProject : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Cards manager
        private CardController cardController;


        public UI_TestProject()
        {
            // Graphics settings
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Static Systems
            OptionsManager.Init(graphics);
            InputManager.Init();
            DebugManager.Init(Content);

            // Game Demo Elements
            cardController = new CardController(Content); 

            // LoadContent + additional graphics setup automatically called
            base.Initialize();
        }

        // Used for loading/uploading/reloading after initialization 
        protected override void LoadContent()
        {
            // Default load
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Update user inputs
            InputManager.Update();
            CheckUserInputs();

            // Update entities
            cardController.Update(gameTime);

            // Update debug info
            WriteDebugInfo(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            cardController.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            // Debug should always draw last
            DebugManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        private void CheckUserInputs()
        {
            // Check for screen updates
            if (InputManager.IsKeyDown(Keys.P))
            {
                //Debug.WriteLine("P Key Pressed, Updating Screen Width to " + (OptionsManager.screenWidth + 200));
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth + 200);
            } else if (InputManager.IsKeyDown(Keys.O)) {
                // Debug.WriteLine("O Key Pressed, Updating Screen Width to " + (OptionsManager.screenWidth - 200));
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth - 200);
            }

            // Debug toggle
            if (InputManager.IsKeyDown(Keys.F1)) DebugManager.isEnabled = !DebugManager.isEnabled;
        }

        private void WriteDebugInfo(GameTime gameTime)
        {
            if (!DebugManager.isEnabled) return;

            // Update debug info
            DebugManager.Write(Math.Clamp(Math.Round(1000 / gameTime.ElapsedGameTime.TotalMilliseconds), 0, 99999) + " FPS");
            DebugManager.Write("Screen Width: " + OptionsManager.screenWidth);
            DebugManager.Write("Screen Height: " + OptionsManager.screenWidth);
        }
    }
}