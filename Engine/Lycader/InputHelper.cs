using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader
{
    using OpenTK;
    using OpenTK.Input;
    using System.Drawing;

    public class InputHelper
    {
        private static KeyboardState prevKeyState = Keyboard.GetState();
        private static KeyboardState currentKeyState = Keyboard.GetState();
        private static MouseState prevMouseState = Mouse.GetState();
        private static MouseState currentMouseState = Mouse.GetState();

        public static void Update()
        {
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        //
        // Keyboard Events
        //

        public static bool IsKeyDown(Key key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Key key)
        {
            return currentKeyState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Key key)
        {
            return IsKeyDown(key) && prevKeyState.IsKeyUp(key);
        }

        public static bool KeyReleased(Key key)
        {
            return IsKeyUp(key) && prevKeyState.IsKeyDown(key);
        }

        //
        // Mouse Left Events
        //

        public static bool MouseLeftDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool MouseLeftUp()
        {
            return currentMouseState.LeftButton == ButtonState.Released;
        }

        public static bool MouseLeftTriggered()
        {
            return MouseLeftDown() && prevMouseState.LeftButton == ButtonState.Released;
        }

        public static bool MouseLeftReleased()
        {
            return MouseLeftUp() && prevMouseState.LeftButton == ButtonState.Pressed;
        }

        //
        // Mouse Right Events
        //

        public static bool MouseRightDown()
        {
            return currentMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool MouseRightUp()
        {
            return currentMouseState.RightButton == ButtonState.Released;
        }

        public static bool MouseRightTriggered()
        {
            return MouseRightDown() && prevMouseState.RightButton == ButtonState.Released;
        }

        public static bool MouseRightReleased()
        {
            return MouseRightUp() && prevMouseState.RightButton == ButtonState.Pressed;
        }

        //
        // Mouse Middle Events
        //

        public static bool MouseMiddleDown()
        {
            return currentMouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool MouseMiddleUp()
        {
            return currentMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool MouseMiddleTriggered()
        {
            return MouseMiddleDown() && prevMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool MouseMiddleReleased()
        {
            return MouseMiddleUp() && prevMouseState.MiddleButton == ButtonState.Pressed;
        }

        public static void CenterMouse(Rectangle bounds)
        {
            System.Windows.Forms.Cursor.Position = new Point(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
        }

        //
        // Public Mouse Variables
        //

        public static int GetMouseX()
        {
            return currentMouseState.X;
        }

        public static int GetMouseY()
        {
            return currentMouseState.Y;
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(GetMouseX(), GetMouseY());
        }

        public static int GetMouseLastX()
        {
            return prevMouseState.X;
        }

        public static int GetMouseLastY()
        {
            return prevMouseState.Y;
        }

        public static Vector2 GetMouseLastPosition()
        {
            return new Vector2(GetMouseLastX(), GetMouseLastY());
        }

        public static int GetMouseScrolledX()
        {
            return GetMouseX() - GetMouseLastX();
        }

        public static int GetMouseScrolledY()
        {
            return GetMouseY() - GetMouseLastY();
        }

        public static Vector2 GetMouseScroll()
        {
            return new Vector2(GetMouseScrolledX(), GetMouseScrolledY());
        }

        public static int GetMouseWheelScroll()
        {
            return currentMouseState.ScrollWheelValue;
        }
    }
}
