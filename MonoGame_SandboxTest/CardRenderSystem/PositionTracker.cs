namespace MonoGame_SandboxTest.CardRenderSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    enum InterpolationType
    {
        linear,
        easeIn,
        easeOut,
        easeInAndOut,
    }

    internal class PositionTracker
    {
        public bool isMoving { get; private set; }
        public InterpolationType interpolationType { get; set; }
        public Vector2 positionCurrent { private get; set; }

        // For movement
        private Vector2 positionStart;
        private Vector2 positionEnd;
        private double durationCurrent;
        private double durationTarget;

        public PositionTracker(Vector2 position)
        {
            this.positionCurrent = position;
        }

        // Move from current position to new position over a defined duration (in seconds)
        public void MoveTo(Vector2 position, float duration)
        {
            this.positionStart = this.positionCurrent;
            this.positionEnd = position;
            this.durationCurrent = 0.0f;
            this.durationTarget = duration;
            this.interpolationType = InterpolationType.linear;
        }

        /* 
        // Variant to allows alternative interpolation types
        public void MoveTo(Vector2 position, float duration, InterpolationType interpolationType)
        {
            this.currentTime = 0.0f;
            this.endPos = position;
            this.duration = duration;
            this.interpolationType = interpolationType;
        } 
        */

        // Only way the current position should be gotten
        public Vector2 GetCurrent(GameTime gameTime)
        {
            // Non-moving
            if (!this.isMoving) { return this.positionCurrent; }

            // Check if this is the last tick of movement
            this.durationCurrent += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.durationCurrent >= this.durationTarget)
            {
                this.durationCurrent = this.durationTarget;
                this.isMoving = false;
            }

            // Calculate new position
            switch (this.interpolationType)
            {   
                case InterpolationType.linear:
                    this.positionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, (float) this.durationCurrent);
                    break;

                default:
                    throw new Exception("Interpolation method not implemented");
            }

            return this.positionCurrent;
        }
    }
}
