using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.Model
{
    class Game
    {
        //
        public enum GameState
        {
            Menu,
            Playing,
            Paused,
            GameOver,
            ExitGame
        }
        public GameState State { get; set; } = GameState.Menu;

        //
        public Game()
        {

        }

        //
        public void KeyPressed(Keyboard.Key keyCode)
        {
            //
            if (State == GameState.Menu)
            {
                switch (keyCode)
                {
                    case Keyboard.Key.Escape:
                        State = GameState.ExitGame;
                        break;
                }
            }
        }

        //
        public void Close()
        {
            State = GameState.ExitGame;
        }
    }
}
