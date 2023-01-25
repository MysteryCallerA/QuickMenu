using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.constraints {

	/// <summary>
	/// Anchor a MenuGroup to two other MenuGroups.
	/// </summary>
	public class DoubleAnchorConstraint:MenuConstraint {

		public MenuGroup Floater;
		public MenuGroup HAnchor;
		public MenuGroup VAnchor;
		public BorderPosition HAnchorPosition = BorderPosition.TopLeft;
		public BorderPosition VAnchorPosition = BorderPosition.TopLeft;
		public Point AnchorOffset = new Point();
		public bool AnchorToScreen = true;

		public DoubleAnchorConstraint(MenuGroup floater, MenuGroup hAnchor, MenuGroup vAnchor) {
			Floater = floater;
			HAnchor = hAnchor;
			VAnchor = vAnchor;
		}

		public DoubleAnchorConstraint(MenuGroup floater) {
			Floater = floater;
		}

		public override void Update(Camera c) {
			var newpos = Floater.Position;

			if (HAnchor == null) {
				if (AnchorToScreen) {
					var screen = c.GetGameBounds();
					newpos.X = (MenuGroup.GetBorderOffset(HAnchorPosition, screen.Size) + AnchorOffset).X;
					newpos.X -= Floater.GetBorderOffset(HAnchorPosition).X;
				}
			} else {
				newpos.X = (HAnchor.Position + HAnchor.GetBorderOffset(HAnchorPosition) + AnchorOffset).X;
			}

			if (VAnchor == null) {
				if (AnchorToScreen) {
					var screen = c.GetGameBounds();
					newpos.Y = (MenuGroup.GetBorderOffset(VAnchorPosition, screen.Size) + AnchorOffset).Y;
					newpos.Y -= Floater.GetBorderOffset(VAnchorPosition).Y;
				}
			} else {
				newpos.Y = (VAnchor.Position + VAnchor.GetBorderOffset(VAnchorPosition) + AnchorOffset).Y;
			}

			Floater.Position = newpos;
			Floater.SecondUpdate(c);
		}
	}
}
