using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu {

	public class ConstraintManager:List<MenuConstraint> {

		public ConstraintManager() {
		}

		//NEXT change this to a MenuGroupManager so you dont need to remember when to call your constraint manager.
		public void Update(Camera c) {
			for (int i = 0; i < Count; i++) {
				this[i].Update(c);
			}
		}
	
	}
}
