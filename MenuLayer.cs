using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Toybox;
using Utils.style;
using Utils.text;

namespace QuickMenu {

	public abstract class MenuLayer {

		public Rectangle Bounds;
		public Point Position;
		public bool Autosize = true;

		public MenuLayer() {

		}

		public virtual void PressUp() {
		}

		public virtual void PressDown() {
		}

		public virtual void PressLeft() {
		}

		public virtual void PressRight() {
		}

		public abstract MenuOption GetSelectedOption();

		public abstract void Draw(Renderer r, Camera c, Point origin, Style style, Text t);

		public abstract void Update(Style style, Text t);
	
	}
}
