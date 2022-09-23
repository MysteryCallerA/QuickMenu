using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.options {

	public class SubmenuOption:MenuOption {

		public MenuLayer Submenu;

		public SubmenuOption(string text, MenuLayer sub):base(text) {
			Submenu = sub;
		}

		public override void Select(MenuManager m) {
			m.PushSubmenu(Submenu);
		}
	}
}
