using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.constraints {
	
	/// <summary>
	/// Anchor a MenuGroup to the screen or another MenuGroup. The Floater's position is updated to be relative to the Anchor.
	/// </summary>
	public class AnchorConstraint:MenuConstraint {

		public MenuGroup Floater;

		public MenuGroup Anchor;
		public BorderPosition AnchorPosition = BorderPosition.TopLeft;
		public Point AnchorOffset = new Point();

		/// <summary> Anchor to another MenuGroup. </summary>
		public AnchorConstraint(MenuGroup floater, MenuGroup anchor) {
			Floater = floater;
			Anchor = anchor;
		}

		/// <summary> Anchor to the screen. </summary>
		public AnchorConstraint(MenuGroup floater) {
			Floater = floater;
		}

		public override void Update(Camera c) {
			if (Anchor == null) {
				var screen = c.GetGameBounds();
				Floater.Position = MenuGroup.GetBorderOffset(AnchorPosition, screen.Size) + AnchorOffset;
				Floater.Position -= Floater.GetBorderOffset(AnchorPosition);
				Floater.SecondUpdate(c);
				return;
			}
			Floater.Position = Anchor.Position + Anchor.GetBorderOffset(AnchorPosition) + AnchorOffset;
			Floater.SecondUpdate(c);
		}
	
	}
}
