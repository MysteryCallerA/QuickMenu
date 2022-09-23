using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.style;

namespace QuickMenu {
	
	public class TreeMenu:MenuManager {

		public MenuLayer TopLayer;

		public Stack<MenuLayer> MenuStack = new Stack<MenuLayer>();

		public TreeMenu(MenuLayer top, Style s):base(s) {
			TopLayer = top;
		}

		public MenuLayer CurrentLayer {
			get {
				if (MenuStack.Count == 0) return TopLayer;
				return MenuStack.Peek();
			}
		}

		public override void PressCancel() {
			base.PressCancel();
			MenuStack.Pop();
		}

		public override void PushSubmenu(MenuLayer m) {
			base.PushSubmenu(m);
			MenuStack.Push(m);
		}

		public override void PressConfirm() {
			base.PressConfirm();
			var o = CurrentLayer.GetSelectedOption();
			if (o != null) {
				o.Select(this);
			}
		}

		public override void PressDown() {
			base.PressDown();
			CurrentLayer.PressDown();
		}

		public override void PressUp() {
			base.PressUp();
			CurrentLayer.PressUp();
		}

		public override void PressLeft() {
			base.PressLeft();
			CurrentLayer.PressLeft();
		}

		public override void PressRight() {
			base.PressRight();
			CurrentLayer.PressRight();
		}

		public override void Draw(SpriteBatch s, Camera c) {
			CurrentLayer.Draw(s, c, Position, Style);
		}
	}
}
