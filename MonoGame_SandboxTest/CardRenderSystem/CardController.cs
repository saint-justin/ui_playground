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

        public CardController(ContentManager contentManager) {
            defaultCardTexture = contentManager.Load<Texture2D>("card_template");
            defaultCardScale = new Vector2(4.25f, 4.25f);
            expandedCardScale = new Vector2(5.0f, 5.0f);

            defaultCardSize = new Vector2(
                defaultCardTexture.Width * (int)Math.Round(defaultCardScale.X),
                defaultCardTexture.Height * (int)Math.Round(defaultCardScale.Y));

            cards = new List<Card>() {
                new Card(defaultCardTexture, Vector2.Zero, defaultCardScale),
                new Card(defaultCardTexture, Vector2.Zero, defaultCardScale),
            };
        }

        public void Update(GameTime gameTime)
        {
            int screenCenter = OptionsManager.screenWidth / 2;

            // Assign card positions based on how many cards are in the list
            float cardAmountOffset = cards.Count % 2 == 0 ? 0.5f : 0f;
            for (int i = 0; i < cards.Count; i++)
            {
                int relativeCenteredPosition = i - (cards.Count / 2);
                float scaledOffset = cards[i].scaledTextureDimensions.X * (relativeCenteredPosition + cardAmountOffset);
                cards[i].SetPosition(new Vector2(screenCenter + scaledOffset, OptionsManager.screenHeight));
            }

            cards.ForEach(card => 
            { 
                if (card.needsUpdate) { card.Update(gameTime); } 
            });

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus)) { cards.Add(new Card(defaultCardTexture, Vector2.Zero, defaultCardScale)); }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) { cards.RemoveAt(cards.Count - 1); }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            cards.ForEach(card => card.Draw(gameTime, spriteBatch));
        }
    }
}
