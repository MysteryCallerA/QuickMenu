using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.constraints {

	/// <summary>
	/// Fits the target MenuGroup to the size of the container.
	/// </summary>
	public class FitConstraint:MenuConstraint {

		public MenuGroup Target;
		public Func<Rectangle> GetContainer;
		public bool FitRight = false;
		public bool FitDown = false;
		public int Margins = 0;

		public FitConstraint(MenuGroup target, Func<Rectangle> getContainer) {
			Target = target;
			GetContainer = getContainer;
		}

		public override void Update(Camera c) {
			var container = GetContainer.Invoke();
			container.Inflate(-Margins, -Margins);
			Rectangle newsize = Target.Bounds;

			if (FitRight) {
				newsize.Width += container.Right - newsize.Right;
			}
			if (FitDown) {
				newsize.Height += container.Bottom - newsize.Bottom;
			}

			if (Target.Size != newsize.Size) {
				Target.ConstrainSize(newsize.Size);
				Target.SecondUpdate(c);
			}
		}
	}
}
