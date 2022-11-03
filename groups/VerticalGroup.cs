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

		public VerticalGroup(Text t) : base(t) {

		}

		protected internal override List<MenuElement> GetElements() {
			return Elements;
		}

		protected internal override void Update(MouseInputManager m, Camera c, MenuGroup top) {
			int width = 0, height = 0;
			foreach (var e in Elements) {
				e.FirstUpdate(top);
				var size = e.GetSize();
				if (size.X > width) width = size.X;
				if (size.Y > height) height = size.Y;
			}

			if (Bounds.Width > 0) width = Bounds.Width;
			if (Bounds.Height > 0) height = Bounds.Height;
			Point origin = Bounds.Location;

			foreach (var e in Elements) {
				e.SecondUpdate(top, origin, new Point(width, height));
				origin.Y += e.GetSize().Y;
			}
			foreach (var e in Elements) {
				e.ThirdUpdate(top, c, m);
			}
		}
	}
}
