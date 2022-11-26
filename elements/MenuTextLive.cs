using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.elements {

	public class MenuTextLive:MenuText {

		protected Func<string> Source;

		public MenuTextLive(Func<string> source) {
			Source = source;
		}

		protected internal override void FirstUpdate(MenuGroup g) {
			var newcontent = Source.Invoke();
			if (newcontent != Content) Content = newcontent;

			base.FirstUpdate(g);
		}

	}
}
