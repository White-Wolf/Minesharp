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
    public abstract class GUI
    {
        protected DefaultTheme theme;
        private uint width;
        private uint height;
        public GUI()
        {
        }
        public virtual void Init(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            theme = new DefaultTheme(width, height);
            theme.Setup("aw.png", "./Resources/GUI/aw.png");
        }
        public virtual void Draw()
        {
        }
        public virtual void Resize(uint width, uint height)
        {
            this.width = width;
            this.height = height;
        }
        public virtual void MouseMovement(int mx, int my)
        {
        }
    }
}
