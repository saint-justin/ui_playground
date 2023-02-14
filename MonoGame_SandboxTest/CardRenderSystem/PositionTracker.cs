namespace MonoGame_SandboxTest.CardRenderSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    internal enum InterpolationType
    {
        linear,
        easeIn,
        easeOut,
        easeInAndOut,
    }

    internal class PositionTracker
    {
        // Internal movement trackers
        private Vector2 positionStart;
        private Vector2 positionEnd;
        private double durationCurrent;
        private double durationTarget;

        public bool Moving { get; private set; }

        public InterpolationType Interpolation { get; set; }

        public Vector2 PositionCurrent { private get; set; }

        public PositionTracker(Vector2 position)
        {
            this.PositionCurrent = position;
        }

        // Move from current position to new position over a defined duration (in seconds)
        public void MoveTo(Vector2 position, float duration)
        {
            this.Moving = true;
            this.Interpolation = InterpolationType.linear;

            this.positionStart = this.PositionCurrent;
            this.positionEnd = position;

            this.durationCurrent = 0.0f;
            this.durationTarget = duration;
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
            if (!this.Moving) { return this.PositionCurrent; }

            // Check if this is the last tick of movement
            this.durationCurrent += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.durationCurrent >= this.durationTarget)
            {
                this.durationCurrent = this.durationTarget;
                this.Moving = false;
            }

            // Calculate new position
            switch (this.Interpolation)
            {
                case InterpolationType.linear:
                    this.PositionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, (float)(this.durationCurrent / this.durationTarget));
                    break;

                default:
                    throw new Exception("Interpolation method not implemented");
            }

            return this.PositionCurrent;
        }
    }
}
