using Microsoft.Xna.Framework;
using QuickMenu.groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.input;

namespace QuickMenu.elements
{

    public class MenuButton:OuterElement {

		public InnerElement Content;
		public ColorTable Colors;
		public int? TargetWidth, TargetHeight;
		public bool FitWidth = false, FitHeight = false;
		public EventHandler OnPress;

		protected Rectangle DrawRect;

		public MenuButton(InnerElement e):this(e, new ColorTable()) {
		}

		public MenuButton(InnerElement e, ColorTable c) {
			Content = e;
			Colors = c;
		}

		public ColorTable.State State {
			get; protected set;
		}

		protected internal override void FirstUpdate(MenuGroup g) {
			Content.FirstUpdate(g);
			var content = Content.GetSize() + PaddingSize;

			int width = TargetWidth ?? content.X;
			int height = TargetHeight ?? content.Y;

			DrawRect = new Rectangle(0, 0, width, height);
		}

		protected internal override void SecondUpdate(MenuGroup g, Point origin, Point maxSize) {
			maxSize -= MarginSize;
			int width = DrawRect.Width, height = DrawRect.Height;

			if (FitWidth) width = maxSize.X;
			if (FitHeight) height = maxSize.Y;

			DrawRect = new Rectangle(origin.X + MarginLeft, origin.Y + MarginTop, width, height);

			Content.SecondUpdate(g, DrawRect.Location + PaddingOffset, maxSize - PaddingSize);
		}

		protected internal override void ThirdUpdate(MenuGroup g, Camera c, MouseInputManager m) {
			base.ThirdUpdate(g, c, m);
			if (m.Blocked) {
				State = ColorTable.State.Normal;
				return;
			}

			var pointer = c.Project(Camera.Space.Screen, Camera.Space.Pixel, m.Position);
			pointer -= c.GamePosition; //TODO add a better way to project statically
			if (DrawRect.Contains(pointer)) {
				m.Block();
				if (m.LeftPress) {
					State = ColorTable.State.Press;
					Press(g);
				} else if (State != ColorTable.State.Press || !m.Left) {
					State = ColorTable.State.Hover;
				}
			} else {
				State = ColorTable.State.Normal;
			}
		}

		protected internal override void Draw(MenuGroup g, Renderer r, Camera c) {
			r.DrawRectStatic(DrawRect, Colors.Get(ColorTable.Field.Back, State), c, Camera.Space.Pixel);
			Content.Draw(g, r, c, Colors, State);
		}

		public override Point GetSize() {
			return DrawRect.Size + MarginSize;
		}

		protected internal virtual void Press(MenuGroup g) {
			OnPress?.Invoke(this, new EventArgs());
		}

		public bool Hovered {
			get { return State == ColorTable.State.Hover; }
		}

		protected internal override void DropFocus() {
			base.DropFocus();
			State = ColorTable.State.Normal;
		}

		public override void Apply(Action<MenuElement> a) {
			base.Apply(a);
			Content.Apply(a);
		}
	}
}
