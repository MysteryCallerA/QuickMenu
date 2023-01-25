using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.constraints {
	
	/// <summary>
	/// Centers multiple MenuGroups within a container rectangle. Relative positions of MenuGroups are preserved.
	/// </summary>
	public class CenterConstraint:MenuConstraint {

		public Func<Rectangle> GetContainer;
		public List<MenuGroup> Targets = new List<MenuGroup>();
		public float? VerticalCenterRatio = null;
		public float? HorizontalCenterRatio = null;

		public CenterConstraint(Func<Rectangle> getContainer, params MenuGroup[] targets) {
			GetContainer = getContainer;
			Targets.AddRange(targets);
		}

		public override void Update(Camera c) {
			var currentcenter = FindTargetBounds().Center;
			var container = GetContainer.Invoke();
			
			var targetcenter = container.Center;
			if (VerticalCenterRatio.HasValue) {
				targetcenter.Y = (int)(container.Height * VerticalCenterRatio.Value);
			}
			if (HorizontalCenterRatio.HasValue) {
				targetcenter.X = (int)(container.Width * HorizontalCenterRatio.Value);
			}

			var dif = targetcenter - currentcenter;

			foreach (var target in Targets) {
				target.Position += dif;
				target.SecondUpdate(c);
			}
		}	

		private Rectangle FindTargetBounds() {
			var size = Targets[0].Bounds;

			for (int i = 1; i < Targets.Count; i++) {
				size = Rectangle.Union(size, Targets[i].Bounds);
			}
			return size;
		}

	}
}
