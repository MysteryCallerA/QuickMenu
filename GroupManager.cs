using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.input;

namespace QuickMenu {
	
	public class GroupManager {

		public List<MenuConstraint> Constraints = new List<MenuConstraint>();
		public List<MenuGroup> Groups = new List<MenuGroup>();

		public GroupManager() {
		}

		public void Update(Camera c, MouseInputManager m) {
			foreach (var g in Groups) {
				g.FirstUpdate(c);
				g.SecondUpdate(c);
			}

			foreach (var con in Constraints) {
				con.Update(c);
			}

			foreach (var g in Groups) {
				g.ThirdUpdate(m, c);
			}
		}

		public void Draw(Renderer r, Camera c) {
			foreach (var g in Groups) {
				g.Draw(r, c);
			}
		}

	}
}
