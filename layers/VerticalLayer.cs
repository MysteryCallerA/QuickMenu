using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Toybox;
using Utils.style;
using Utils.text;

namespace QuickMenu.layers {

	public class VerticalLayer:MenuLayer {

		public List<MenuOption> Options = new List<MenuOption>();
		public int Selection = 0;

		public override void Draw(Renderer r, Camera c, Point origin, Style style, Text t) {
			foreach (var option in Options) {
				option.Draw(r, c, style, t);
			}
		}

		public override MenuOption GetSelectedOption() {
			return Options[Selection];
		}

		public override void PressDown() {
			base.PressDown();
			Selection++;
		}

		public override void PressUp() {
			base.PressUp();
			Selection--;
		}

		public override void Update(Style style, Text t) {
			Rectangle largest = Rectangle.Empty;
			Rectangle container = Rectangle.Empty;
			foreach (var option in Options) {
				option.Bounds = option.GetMinBounds(style, t);
				option.Bounds.Location += Position;
				if (option.Bounds.Width > largest.Width) largest = option.Bounds;
				if (container.Width == 0) container = option.Bounds;
				else container = Rectangle.Union(container, option.Bounds);
			}

			if (Autosize) Bounds = container;

			foreach (var option in Options) {
				option.SelectStyle(style);
				if (style.HSizeMode == Style.SizeMode.Match) option.Bounds.Width = largest.Width;
				else if (style.HSizeMode == Style.SizeMode.Stretch) option.Bounds.Width = container.Right - option.Bounds.X;
				if (style.VSizeMode == Style.SizeMode.Match) option.Bounds.Height = largest.Height;
			}
		}
	}
}
