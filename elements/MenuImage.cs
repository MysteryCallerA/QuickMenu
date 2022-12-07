using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuickMenu.groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.graphic;
using Utils.math;

namespace QuickMenu.elements
{

	public class MenuImage:InnerElement {

		public Color Color = Color.White;
		public ITextureObject Content;
		public int Scale = 1;
		public int? TargetWidth, TargetHeight;
		public bool FitWidth = false, FitHeight = false, KeepAspect = true;

		protected Rectangle DrawRect;

		public MenuImage(ITextureObject g) {
			Content = g;
		}

		protected internal override void FirstUpdate(MenuGroup g) {
			int width = TargetWidth ?? Content.Width * Scale;
			int height = TargetHeight ?? Content.Height * Scale;

			if (KeepAspect) { //If only one dimension is set then the other will match aspect
				if (TargetWidth.HasValue && !TargetHeight.HasValue) {
					height = Aspect.GetAspectedHeight(Content.Source.Size, width);
				} else if (TargetHeight.HasValue && !TargetWidth.HasValue) {
					width = Aspect.GetAspectedWidth(Content.Source.Size, height);
				}
			}

			DrawRect = new Rectangle(0, 0, width, height);
		}

		protected internal override void SecondUpdate(MenuGroup g, Point origin, Point maxSize) {
			maxSize -= MarginSize;
			int width = DrawRect.Width, height = DrawRect.Height;

			if (FitWidth) {
				width = maxSize.X;
			}
			if (FitHeight) {
				height = maxSize.Y;
			}

			if (KeepAspect) {
				var bestfit = Aspect.GetBestFit(Content.Source.Size, maxSize);
				width = bestfit.X;
				height = bestfit.Y;
			}

			DrawRect = new Rectangle(origin.X + MarginLeft, origin.Y + MarginTop, width, height);
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c) {
			Draw(g, r, c, Color);
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c, ColorTable t, ColorTable.State state) {
			Draw(g, r, c, t.Get(ColorTable.Field.Image, state));
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c, Color color) {
			r.DrawStatic(Content.Texture, DrawRect, Content.Source, color, c, Camera.Space.Pixel);
		}

		public override Point GetSize() {
			return base.GetSize() + DrawRect.Size;
		}
	}
}
