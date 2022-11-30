using Microsoft.Xna.Framework;
using QuickMenu.groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.text;

namespace QuickMenu.elements
{

    public class MenuText:InnerElement {

		public Color Color = Color.White;

		protected Point DrawPoint;
		protected Point ContentSize;
		private string _Content;

		public MenuText():this("") {
		}

		public MenuText(string t) {
			Content = t;
		}

		public bool ContentChanged {
			get; private set;
		} = true;

		public string Content {
			get { return _Content; }
			set { _Content = value; ContentChanged = true; }
		}

		protected internal override void FirstUpdate(MenuGroup g) {
			if (ContentChanged) {
				var old = g.Text.Scale;
				g.Text.Scale = 1;
				ContentSize = g.Text.GetSize(Content);
				g.Text.Scale = old;
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
	}
}
