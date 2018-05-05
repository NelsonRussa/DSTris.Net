using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DSTris.View
{
    interface IRender
    {
        IEnumerable<Drawable> DrawObjects { get; }
    }
}
