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

		public BasicGroup(Text t):base(t) {
		}

		protected internal override void Update(MouseInputManager m, Camera c, MenuGroup top) {
			foreach (var e in Elements) {
				e.FirstUpdate(top);
			}
			foreach (var e in Elements) {
				e.SecondUpdate(top, Bounds.Location, Bounds.Size);
			}
			foreach (var e in Elements) {
				e.ThirdUpdate(top, c, m);
			}
		}

		protected internal override List<MenuElement> GetElements() {
			return Elements;
		}

	}
}
