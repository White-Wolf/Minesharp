using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace Minesharp
{
    public static class ScreenManager
    {
        #region Properties
        private static Stack<Screen> _currentScreens = new Stack<Screen>();
        #endregion

        #region Functions
        public static void Draw()
        {
            if (_currentScreens.Count > 0)
                _currentScreens.Peek().Draw();
        }
        public static void InputController(MouseDevice md, KeyboardDevice kd)
        {
            if (_currentScreens.Count > 0)
                _currentScreens.Peek().Input_Controller(md, kd);
        }
        public static void Update(FrameEventArgs e)
        {
            if(_currentScreens.Count > 0)
                _currentScreens.Peek().Update(e);
        }
        public static void Pop()
        {
            if (_currentScreens.Count > 0)
            {
                _currentScreens.Peek().Unload();
                _currentScreens.Pop();
            }
        }
        public static void Unload()
        {
            foreach (Screen scrn in _currentScreens)
            {
                scrn.Unload();
            }
            _currentScreens.Clear();
        }
        public static void Push(Screen newScreen)
        {
            newScreen.Load();
            _currentScreens.Push(newScreen);
        }
        public static void Push(Screen newScreen, bool popOld)
        {
            if (popOld)
                Pop();
            Push(newScreen);
        }
        #endregion
    }
}
