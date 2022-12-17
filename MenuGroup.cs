using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu {
	public abstract class MenuGroup {

		public Color BackColor = Color.Transparent;

		public MenuGroup(Font f) {
			Text = new Text(f);
		}

		public virtual Text Text {
			get; private set;
		}

		public virtual Point Position {
			get; set;
		}

		public virtual Point Size {
			get; protected set;
		}

		public enum AnchorPositions {
			TopLeft, TopRight, BotLeft, BotRight, CenterTop
		}

		public AnchorPositions AnchorPosition = AnchorPositions.TopLeft;

		public virtual Rectangle Bounds {
			get { return new Rectangle(Position, Size); }
		}

		public void Update(MouseInputManager m, Camera c) {
			Text.Scale = c.GameScale;
			Update(m, c, this);
		}

		/// <summary> Updates elements. Calls FirstUpdate, SecondUpdate, then ThirdUpdate. </summary>
		/// <param name="top">The top-level group. Will either be this group or one that contains this group.
		/// <br></br>Use this parameter when calling element update methods instead of 'this'.</param>
		protected internal abstract void Update(MouseInputManager m, Camera c, MenuGroup top);

		public virtual void Draw(Renderer r, Camera c) {
			DrawBack(r, c);

			var e = GetElements();
			foreach (var element in e) {
				element.Draw(this, r, c);
			}
		}

		protected virtual void DrawBack(Renderer r, Camera c) {
			if (BackColor == Color.Transparent) return;
			var dest = Bounds;
			dest.Location += GetAnchorOffset();
			r.DrawRectStatic(dest, BackColor, c, Camera.Space.Pixel);
		}

		protected internal abstract List<MenuElement> GetElements();

		/// <summary> Call this to switch between menu pages (when supported).
		/// <br></br>Use an enum for page ids. Pages are stored in a Dictionary so you don't need to worry about order when adding.</summary>
		public virtual void SwitchPage(int p) {
		}

		/// <summary> Call this to go to the previous menu page (when supported).
		public virtual void BackPage() {
		}

		/// <summary> Call this to go to the home menu page (when supported).
		public virtual void HomePage() {
		}

		/// <summary> Called when page switching away from this group. Drops focus from elements. </summary>
		protected internal virtual void DropFocus() {
			var e = GetElements();
			foreach (var element in e) {
				element.DropFocus();
			}
		}

		public void Apply(Action<MenuElement> a) {
			var elements = GetElements();
			foreach (var e in elements) {
				e.Apply(a);
			}
		}

		/// <summary> Returns a point that can be added to Position to get the true draw location based on AnchorPosition.
		/// <br></br> Size needs to be set accurately for this to work.</summary>
		protected Point GetAnchorOffset() {
			if (AnchorPosition == AnchorPositions.TopLeft) return Point.Zero;
			if (AnchorPosition == AnchorPositions.BotLeft) return new Point(0, -Size.Y);
			if (AnchorPosition == AnchorPositions.TopRight) return new Point(-Size.X, 0);
			if (AnchorPosition == AnchorPositions.BotRight) return new Point(-Size.X, -Size.Y);
			if (AnchorPosition == AnchorPositions.CenterTop) return new Point(-Size.X / 2, 0);
			return Point.Zero;
		}
	}
}
