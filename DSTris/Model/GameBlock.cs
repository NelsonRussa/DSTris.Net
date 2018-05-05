using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSTris.Model
{
    class GameBlock
    {
        public List<GameBlockPart> Parts { get; set; } = null;
        private Vector2f _screenPosition;
        public Vector2f ScreenPosition
        {
            get
            {
                return _screenPosition;
            }

            set
            {
                _screenPosition = value;
                if (Parts != null)
                {
                    foreach (var gameBlockPart in Parts)
                        gameBlockPart.Move(_screenPosition);
                }
            }
        }
        public Vector2i GridPosition { get; set; }
        public bool Visible { get; set; } = true;
        public Vector2i Size { get; set; }
        private Texture Texture;

        //
        public GameBlock(Texture texture, List<Vector2i> PartPositions)
        {
            // Inicializar propriedades do novo bloco
            ScreenPosition = new Vector2f(0, 0);
            GridPosition = new Vector2i(0, 0);
            Texture = texture;

            // Definir as várias partes que compoem o bloco
            Parts = new List<GameBlockPart>();
            foreach (var partPosition in PartPositions)
            {
                GameBlockPart gameBlockPart = new GameBlockPart(Texture, partPosition);
                gameBlockPart.Move(ScreenPosition);
                Parts.Add(gameBlockPart);
            }

            //
            UpdateSize();
        }

        // Atribuir novas partes ao bloco
        // (apos rodar, caso fique numa posicao valida)
        public void SetNewParts(List<GameBlockPart> newParts)
        {
            Parts = newParts;
            //
            foreach (var part in Parts)
                part.UpdatePosition();
            UpdateSize();
        }

        // Manter o tamanho do bloco atualizado
        // Necessário para calcular corretamente a rotação
        private void UpdateSize()
        {
            int maxX = -1;
            int maxY = -1;
            foreach (var part in Parts)
            {
                if (part.Position.X > maxX)
                    maxX = part.Position.X;
                if (part.Position.Y > maxY)
                    maxY = part.Position.Y;
            }

            //
            Size = new Vector2i(maxX + 1, maxY + 1);
        }
    }
}
