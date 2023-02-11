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
        public PositionTracker position { get; private set; }
        public Vector2 scale { get; set; }

        // Denotes whether or not the transpose needs to be re-calculated
        public bool isDirty { get; private set; }
        
        public bool isMoving { get; set; }

        private Rectangle baseTranspose;
        private Rectangle currentTranspose;

        public Card(Texture2D texture, Vector2 position, Vector2 scale)
        {
            this.texture = texture;
            this.position = new PositionTracker(position);
            this.scale = scale;
            
            this.baseTranspose = new Rectangle(0, 0, texture.Width, texture.Height);
            this.scaledTextureDimensions = new Vector2(texture.Width * scale.X, texture.Height * scale.Y);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = new PositionTracker(position);
            this.isDirty = true;
        }

        public void MoveTo(Vector2 targetPosition, float duration)
        {
            this.position.MoveTo(targetPosition, duration);
        }

        public void Initialize() { /* do nothing */ }

        // Update the card's current draw position to be centered about its current position
        public void Update(GameTime gameTime)
        {
            if (!this.isDirty) { return; }

            Vector2 current = this.position.GetCurrent(gameTime);
            this.currentTranspose = new Rectangle(
                (int) Math.Round(current.X) - (int) Math.Round(scaledTextureDimensions.X / 2),
                (int) Math.Round(current.Y) - (int) Math.Round(scaledTextureDimensions.Y / 2), 
                (int) Math.Round(scaledTextureDimensions.X),
                (int) Math.Round(scaledTextureDimensions.Y));

            isDirty = false;
        }

        // Draws card centered about its position
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.currentTranspose, 
                this.baseTranspose, 
                Color.White);
        }
    }
}
