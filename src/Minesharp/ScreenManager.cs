using System;
using System.Collections.Generic;
using System.Text;

namespace Minesharp
{
    public class ScreenManager
    {
        #region Properties
        private Stack<Screen> _currentScreens;
        #endregion

        #region Construct
        public ScreenManager()
        {
            _currentScreens = new Stack<Screen>();
        }
        #endregion
        #region Functions
        public void Draw()
        {
            if (_currentScreens.Count > 0)
                _currentScreens.Peek().Draw();
        }
        public void InputController() { }
        public void Update()
        {
            if(_currentScreens.Count > 0)
                _currentScreens.Peek().Update();
        }
        public void Pop()
        {
            if (_currentScreens.Count > 0)
            {
                _currentScreens.Peek().Unload();
                _currentScreens.Pop();
            }
        }
        public void Push(Screen newScreen)
        {
            newScreen.Load();
            _currentScreens.Push(newScreen);
        }
        #endregion
    }
}
