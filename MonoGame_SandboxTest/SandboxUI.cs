namespace MonoGame_SandboxTest
{
    using System;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGame_SandboxTest.CardRenderSystem;
    using MonoGame_SandboxTest.Options;
    using MonoGame_SandboxTest.Utilities;

    /// <summary>
    /// Sandbox environment to explore creating different UI elements via Monogame.
    /// </summary>
    public class SandboxUI : Game
    {
        // Instanced default classes
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Instanced managers
        private CardController cardController;

        public SandboxUI()
        {
            // Graphics settings
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Static Systems
            OptionsManager.Init(this.graphics);
            InputManager.Init();
            DebugManager.Init(this.Content);

            // Game Demo Elements
            this.cardController = new CardController(this.Content);

            // LoadContent + additional graphics setup automatically called
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Default load
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Update user inputs
            InputManager.Update();
            this.CheckUserInputs();

            // Update entities
            this.cardController.Update(gameTime);

            // Update debug info
            this.WriteDebugInfo(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here
            this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            this.cardController.Draw(gameTime, this.spriteBatch);
            this.spriteBatch.End();

            // Debug should always draw last
            DebugManager.Draw(this.spriteBatch);

            base.Draw(gameTime);
        }

        private void CheckUserInputs()
        {
            // Check for screen updates
            if (InputManager.IsKeyDown(Keys.P))
            {
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth + 200);
            } else if (InputManager.IsKeyDown(Keys.O)) {
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth - 200);
            }

            // Debug toggle
            if (InputManager.IsKeyDown(Keys.F1)) DebugManager.Enabled = !DebugManager.Enabled;
        }

        private void WriteDebugInfo(GameTime gameTime)
        {
            if (!DebugManager.Enabled) return;

            // Update debug info
            DebugManager.WriteLine(Math.Clamp(Math.Round(1000 / gameTime.ElapsedGameTime.TotalMilliseconds), 0, 999999) + " FPS");
            DebugManager.WriteLine("Screen Width: " + OptionsManager.screenWidth);
            DebugManager.WriteLine("Screen Height: " + OptionsManager.screenHeight);

            // Draw horizontal + vertical centers
            DebugManager.DrawLine(
                new Vector2(OptionsManager.screenWidth / 2, 0),
                new Vector2(OptionsManager.screenWidth / 2, OptionsManager.screenHeight),
                3.0f,
                Color.Green);

            DebugManager.DrawLine(
                new Vector2(0, OptionsManager.screenHeight / 2),
                new Vector2(OptionsManager.screenWidth, OptionsManager.screenHeight / 2),
                3.0f,
                Color.Gold);
        }
    }
}