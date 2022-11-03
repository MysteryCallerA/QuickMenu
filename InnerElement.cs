using Microsoft.Xna.Framework;
using QuickMenu.groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu
{
    public abstract class InnerElement:MenuElement {

		protected internal abstract void Draw(MenuGroup g, Renderer r, Camera c, Color color);

		protected internal abstract void Draw(MenuGroup g, Renderer r, Camera c, ColorTable t, ColorTable.State state);

	}
}
