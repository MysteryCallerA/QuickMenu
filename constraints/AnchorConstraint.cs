using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu.constraints {
	
	public class AnchorConstraint:IMenuConstraint {

		public MenuGroup Floater;

		public MenuGroup Anchor;
		public BorderPosition AnchorPosition = BorderPosition.TopLeft;
		public Point AnchorOffset = new Point();

		public AnchorConstraint(MenuGroup floater, MenuGroup anchor) {
			Floater = floater;
			Anchor = anchor;
		}

		public void Update() {
			Floater.Position = Anchor.Position + Anchor.GetBorderOffset(AnchorPosition) + AnchorOffset;
		}
	
	}
}
