using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.View
{
    class Screen
    {
        public event EventHandler<KeyEventArgs> OnKeyPressed = null;
        public event EventHandler OnClosed = null;
        private RenderWindow renderWindow;

        //
        public Screen(uint width, uint height, string title)
        {
            renderWindow = new RenderWindow(new VideoMode(width, height), title);
            renderWindow.SetActive(true);
            //
            renderWindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressedEvent);
            renderWindow.Closed += new EventHandler(OnClosedEvent);
        }

        //
        public void Render()
        {
            renderWindow.Clear(Color.Black);
            renderWindow.DispatchEvents();

            renderWindow.Display();
        }

        //
        public void Close()
        {
            renderWindow.Close();
        }

        //
        private void OnKeyPressedEvent(object sender, KeyEventArgs e)
        {
            OnKeyPressed?.Invoke(sender, e);
        }

        //
        private void OnClosedEvent(object sender, EventArgs e)
        {
            OnClosed?.Invoke(sender, e);
        }
    }
}
