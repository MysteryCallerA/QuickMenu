using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu.groups {

	public class VerticalGroup:UnpagedMenuGroup {

		public List<MenuElement> Elements = new List<MenuElement>();

		public VerticalGroup(Font f) : base(f) {
		}

		protected internal override List<MenuElement> GetElements() {
			return Elements;
		}

		public int Width {
			get; private set;
		}

		protected internal override void FirstUpdate(Camera c, MenuGroup top) {
			int width = 0;
			foreach (var e in Elements) {
				e.FirstUpdate(top);
				var size = e.GetSize();
				if (size.X > width) width = size.X;
			}
			Width = width;
		}

		protected internal override void SecondUpdate(Camera c, MenuGroup top) {
			Point origin = Position;
			foreach (var e in Elements) {
				e.SecondUpdate(top, origin, new Point(Width, e.GetSize().Y));
				origin.Y += e.GetSize().Y;
			}
		}

		protected internal override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			foreach (var e in Elements) {
				e.ThirdUpdate(top, c, m);
			}
		}
	}
}
