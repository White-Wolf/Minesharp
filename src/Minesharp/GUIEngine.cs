using System;
using System.Collections.Generic;
using System.Text;
using AssortedWidgets;
using AssortedWidgets.Events;
using AssortedWidgets.Managers;
using AssortedWidgets.Test;
using AssortedWidgets.Themes;
using AssortedWidgets.Util;
using AssortedWidgets.Widgets;

namespace Minesharp
{
    /*
     * All of the documentation and software included in the AssortedWidgets Releases is copyrighted by BillConan Studio.
     * 
     * Copyright 2007. All rights reserved.
     */
    public static class GUIEngine
    {
        public Stack<GUI> _currentGUI = new Stack<GUI>();

        public void Draw()
        {
            if (_currentGUI.Count > 0)
                _currentGUI.Peek().Draw();
        }
        public void MouseMovement(int mx, int my)
        {
            if (_currentGUI.Count > 0)
                _currentGUI.Peek().MouseMovement(mx, my);
        }
        public void Resize(uint width, uint height)
        {
            if (_currentGUI.Count > 0)
                _currentGUI.Peek().Resize(width, height);
        }
        public void Pop()
        {
            if (_currentGUI.Count > 0)
            {
                _currentGUI.Peek().Unload();
                _currentGUI.Pop();
            }
        }
        public void Push(GUI newGUI, bool popOld)
        {
            if (popOld)
                _currentGUI.Pop();
            Push(newGUI);
        }
        public void Push(GUI newGUI)
        {
            newGUI.Init();
            _currentGUI.Push(newGUI);
        }
    }
}
