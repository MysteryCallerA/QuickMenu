using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu.groups {
	
	public abstract class MetaGroup:MenuGroup {

		protected MetaGroup(Font f) : base(f) {

		}

		protected abstract MenuGroup GetGroup();

		protected internal override void FirstUpdate(Camera c, MenuGroup top) {
			GetGroup().FirstUpdate(c, top);
		}

		protected internal override void SecondUpdate(Camera c, MenuGroup top) {
			GetGroup().SecondUpdate(c, top);
		}

		protected internal override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			GetGroup().ThirdUpdate(m, c, top);
		}

		protected internal override List<MenuElement> GetElements() {
			return GetGroup().GetElements();
		}

		public override Text Text {
			get { return GetGroup().Text; }
		}

		public override Rectangle Bounds {
			get { return GetGroup().Bounds; }
		}

		public override Point Position {
			get { return GetGroup().Position; }
			set { GetGroup().Position = value; }
		}

		public override Point Size {
			get { return GetGroup().Size; }
		}

		public override void ConstrainSize(Point newSize) {
			GetGroup().ConstrainSize(newSize);
		}

		public override BorderPosition OriginPosition {
			get { return GetGroup().OriginPosition; }
			set { GetGroup().OriginPosition = value; }
		}

		public override Point GetOriginOffset() {
			return GetGroup().GetOriginOffset();
		}

		public override Point GetBorderOffset(BorderPosition b) {
			return GetGroup().GetBorderOffset(b);
		}

		public override Color BackColor {
			get { return GetGroup().BackColor; }
			set { GetGroup().BackColor = value; }
		}

	}
}
