using SFML.Graphics;
using SFML.System;
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
        public string FontName { get { return Config.Game.FontName; }  }
        private Config Config;
        public Sprite BackgroundMenu { get; set; }
        public Sprite BackgroundGame { get; set; }
        public Text txtGameOver;
        public Text txtPaused;

        //
        public Game()
        {
        }

        // Inicializar o jogo
        public void Initialize()
        {
            Config = new Config();
            Config.Load();

            //
            BackgroundMenu = new Sprite(new Texture(Config.Menu.Background.TextureName));
            BackgroundGame = new Sprite(new Texture(Config.Game.Background.TextureName));
            //
            txtGameOver = new Text("Game Over", new Font(FontName));
            txtGameOver.CharacterSize = 50;
            txtGameOver.Style = Text.Styles.Bold;
            txtGameOver.Color = new Color(Color.Red);
            txtGameOver.Position = new Vector2f(400, 300);
            //
            txtPaused = new Text("P A U S E", new Font(FontName));
            txtPaused.CharacterSize = 50;
            txtPaused.Style = Text.Styles.Bold;
            txtPaused.Color = new Color(Color.White);
            txtPaused.Position = new Vector2f(425, 300);
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