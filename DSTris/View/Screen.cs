using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using DSTris.Model;

namespace DSTris.View
{
    class Screen
    {
        // Criar evento para avisar os subscritores quando uma tecla é pressionada
        public event EventHandler<KeyEventArgs> OnKeyPressed = null;
        // ou quando é pedido para fechar a janela
        public event EventHandler OnClosed = null;
        //
        private RenderWindow renderWindow;

        // Inicializar ecran onde vai ser mostrado o jogo
        public Screen(uint width, uint height, string title)
        {
            renderWindow = new RenderWindow(new VideoMode(width, height), title);
            renderWindow.SetActive(true);
            
            // Subscrever o evento do objecto da janela, para as teclas pressionadas
            // ou quando a janela é fechada
            renderWindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressedEvent);
            renderWindow.Closed += new EventHandler(OnClosedEvent);
        }

        // Mostrar estado atual do jogo
        public void Render(Game game)
        {
            renderWindow.Clear(Color.Black);
            renderWindow.DispatchEvents();

            // Desenhar os objectos do jogo


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
