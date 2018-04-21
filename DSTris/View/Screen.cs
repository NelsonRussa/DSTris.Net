using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using DSTris.Model;
using SFML.System;

namespace DSTris.View
{
    class Screen
    {
        // Criar evento para avisar os subscritores quando uma tecla é pressionada
        public event EventHandler<KeyEventArgs> OnKeyPressed = null;
        // ou quando é pedido para fechar a janela
        public event EventHandler OnClosed = null;
        //
        private const bool SHOW_FPS = true;
        private RenderWindow renderWindow;
        private Clock clock;
        private int frames = 0;
        private Text txtFPS;

        // Inicializar ecran onde vai ser mostrado o jogo
        public Screen(uint width, uint height, string title, string fullFontName)
        {
            renderWindow = new RenderWindow(new VideoMode(width, height), title);
            renderWindow.SetActive(true);
            
            // Subscrever o evento do objecto da janela, para as teclas pressionadas
            // ou quando a janela é fechada
            renderWindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressedEvent);
            renderWindow.Closed += new EventHandler(OnClosedEvent);
            //
            clock = new Clock();

            txtFPS = new Text("", new Font(fullFontName));
            txtFPS.CharacterSize = 20;
            txtFPS.Color = new Color(Color.White);
            txtFPS.Position = new Vector2f(0, 0);
        }

        // Mostrar estado atual do jogo
        public void Render(Game game)
        {
            renderWindow.Clear(Color.Black);
            renderWindow.DispatchEvents();

            // Desenhar os objectos do jogo


            //
            if (SHOW_FPS)
            {
                frames++;
                if (clock.ElapsedTime.AsSeconds() > 1)
                {
                    var time = clock.Restart().AsSeconds();
                    int fps = (int)(frames / time);
                    frames = 0;

                    txtFPS.DisplayedString = $"{fps} FPS";
                }
                renderWindow.Draw(txtFPS);
            }

            renderWindow.Display();
        }

        // Método usado quando é necessário fechar a janela do jogo
        public void Close()
        {
            renderWindow.Close();
        }

        
        // Método chamado quando é desplotado o evento de tecla pressionada na janela
        private void OnKeyPressedEvent(object sender, KeyEventArgs e)
        {
            // Se está definido algum método para chamar, chama agora
            // passando o objecto que originou o evento e a tecla pressionada
            OnKeyPressed?.Invoke(sender, e);
        }

        // Método chamado quando é desplotado o evento de fechar a janela do jogo
        private void OnClosedEvent(object sender, EventArgs e)
        {
            // Se está definido algum método para chamar, chama agora 
            // passando o objecto que originou o eento e os parametros
            OnClosed?.Invoke(sender, e);
        }
    }
}
