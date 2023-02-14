namespace MonoGame_SandboxTest.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame_SandboxTest.Options;

    /// <summary>
    /// Static debug class that allows debug information to be printed on top of the game render.
    /// </summary>
    internal static class DebugManager
    {
        private static List<string> messageQueue;
        private static List<Line> lineQueue;

        private static int borderPadding = 16;
        private static int linePadding = 24;
        private static Vector2 baseDrawPos;

        private static SpriteFont debugFont;
        private static Texture2D debugPixel;

        // On/off switch
        public static bool Enabled { get; set; } = true;

        public static void Init(ContentManager contentManager)
        {
            messageQueue = new List<string>();
            lineQueue = new List<Line>();
            baseDrawPos = new Vector2(borderPadding);

            debugFont = contentManager.Load<SpriteFont>("Fonts/Debug");
            debugFont.Spacing = 1.5f;

            debugPixel = contentManager.Load<Texture2D>("debug_pixel");
        }

        /// <summary>
        /// Add message to be drawn to the screen this frame.
        /// </summary>
        /// <param name="message">Text that will be written to the screen.</param>
        public static void WriteLine(string message)
        {
            // TODO: Fix double-writing bug when FPS de-syncs from update calls
            messageQueue.Add(message);
        }

        /// <summary>
        /// Helper function to draw a line segment between two positions.
        /// </summary>
        /// <param name="start">Starting position where the line is drawn from.</param>
        /// <param name="end">Ending position where the line is drawn to.</param>
        /// <param name="thickness">Stroke thickness/width of the line.</param>
        /// <param name="color">Color of the line.</param>
        public static void DrawLine(Vector2 start, Vector2 end, float thickness, Color color)
        {
            float magnitude = Vector2.Distance(start, end);
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            lineQueue.Add(new Line(start, magnitude, angle, thickness, color));
        }

        /// <summary>
        /// Helper function to draw the outline of a shape represented as a series of lines.
        /// </summary>
        /// <param name="lines">List of tuples representing start/end positions of the line segments that make the shape.</param>
        /// <param name="thickness">Stroke thickness/width of the line.</param>
        /// <param name="color">Color of the shape.</param>
        public static void DrawShape(List<(Vector2, Vector2)> lines, float thickness, Color color)
        {
            lines.ForEach(line => DrawLine(line.Item1, line.Item2, thickness, color));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled)
            {
                messageQueue.Clear();
                return;
            }

            // Setup
            spriteBatch.Begin();

            // Draw each message from queue
            for (int i = 0; i < messageQueue.Count; i++)
            {
                spriteBatch.DrawString(debugFont, messageQueue[i], new Vector2(baseDrawPos.X, baseDrawPos.Y + (i * linePadding)), Color.White);
            }


            // Draw each line from queue
            lineQueue.ForEach(line =>
            {
                spriteBatch.Draw(
                    debugPixel,
                    line.Start,
                    null,
                    line.Color,
                    line.Angle,
                    Vector2.Zero,
                    new Vector2(line.Magnitude, line.Thickness),
                    SpriteEffects.None,
                    0);
            });

            // Cleanup
            messageQueue.Clear();
            lineQueue.Clear();
            spriteBatch.End();
        }
    }

    #pragma warning disable SA1201
    internal struct Line
    {
        public Vector2 Start { get; private set; }

        public float Magnitude { get; private set; }

        public float Angle { get; private set; }

        public float Thickness { get; private set; }

        public Color Color { get; private set; }

        public Line(Vector2 start, float magnitude, float angle, float thickness, Color color)
        {
            this.Start = start;
            this.Magnitude = magnitude;
            this.Angle = angle;
            this.Thickness = thickness;
            this.Color = color;
        }
    }
    #pragma warning restore SA1201

}
