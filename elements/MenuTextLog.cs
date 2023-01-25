using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace QuickMenu.elements {
	
	public class MenuTextLog:InnerElement {

		public Color Color = Color.White;
		private List<string> Lines = new List<string>();
		protected Point ContentSize;

		private Point DrawPoint;

		public bool ContentChanged {
			get; private set;
		} = true;

		public string Content {
			get; private set;
		} = "";

		protected internal override void FirstUpdate(MenuGroup g) {
			if (ContentChanged) {
				Content = String.Join('\n', Lines);
				
				var old = g.Text.Scale;
				g.Text.Scale = 1;
				ContentSize = g.Text.GetSize(Content);
				g.Text.Scale = old;
				ContentChanged = false;
			}
		}

		protected internal override void SecondUpdate(MenuGroup g, Point origin, Point maxSize) {
			DrawPoint = origin + new Point(MarginLeft, MarginTop);
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c) {
			Draw(g, r, c, Color);
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c, ColorTable t, ColorTable.State state) {
			Draw(g, r, c, t.Get(ColorTable.Field.Text, state));
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c, Color color) {
			var dest = c.Project(Camera.Space.Pixel, Camera.Space.Subpixel, DrawPoint);
			g.Text.Draw(r.Batch, color, dest, Content);
		}

		public override Point GetSize() {
			return base.GetSize() + ContentSize;
		}

		public void AddLine(string line) {
			Lines.Add(line);
			ContentChanged = true;
		}

	}
}
