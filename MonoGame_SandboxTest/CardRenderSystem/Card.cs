using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_SandboxTest.CardRenderSystem
{
    internal class Card : IEntity
    {
        private Texture2D texture;

        public Vector2 scaledTextureDimensions { get; private set; }
        public Vector2 position { get; private set; }
        public Vector2 scale { get; set; }
        public bool needsUpdate { get; private set;  }

        private Rectangle baseTranspose;
        private Rectangle currentDrawPosition;

        public Card(Texture2D texture, Vector2 position, Vector2 scale)
        {
            this.texture = texture;
            this.scaledTextureDimensions = new Vector2(texture.Width * scale.X, texture.Height * scale.Y);

            this.position = position;
            this.scale = scale;

            this.baseTranspose = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
            this.needsUpdate = true;
        }

        public void Initialize() {}

        // Update the card's current draw position to be centered about its current position
        public void Update(GameTime gameTime)
        {
            this.currentDrawPosition = new Rectangle(
                (int) Math.Round(position.X) - (int) Math.Round(scaledTextureDimensions.X / 2),
                (int) Math.Round(position.Y) - (int) Math.Round(scaledTextureDimensions.Y / 2), 
                (int) Math.Round(scaledTextureDimensions.X),
                (int) Math.Round(scaledTextureDimensions.Y));

            needsUpdate = false;
        }

        // Draws card centered about its position
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.currentDrawPosition, 
                this.baseTranspose, 
                Color.White);
        }
    }
}
