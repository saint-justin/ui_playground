using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_SandboxTest.Options;

namespace MonoGame_SandboxTest.Utilities
{
    static class DebugManager
    {
        public static bool isEnabled { get; set; } = true;
        private static List<string> messageQueue;
        private static int borderPadding = 16;
        private static int linePadding = 24;
        private static Vector2 baseDrawPos;
        private static SpriteFont debugFont;

        public static void Init(ContentManager contentManager)
        {
            messageQueue = new List<string>();
            baseDrawPos = new Vector2(borderPadding);
            debugFont = contentManager.Load<SpriteFont>("Fonts/Debug");
            debugFont.Spacing = 1.5f;
        }

        // TODO: Fix double-writing bug when another window is selected
        public static void Write(string message)
        {
            messageQueue.Add(message);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!isEnabled)
            {
                messageQueue.Clear();
                return;
            };

            spriteBatch.Begin();

            // Draw all string content
            for (int i = 0; i < messageQueue.Count; i++)
            {
                spriteBatch.DrawString(debugFont, messageQueue[i], new Vector2(baseDrawPos.X, baseDrawPos.Y + (i * linePadding)), Color.White);
            }
            messageQueue.Clear();

            // TODO: Draw lines/rects/simple shapes

            spriteBatch.End();

        }
    }
}
