using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_SandboxTest.Options;

namespace MonoGame_SandboxTest.CardRenderSystem
{
    internal class CardController
    {
        List<Card> cards;
        Texture2D defaultCardTexture;

        Vector2 defaultCardScale;
        Vector2 expandedCardScale;
        Vector2 defaultCardSize;
        float paddedCardWidth;

        public CardController(ContentManager contentManager) {
            defaultCardTexture = contentManager.Load<Texture2D>("card_template");
            defaultCardScale = new Vector2(4.25f, 4.25f);
            // expandedCardScale = new Vector2(5.0f, 5.0f);

            defaultCardSize = new Vector2(
                defaultCardTexture.Width * (int)Math.Round(defaultCardScale.X),
                defaultCardTexture.Height * (int)Math.Round(defaultCardScale.Y));

            paddedCardWidth = defaultCardSize.X * 1.1f;

            cards = new List<Card>() {
                new Card(defaultCardTexture, Vector2.Zero, defaultCardScale),
                new Card(defaultCardTexture, Vector2.Zero, defaultCardScale),
            };

            UpdatePositionOfCardsInHand();
        }

        public void Update(GameTime gameTime)
        {
            CheckUserInputs();
            cards.ForEach(card => card.Update(gameTime));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            cards.ForEach(card => card.Draw(gameTime, spriteBatch));
        }

        private void CheckUserInputs()
        {

            if (InputManager.IsKeyDown(Keys.OemPlus))
            {
                cards.Add(new Card(defaultCardTexture, Vector2.Zero, defaultCardScale));
                UpdatePositionOfCardsInHand();
            }

            if (InputManager.IsKeyDown(Keys.OemMinus))
            {
                cards.RemoveAt(cards.Count - 1);
                UpdatePositionOfCardsInHand();
            }

            if (InputManager.IsKeyDown(Keys.Enter))
            {
                Console.WriteLine("Starting position movement...");
                Random rng = new Random();
                cards.ForEach(card => {
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
            int startPosition = screenCenter - (int) Math.Round(((cards.Count - 1) / 2f) * paddedCardWidth);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].SetPosition(new Vector2(startPosition + (i * paddedCardWidth), OptionsManager.screenHeight));
            }
        }
    }
}
