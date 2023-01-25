using Microsoft.Xna.Framework;
using Toybox;

namespace QuickMenu.constraints {

	/// <summary>
	/// Adjusts the size of target MenuGroup's size to match each other.
	/// </summary>
	public class MatchConstraint:MenuConstraint {

		public enum MatchType {
			MatchLargest, MatchFirst
		}

		public MatchType Type = MatchType.MatchLargest;
		public MenuGroup[] Targets;

		public MatchConstraint(params MenuGroup[] targets) {
			Targets = targets;
		}

		public override void Update(Camera c) {
			if (Type == MatchType.MatchLargest) {
				var size = GetLarge();
				foreach (var target in Targets) {
					target.ConstrainSize(size);
					target.SecondUpdate(c);
				}
				return;
			}

			if (Type == MatchType.MatchFirst) {
				var size = Targets[0].Size;
				for (int i = 1; i < Targets.Length; i++) {
					Targets[i].ConstrainSize(size);
					Targets[i].SecondUpdate(c);
				}
				return;
			}
		}

		private Point GetLarge() {
			int x = 0, y = 0;
			foreach (var target in Targets) {
				if (target.Size.X > x) x = target.Size.X;
				if (target.Size.Y > y) y = target.Size.Y;
			}
			return new Point(x, y);
		}
		
	}
}
