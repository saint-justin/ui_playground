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
        Linear,
        EaseIn,
        EaseOut,
        EaseInAndOut,
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

        /// <summary>
        /// Move from current position to new position over a defined duration in seconds.
        /// </summary>
        /// <param name="position">Target position that to end movement at.</param>
        /// <param name="duration">Total duration of movement in seconds.</param>
        public void MoveTo(Vector2 position, float duration)
        {
            this.Moving = true;
            this.Interpolation = InterpolationType.Linear;

            this.positionStart = this.PositionCurrent;
            this.positionEnd = position;

            this.durationCurrent = 0.0f;
            this.durationTarget = duration;
        }

        /// <summary>
        /// Move from current position to new position over a defined duration in seconds by specific interpolation method.
        /// </summary>
        /// <param name="position">Target position that to end movement at.</param>
        /// <param name="duration">Total duration of movement in seconds.</param>
        /// <param name="interpolationType">Method of interpolation to use.</param>
        public void MoveTo(Vector2 position, float duration, InterpolationType interpolationType)
        {
            this.Interpolation = interpolationType;
            this.Moving = true;

            this.positionStart = this.PositionCurrent;
            this.positionEnd = position;

            this.durationCurrent = 0.0f;
            this.durationTarget = duration;
        }

        /// <summary>
        /// Get the current position of the object.
        /// </summary>
        /// <param name="gameTime">GameTime object from main</param>
        /// <returns>The current position of the object this is attached to.</returns>
        /// <exception cref="Exception">Thrown if interpolation method requested is not yet implemented.</exception>
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
            float basePercentage = (float)(this.durationCurrent / this.durationTarget);
            switch (this.Interpolation)
            {
                case InterpolationType.Linear:
                    this.PositionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, basePercentage);
                    break;

                case InterpolationType.EaseIn:
                    this.PositionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, (float)(1.0 - Math.Cos((basePercentage * Math.PI) / 2)));
                    break;

                case InterpolationType.EaseOut:
                    this.PositionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, (float)Math.Sin((basePercentage * Math.PI) / 2));
                    break;

                case InterpolationType.EaseInAndOut:
                    this.PositionCurrent = Vector2.Lerp(this.positionStart, this.positionEnd, (float)((Math.Cos(basePercentage * Math.PI) - 1) / -2f));
                    break;

                default:
                    throw new Exception("Interpolation method not implemented");
            }

            return this.PositionCurrent;
        }
    }
}
