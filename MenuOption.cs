using Microsoft.Xna.Framework;
using Toybox;
using Utils.style;
using Utils.text;

namespace QuickMenu {
	
	public abstract class MenuOption {

		public string Text;
		public string Description;

		public const string ElementName = "MenuOption";

		public enum State {
			Default, Hover, Pressed
		}

		public State CurrentState = State.Default;

		public Rectangle Bounds;

		public MenuOption(string text) {
			Text = text;
		}

		public abstract void Select(MenuManager m);

		/// <summary> Returns the minimum size rectangle at 0,0 with margins included. The container then sets Bounds based on outside factors. </summary>
		public Rectangle GetMinBounds(Style s, Text t) {
			SelectStyle(s);
			var output = s.OuterMinBounds;
			if (s.HSizeMode != Style.SizeMode.Set || s.VSizeMode != Style.SizeMode.Set) {
				var content = t.GetSize(Text);
				if (s.HSizeMode != Style.SizeMode.Set) output.Width += content.X;
				if (s.VSizeMode != Style.SizeMode.Set) output.Height += content.Y;
			}
			if (s.HSizeMode == Style.SizeMode.Set) output.Width += s.Width;
			if (s.VSizeMode == Style.SizeMode.Set) output.Height += s.Height;

			return output;
		}

		public void Draw(Renderer r, Camera c, Style s, Text t) {
			SelectStyle(s);
			var content = new Rectangle(Bounds.X + s.MarginLeft, Bounds.Y + s.MarginTop, Bounds.Width - (s.MarginLeft + s.MarginRight), Bounds.Height - (s.MarginTop + s.MarginBottom));

			if (s.ColorBack != Color.Transparent) {
				r.DrawRectStatic(content, s.ColorBack, c, Camera.Space.Pixel);
			}

			if (s.ColorBorder != Color.Transparent) {
				Utils.math.Utils.GetEdges(content, s.BorderThickness, out Rectangle top, out Rectangle bot, out Rectangle left, out Rectangle right);
				r.DrawRectStatic(top, s.ColorBorder, c, Camera.Space.Pixel);
				r.DrawRectStatic(bot, s.ColorBorder, c, Camera.Space.Pixel);
				r.DrawRectStatic(left, s.ColorBorder, c, Camera.Space.Pixel);
				r.DrawRectStatic(right, s.ColorBorder, c, Camera.Space.Pixel);
			}

			if (s.ColorText != Color.Transparent) {
				var pos = content.Location + s.InnerOffset;
				pos = c.Project(Camera.Space.Pixel, Camera.Space.Subpixel, pos);
				t.Draw(r.Batch, s.ColorText, pos, Text);
			}
		}

		public virtual void SelectStyle(Style s) {
			s.Select(CurrentState.ToString(), ElementName, Text);
		}
	
	}
}
