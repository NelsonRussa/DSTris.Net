/*
    21179 - Laboratório de Desenvolvimento de Software 
    Nelson Russa - 1401826
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTris.View;
using DSTris.Model;
using SFML.Window;

namespace DSTris
{
    static class Program
    {
        //
        static Game game;

        static void Main()
        {
            // Criar o objecto que vai conter toda a informação do jogo
            game = new Game();
            
            // Inicializar o ecran
            var screen = new Screen(1024, 768, "DSTris - Projecto para LabDS");
            // Subscrever aos inputs da view para teclas pressionadas e ao fechar 
            // a janela
            screen.OnKeyPressed += OnKeyPressed;
            screen.OnClosed += OnClosed;
            
            // Ciclo principal do jogo
            // Termina quando o estado do jogo for para sair
            while (game.State != Game.GameState.ExitGame)
            {
                // Atualizar o jogo
                game.Update();
                // Mostrar estado atual do jogo
                screen.Render(game);
            }

            // O estado do jogo indica que é para sair. Avisar a view
            // para fechar
            screen.Close();
        }

        // Método usado na subscrição do evento de tecla pressionada, 
        // gerado pela view. Indica que o utilizador pressionou uma tecla
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            // Passa a tecla pressionada para o model
            game.KeyPressed(e.Code);
        }

        // Método usado na subscrição do evento de fecho de janela,
        // gerado pela view. Indica que o utilizador fechou a janela de jogo
        static void OnClosed(object sender, EventArgs e)
        {
            // Indica ao model que vai fechar
            game.Close();
        }
    }
}
