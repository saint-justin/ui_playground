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

        private Vector2 position;
        private Vector2 scale;

        private Rectangle baseDrawPosition;
        private Rectangle currentDrawPosition;

        public Card(Texture2D texture, Vector2 position, Vector2 scale)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;

            this.baseDrawPosition = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Initialize() {}

        public void Update(GameTime gameTime)
        {
            this.currentDrawPosition = new Rectangle(
                baseDrawPosition.X + (int) Math.Round(position.X),
                baseDrawPosition.Y + (int) Math.Round(position.Y), 
                (int) Math.Round(texture.Width * scale.X),
                (int) Math.Round(texture.Height * scale.Y));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.currentDrawPosition, 
                this.baseDrawPosition, 
                Color.White);
        }
    }
}
