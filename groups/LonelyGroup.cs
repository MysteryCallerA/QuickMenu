using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu.groups {

	public class LonelyGroup:UnpagedMenuGroup {

		public MenuElement Content;

		public LonelyGroup(Font f) : base(f) {
		}

		public LonelyGroup(Font f, MenuElement content):base(f) {
			Content = content;
		}

		protected internal override List<MenuElement> GetElements() {
			return new List<MenuElement>() { Content };
		}

		protected internal override void FirstUpdate(Camera c, MenuGroup top) {
			Content.FirstUpdate(top);
		}

		protected internal override void SecondUpdate(Camera c, MenuGroup top) {
			Content.SecondUpdate(top, Bounds.Location, Bounds.Size);
		}

		protected internal override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			Content.ThirdUpdate(top, c, m);
		}
	}
}
