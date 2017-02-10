//-----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System.Drawing;

    using OpenTK;
    using OpenTK.Input;

    public class InputManager
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

        public static bool IsKeyReleased(Key key)
        {
            return IsKeyUp(key) && prevKeyState.IsKeyDown(key);
        }

        public static bool IsMouseDown(MouseButton button)
        {
            return currentMouseState.IsButtonDown(button);
        }

        public static bool IsMouseUp(MouseButton button)
        {
            return currentMouseState.IsButtonUp(button);
        }

        public static bool IsMousePressed(MouseButton button)
        {
            return IsMouseDown(button) && prevMouseState.IsButtonUp(button);
        }

        public static bool IsMouseReleased(MouseButton button)
        {
            return IsMouseUp(button) && prevMouseState.IsButtonDown(button);
        }

        //
        // Mouse Middle Events
        //
        public static void CenterMouse(Rectangle bounds)
        {
            System.Windows.Forms.Cursor.Position = new Point(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
        }

        //
        // Public Mouse Variables
        //
        public static Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public static Vector2 GetMouseLastPosition()
        {
            return new Vector2(prevMouseState.X, prevMouseState.Y);
        }

        public static Vector2 GetMouseScrolled()
        {
            return GetMousePosition() - GetMouseLastPosition();
        }

        public static int GetMouseWheelScroll()
        {
            return currentMouseState.ScrollWheelValue;
        }
    }
}
