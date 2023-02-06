using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_SandboxTest.CardRenderSystem
{
    internal class CardController
    {
        List<Card> cards;
        Texture2D defaultCardTexture;

        Vector2 defaultCardScale;
        Vector2 expandedCardScale;
        Vector2 defaultCardSize;

        public CardController(ContentManager contentManager) {
            defaultCardTexture = contentManager.Load<Texture2D>("card_template");
            defaultCardScale = new Vector2(4.25f, 4.25f);
            expandedCardScale = new Vector2(5.0f, 5.0f);

            defaultCardSize = new Vector2(
                defaultCardTexture.Width * (int)Math.Round(defaultCardScale.X),
                defaultCardTexture.Height * (int)Math.Round(defaultCardScale.Y));

            cards = new List<Card>() {
                new Card(defaultCardTexture, new Vector2(100, 100), new Vector2(5.0f, 5.0f)),
                new Card(defaultCardTexture, new Vector2(100, 300), new Vector2(3.5f, 3.5f)),
            };
        }

        public void Update(GameTime gameTime)
        {
            // Assign card default positions based on how many cards are in the list
            for (int i = 0; i < cards.Count; i++)
            {

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            cards.ForEach(card => card.Draw(gameTime, spriteBatch));
        }
    }
}
