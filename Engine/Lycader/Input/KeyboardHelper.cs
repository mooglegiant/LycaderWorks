using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Input
{
    using OpenTK.Input;

    public static class KeyboardHelper
    {
        static List<Key> keyboardState;
        static List<Key> lastKeyboardState;

        static KeyboardHelper()
        {
            keyboardState = new List<Key>();
            lastKeyboardState = new List<Key>();
        }

        public static void Poll()
        {            
            lastKeyboardState = keyboardState;  
        }

        internal static void AddKeyPress(Key key)
        {
            keyboardState.Add(key);
        }

        internal static void RemoveKeyPress(Key key)
        {
            keyboardState.RemoveAll(k => k == key);
        }

        public static bool IsKeyPressed(Key key)
        {
            return (keyboardState.Contains(key) && (keyboardState.Contains(key) != lastKeyboardState.Contains(key)));
        }

        public static bool IsKeyReleased(Key key)
        {
            return (!keyboardState.Contains(key) && (keyboardState.Contains(key) != lastKeyboardState.Contains(key)));
        }

        public static bool IsKeyHeld(Key key)
        {
            return keyboardState.Contains(key);
        }
    }
}
