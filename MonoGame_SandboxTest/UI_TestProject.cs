using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_SandboxTest.CardRenderSystem;
using MonoGame_SandboxTest.Options;

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
            OptionsManager.Init(graphics);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (OptionsManager.shouldUpdate) {
                Debug.WriteLine("Options dirty, updating...");
                OptionsManager.UpdateOptions(); 
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Debug.WriteLine("P Key Pressed, Updating Screen Width to " + (OptionsManager.screenWidth + 200));
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth + 200);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                Debug.WriteLine("P Key Pressed, Updating Screen Width to " + (OptionsManager.screenWidth - 200));
                OptionsManager.SetScreenWidth(OptionsManager.screenWidth - 200);
            }

            cardController.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            cardController.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}