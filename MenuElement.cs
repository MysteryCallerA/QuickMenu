using Microsoft.Xna.Framework;
using QuickMenu.groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.input;

namespace QuickMenu
{
	public abstract class MenuElement {

		public string Name = "";
		public string Class = "";

		public int MarginLeft, MarginRight, MarginTop, MarginBot;

		protected internal abstract void Draw(MenuGroup g, Renderer r, Camera c);

		/// <summary> Updates size to the minimum size. </summary>
		protected internal abstract void FirstUpdate(MenuGroup g);

		/// <summary> Updates position and stretches element if neccessary.  </summary>
		/// <param name="maxSize"> The max size the element can take up if stretched, including margins. </param>
		protected internal abstract void SecondUpdate(MenuGroup g, Point origin, Point maxSize);

		/// <summary> Updates user input </summary>
		protected internal virtual void ThirdUpdate(MenuGroup g, Camera c, MouseInputManager m) {
		}

		public virtual Point GetSize() {
			return MarginSize;
		}

		protected Point MarginSize {
			get { return new Point(MarginLeft + MarginRight, MarginBot + MarginTop); }
		}

		public Point Offset {
			get { return new Point(MarginLeft, MarginTop); }
			set { MarginLeft = value.X; MarginTop = value.Y; }
		}

		/// <summary> Does things like reseting the hover state of a button or dropping focus from a textfield. </summary>
		protected internal virtual void DropFocus() {
		}

		public virtual void Apply(Action<MenuElement> a) {
			a.Invoke(this);
		}

	}
}
