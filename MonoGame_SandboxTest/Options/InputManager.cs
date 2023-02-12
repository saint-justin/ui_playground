using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_SandboxTest.Options
{
    /// <summary>
    /// Class for managing Keyboard (and potentially Controller) inputs
    /// </summary>
    static class InputManager
    {
        private static KeyboardState previousState;
        private static KeyboardState currentState;

        public static void Init()
        {
            previousState = Keyboard.GetState();
            currentState = Keyboard.GetState();
        }

        public static void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        /// Returns whether a key has been pressed this frame and not the previous
        public static bool IsKeyDown(Keys key)
        {
            if (!currentState.GetPressedKeys().Contains(key)) { return false; }
            if (previousState.GetPressedKeys().Contains(key)) { return false; }
            return true;
        }
    }
}
