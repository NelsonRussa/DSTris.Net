using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.Model
{
    interface IRender
    {
        IEnumerable<Drawable> DrawObjects{ get; }
    }
}
