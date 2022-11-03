using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuickMenu.elements {
	public class BackButton:MenuButton {

		public BackButton(InnerElement e) : base(e) {
		}

		public BackButton(InnerElement e, ColorTable c) : base(e, c) {
		}

		protected internal override void Press(MenuGroup g) {
			base.Press(g);
			g.BackPage();
		}

	}
}
