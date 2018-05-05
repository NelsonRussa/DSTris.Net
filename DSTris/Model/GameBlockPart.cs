using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSTris.Model
{
    class GameBlockPart
    {
        public Sprite Sprite { get; set; }
        public Vector2i Position { get; set; }
        public Texture Texture { get; set; }
        private Vector2f RelativePosition;

        // 
        public GameBlockPart(Texture texture, Vector2i position)
        {
            Position = position;
            Texture = texture;
            Sprite = new Sprite(texture);
            RelativePosition = new Vector2f(Position.X * Texture.Size.X, Position.Y * Texture.Size.Y);
        }

        // Criar uma nova parte do bloco, copiando propriedades de outra parte
        public GameBlockPart(GameBlockPart gameBlockPart)
        {
            Position = gameBlockPart.Position;
            Texture = gameBlockPart.Texture;
            Sprite = new Sprite(gameBlockPart.Texture);
            RelativePosition = new Vector2f(Position.X * Texture.Size.X, Position.Y * Texture.Size.Y);
        }

        // Quando é necessário atualizar a posição relativa de cada parte
        // Refere-se às coordenadas de cada parte em relação à posição do bloco
        public void UpdatePosition()
        {
            RelativePosition = new Vector2f(Position.X * Texture.Size.X, Position.Y * Texture.Size.Y);
        }

        // Quando o bloco principal move, atualiza a posição do sprite
        public void Move(Vector2f parentPosition)
        {
            Sprite.Position = new Vector2f(parentPosition.X + RelativePosition.X, parentPosition.Y + RelativePosition.Y);
        }
    }
}
