namespace MonoGame_SandboxTest.CardRenderSystem
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Card : IEntity
    {
        private Texture2D texture;
        private Rectangle baseTranspose;
        private Rectangle currentTranspose;

        public Vector2 ScaledTextureDimensions { get; private set; }

        public PositionTracker Position { get; private set; }

        public Vector2 Scale { get; set; }

        public bool Dirty { get; private set; }

        public Card(Texture2D texture, Vector2 position, Vector2 scale)
        {
            this.texture = texture;
            this.Position = new PositionTracker(position);
            this.Scale = scale;

            this.baseTranspose = new Rectangle(0, 0, texture.Width, texture.Height);
            this.ScaledTextureDimensions = new Vector2(texture.Width * scale.X, texture.Height * scale.Y);
        }

        public void SetPosition(Vector2 position)
        {
            this.Position = new PositionTracker(position);
            this.Dirty = true;
        }

        public void Initialize() { /* do nothing */ }

        // Update the card's current draw position to be centered about its current position
        public void Update(GameTime gameTime)
        {
            if (!this.Dirty && !this.Position.Moving) { return; }

            Vector2 current = this.Position.GetCurrent(gameTime);
            this.currentTranspose = new Rectangle(
                (int)Math.Round(current.X) - (int)Math.Round(this.ScaledTextureDimensions.X / 2),
                (int)Math.Round(current.Y) - (int)Math.Round(this.ScaledTextureDimensions.Y / 2),
                (int)Math.Round(this.ScaledTextureDimensions.X),
                (int)Math.Round(this.ScaledTextureDimensions.Y));

            this.Dirty = false;
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
