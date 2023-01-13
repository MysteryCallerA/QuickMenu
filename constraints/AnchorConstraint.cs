using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.constraints {
	
	public class AnchorConstraint:MenuConstraint {

		public MenuGroup Floater;

		public MenuGroup Anchor;
		public BorderPosition AnchorPosition = BorderPosition.TopLeft;
		public Point AnchorOffset = new Point();

		public AnchorConstraint(MenuGroup floater, MenuGroup anchor) {
			Floater = floater;
			Anchor = anchor;
		}

		public override void Update(Camera c) {
			Floater.Position = Anchor.Position + Anchor.GetBorderOffset(AnchorPosition) + AnchorOffset;
			Floater.SecondUpdate(c);
		}
	
	}
}
