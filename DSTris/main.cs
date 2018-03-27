using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTris.View;
using DSTris.Model;

namespace DSTris
{
    static class Program
    {
        //
        static Game game;

        static void Main()
        {
            //
            game = new Game();
            //
            var screen = new Screen(1024, 768, "DSTris - Projecto para LabDS");
            screen.OnKeyPressed += OnKeyPressed;
            screen.OnClosed += OnClosed;
            //
            while (game.State != Game.GameState.ExitGame)
            {

                //
                screen.Render();
            }
            screen.Close();
        }

        //
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            game.KeyPressed(e.Code);
        }

        //
        static void OnClosed(object sender, EventArgs e)
        {
            game.Close();
        }
    }
}
