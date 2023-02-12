namespace MonoGame_SandboxTest.CardRenderSystem
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGame_SandboxTest.Options;

    internal class CardController
    {
        List<Card> cards;
        Texture2D defaultCardTexture;

        Vector2 defaultCardScale;
        Vector2 defaultCardSize;
        float paddedCardWidth;

        public CardController(ContentManager contentManager) {
            this.defaultCardTexture = contentManager.Load<Texture2D>("card_template");
            this.defaultCardScale = new Vector2(4.25f, 4.25f);

            this.defaultCardSize = new Vector2(
                this.defaultCardTexture.Width * (int)Math.Round(this.defaultCardScale.X),
                this.defaultCardTexture.Height * (int)Math.Round(this.defaultCardScale.Y));

            this.paddedCardWidth = this.defaultCardSize.X * 1.1f;

            this.cards = new List<Card>() {
                new Card(this.defaultCardTexture, Vector2.Zero, this.defaultCardScale),
                new Card(this.defaultCardTexture, Vector2.Zero, this.defaultCardScale),
            };

            this.UpdatePositionOfCardsInHand();
        }

        public void Update(GameTime gameTime)
        {
            this.CheckUserInputs();
            this.cards.ForEach(card => card.Update(gameTime));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.cards.ForEach(card => card.Draw(gameTime, spriteBatch));
        }

        private void CheckUserInputs()
        {

            if (InputManager.IsKeyDown(Keys.OemPlus))
            {
                this.cards.Add(new Card(this.defaultCardTexture, Vector2.Zero, this.defaultCardScale));
                this.UpdatePositionOfCardsInHand();
            }

            if (InputManager.IsKeyDown(Keys.OemMinus))
            {
                this.cards.RemoveAt(this.cards.Count - 1);
                this.UpdatePositionOfCardsInHand();
            }

            if (InputManager.IsKeyDown(Keys.Enter))
            {
                Console.WriteLine("Starting position movement...");
                Random rng = new Random();
                this.cards.ForEach(card => {
                    int x = rng.Next(0, OptionsManager.screenWidth);
                    int y = rng.Next(0, OptionsManager.screenHeight);

                    card.MoveTo(new Vector2(x, y), 3.0f);
                });
            }
        }

        private void UpdatePositionOfCardsInHand()
        {
            // Assign card positions based on how many cards exist
            int screenCenter = OptionsManager.screenWidth / 2;
            int startPosition = screenCenter - (int) Math.Round(((this.cards.Count - 1) / 2f) * this.paddedCardWidth);
            for (int i = 0; i < this.cards.Count; i++)
            {
                this.cards[i].SetPosition(new Vector2(startPosition + (i * this.paddedCardWidth), OptionsManager.screenHeight));
            }
        }
    }
}
