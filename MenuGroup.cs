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

	public enum BorderPosition {
		TopLeft, TopRight, BotLeft, BotRight, CenterTop
	}

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
			get; protected set; //TODO only TableGroup currently implements this!
		}

		public BorderPosition OriginPosition = BorderPosition.TopLeft;

		public virtual Rectangle Bounds {
			get { return new Rectangle(Position, Size); }
		}

		/// <summary>
		/// Calls FirstUpdate, SecondUpdate, ThirdUpdate.
		/// </summary>
		public void Update(MouseInputManager m, Camera c) {
			FirstUpdate(c);
			SecondUpdate(c);
			ThirdUpdate(m, c);
		}

		/// <summary> Updates sizes to the minimum sizes. </summary>
		public void FirstUpdate(Camera c) {
			Text.Scale = c.GameScale;
			FirstUpdate(c, this);
		}

		/// <summary> Updates sizes to the minimum sizes. </summary>
		/// <param name="top">The top-level group. Will either be this group or one that contains this group.
		/// <br></br>Use top parameter when calling element update methods instead of 'this'.</param>
		protected abstract void FirstUpdate(Camera c, MenuGroup top);

		/// <summary> Updates positions and stretches elements if neccessary.  </summary>
		public void SecondUpdate(Camera c) {
			SecondUpdate(c, this);
		}

		/// <summary> Updates positions and stretches elements if neccessary.  </summary>
		/// <param name="top">The top-level group. Will either be this group or one that contains this group.
		/// <br></br>Use top parameter when calling element update methods instead of 'this'.</param>
		protected abstract void SecondUpdate(Camera c, MenuGroup top);

		/// <summary> Updates user input </summary>
		public void ThirdUpdate(MouseInputManager m, Camera c) {
			ThirdUpdate(m, c, this);
		}

		/// <summary> Updates user input </summary>
		/// <param name="top">The top-level group. Will either be this group or one that contains this group.
		/// <br></br>Use top parameter when calling element update methods instead of 'this'.</param>
		protected abstract void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top);

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
			dest.Location += GetOriginOffset();
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

		/// <summary> Returns a point that can be added to Position to get the true draw location based on OriginPosition.
		/// <br></br> Size needs to be set accurately for this to work.</summary>
		public Point GetOriginOffset() {
			return GetBorderOffset(OriginPosition);
		}

		/// <summary> Returns a point that can be added to Position to get the desired border point.
		/// <br></br> Size needs to be set accurately for this to work. </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public Point GetBorderOffset(BorderPosition b) {
			if (b == BorderPosition.TopLeft) return Point.Zero;
			if (b == BorderPosition.BotLeft) return new Point(0, -Size.Y);
			if (b == BorderPosition.TopRight) return new Point(-Size.X, 0);
			if (b == BorderPosition.BotRight) return new Point(-Size.X, -Size.Y);
			if (b == BorderPosition.CenterTop) return new Point(-Size.X / 2, 0);
			return Point.Zero;
		}
	}
}
