using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.style;

namespace QuickMenu {
	
	public abstract class MenuManager {

		public Point Position = Point.Zero;
		public Style Style;

		public MenuManager(Style s) {
			Style = s;
		}

		public virtual void PressUp() {
		}

		public virtual void PressDown() {
		}

		public virtual void PressLeft() {
		}

		public virtual void PressRight() {
		}

		public virtual void PressConfirm() {
		}

		public virtual void PressCancel() {
		}

		public virtual void PushSubmenu(MenuLayer m) {
		}

		public abstract void Draw(SpriteBatch s, Camera c);

	}
}
