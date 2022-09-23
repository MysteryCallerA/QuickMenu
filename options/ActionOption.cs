using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.options {

	public class ActionOption:MenuOption {

		public Action Action;

		public ActionOption(string text, Action a):base(text) {
			Action = a;
		}

		public override void Select(MenuManager m) {
			Action.Invoke();
		}
	}
}
