using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu {

	public class ConstraintManager:List<IMenuConstraint> {

		public ConstraintManager() {
		}

		public void Update() {
			for (int i = 0; i < Count; i++) {
				this[i].Update();
			}
		}
	
	}
}
