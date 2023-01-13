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

	public class BasicGroup:UnpagedMenuGroup {

		public List<MenuElement> Elements = new List<MenuElement>();

		public BasicGroup(Font f):base(f) {
		}

		protected internal override List<MenuElement> GetElements() {
			return Elements;
		}

		protected override void FirstUpdate(Camera c, MenuGroup top) {
			foreach (var e in Elements) {
				e.FirstUpdate(top);
			}
		}

		protected override void SecondUpdate(Camera c, MenuGroup top) {
			foreach (var e in Elements) {
				e.SecondUpdate(top, Bounds.Location, Bounds.Size);
			}
		}

		protected override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			foreach (var e in Elements) {
				e.ThirdUpdate(top, c, m);
			}
		}
	}
}
