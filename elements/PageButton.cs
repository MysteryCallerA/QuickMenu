using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.elements {
	public class PageButton:MenuButton {

		public int PageId;

		public PageButton(int pageid, InnerElement e):base(e) {
			PageId = pageid;
		}

		public PageButton(int pageid, InnerElement e, ColorTable c):base(e, c) {
			PageId = pageid;
		}

		protected internal override void Press(MenuGroup g) {
			base.Press(g);
			g.SwitchPage(PageId);
		}

	}
}
