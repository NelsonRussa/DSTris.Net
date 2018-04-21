using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Keyboard;

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
        public Config Config;
        public string FullFontName { get { return Config.Game.FullFontName; }  }

        //
        public Game()
        {
        }

        // Inicializar o jogo
        public void Initialize()
        {
            Config = new Config();
            Config.Load();
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

                    case Keyboard.Key.Return:
                        State = GameState.Playing;
                        break;
                }
            }
            else if (State == GameState.Playing)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                        GameOver();
                        break;

                    case Key.Space:
                        State = GameState.Paused;
                        break;

                    case Key.Left:
                        // Mover bloco para a esquerda
                        break;

                    case Key.Right:
                        // Mover bloco para a direita
                        break;

                    case Key.Down:
                        // Fazer o bloco cair mais rapidamente
                        break;

                    case Key.Up:
                        // Rodar o bloco
                        break;
                }
            }
            else if (State == GameState.Paused)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                        GameOver();
                        break;

                    case Key.Space:
                        State = GameState.Playing;
                        break;
                }
            }
            else if (State == GameState.GameOver)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                    case Key.Return:
                    case Key.Space:
                        State = GameState.Menu;
                        break;
                }
            }

            // Debug
            Console.WriteLine($"Estado atual: {State}");
        }

        //
        public void Close()
        {
            State = GameState.ExitGame;
        }

        //
        public void Update()
        {
        }

        //
        private void GameOver()
        {
            State = GameState.GameOver;
        }
    }
}