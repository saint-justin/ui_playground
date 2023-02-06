using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame_SandboxTest.Options
{
    static class OptionsManager
    {
        // Graphics manager ref
        private static GraphicsDeviceManager graphics;

        // Denotes whether options need to be re-initialized based on if any changes have come in
        public static bool shouldUpdate { get; private set; }

        // Screen size controls (in pixels)
        public static int screenHeight { get; private set; }
        public static int screenWidth { get; private set; }


        // TODO: Explore actual constructors
        public static void Init(GraphicsDeviceManager graphics)
        {
            OptionsManager.graphics = graphics;

            shouldUpdate = false;
            screenHeight = 1080;
            screenWidth = 1920;

            UpdateOptions();
        }

        // Setters should include update flags
        public static void SetScreenHeight(int height) 
        {
            screenHeight = height;
            shouldUpdate = true;
        }
        
        public static void SetScreenWidth(int width)
        {
            screenWidth = width;
            shouldUpdate = true;
        }

        public static void UpdateOptions()
        {
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            shouldUpdate = false;
        }
    }
}
