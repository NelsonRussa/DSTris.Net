using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSTris.Model
{
    class Statistics: Drawable
    {
        private const int SizeW = 11;
        private const int SizeH = 2;

        private class Block
        {
            public int ID { get; set; }
            public RectangleShape Shape { get; set; }
            private int _counter;
            public int Counter
            {
                get
                {
                    return _counter;
                }
                set
                {
                    _counter = value;
                    Shape.Position = new Vector2f(Shape.Position.X, Shape.Position.Y - SizeH);
                    Shape.Size = new Vector2f(SizeW, _counter * SizeH);
                }
            }

            //
            public Block(int ID, Color statsColor)
            {
                this.ID = ID;
                Shape = new RectangleShape();
                Shape.FillColor = statsColor;
            }

            //
            public void Increment()
            {
                Counter++;
            }
        }
        //
        private Dictionary<int, Block> Blocks = new Dictionary<int, Block>();
        private Vector2f ScreenCoords;
        private float MaxY;

        //
        public Statistics(Vector2f screenCoords, float maxY)
        {
            ScreenCoords = screenCoords;
            maxY = MaxY;
        }

        // Guardar o ID do bloco, para mostrar as estatisticas
        public void Add(int blockID, Color statsColor)
        {
            if (Blocks.ContainsKey(blockID))
                Blocks[blockID].Increment();
            else
            {
                Block block = new Block(blockID, statsColor);
                block.Shape.Position = new Vector2f(ScreenCoords.X+blockID * SizeW, ScreenCoords.Y);
                block.Counter = 1;
                Blocks.Add(blockID, block);
            }
        }

        //
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var block in Blocks.Values)
                target.Draw(block.Shape);
        }
    }
}
