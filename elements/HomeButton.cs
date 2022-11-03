using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.elements {
	public class HomeButton:MenuButton {

		public HomeButton(InnerElement e) : base(e) {
		}

		public HomeButton(InnerElement e, ColorTable c) : base(e, c) {
		}

		protected internal override void Press(MenuGroup g) {
			base.Press(g);
			g.HomePage();
		}

	}
}
