using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame_SandboxTest.CardRenderSystem
{
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
            this.positionStart = positionCurrent;
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
            if (!isMoving) { return positionCurrent; }

            // Check if this is the last tick of movement
            durationCurrent += gameTime.ElapsedGameTime.TotalSeconds;
            if (durationCurrent >= durationTarget)
            {
                durationCurrent = durationTarget;
                isMoving = false;
            }

            // Calculate new position
            switch (interpolationType)
            {   
                case InterpolationType.linear:
                    positionCurrent = Vector2.Lerp(positionStart, positionEnd, (float) durationCurrent);
                    break;

                default:
                    throw new Exception("Interpolation method not implemented");
            }

            return positionCurrent;
        }
    }
}
