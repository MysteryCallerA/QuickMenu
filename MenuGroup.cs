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

		public MenuGroup(Text t) {
			Text = t;
		}

		/// <summary> If you use this constructor, remember to set Text. </summary>
		protected MenuGroup() {
		}

		public virtual Text Text {
			get; set;
		}

		public virtual Rectangle Bounds { //TODO change this so its readonly, use BlankSpace elements instead for sizing
			get; set;
		} = Rectangle.Empty;

		public void Update(MouseInputManager m, Camera c) {
			Update(m, c, this);
		}

		/// <summary> Updates elements. Calls FirstUpdate, SecondUpdate, then ThirdUpdate. </summary>
		/// <param name="top">The top-level group. Will either be this group or one that contains this group.
		/// <br></br>Use this parameter when calling element update methods instead of 'this'.</param>
		protected internal abstract void Update(MouseInputManager m, Camera c, MenuGroup top);

		public virtual void Draw(Renderer r, Camera c) {
			var e = GetElements();
			foreach (var element in e) {
				element.Draw(this, r, c);
			}
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
	}
}
