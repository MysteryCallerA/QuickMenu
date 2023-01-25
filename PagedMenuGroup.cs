using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu {
	public class PagedMenuGroup:MenuGroup {

		protected Dictionary<int, MenuGroup> Pages = new Dictionary<int, MenuGroup>();
		protected Stack<int> History = new Stack<int>();
		public int TopPageId;
		public int CurrentPageId {
			get; private set;
		}
		public bool RightClickBack = false;

		private int? WillSwitchPageTo;

		public PagedMenuGroup(Font f, UnpagedMenuGroup top, int topid):base(f) {
			Pages.Add(topid, top);
			CurrentPageId = TopPageId = topid;
		}

		protected PagedMenuGroup(Font f) : base(f) {

		}

		public MenuGroup CurrentPage {
			get { return Pages[CurrentPageId]; }
		}

		public void AddPage(int id, UnpagedMenuGroup page) {
			Pages.Add(id, page);
		}

		protected internal override void FirstUpdate(Camera c, MenuGroup top) {
			if (WillSwitchPageTo.HasValue) {
				CompletePageSwitch();
			}
			CurrentPage.Text.Scale = c.GameScale;
			CurrentPage.FirstUpdate(c, top);
		}

		protected internal override void SecondUpdate(Camera c, MenuGroup top) {
			CurrentPage.SecondUpdate(c, top);
		}

		protected internal override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			if (RightClickBack && m.RightPress) {
				BackPage();
				return;
			}

			CurrentPage.ThirdUpdate(m, c, top);
		}

		protected internal override List<MenuElement> GetElements() {
			return CurrentPage.GetElements();
		}

		public override Text Text {
			get { return CurrentPage.Text; }
		}

		public override Rectangle Bounds {
			get { return CurrentPage.Bounds; }
		}

		public override Point Position {
			get { return CurrentPage.Position; }
			set { CurrentPage.Position = value; }
		}

		public override Point Size {
			get { return CurrentPage.Size; }
		}

		public override void ConstrainSize(Point newSize) {
			CurrentPage.ConstrainSize(newSize);
		}

		public override BorderPosition OriginPosition {
			get { return CurrentPage.OriginPosition; }
			set { CurrentPage.OriginPosition = value; }
		}

		public override Point GetOriginOffset() {
			return CurrentPage.GetOriginOffset();
		}

		public override Point GetBorderOffset(BorderPosition b) {
			return CurrentPage.GetBorderOffset(b);
		}

		public override Color BackColor {
			get { return CurrentPage.BackColor; }
			set { CurrentPage.BackColor = value; }
		}


		public override void SwitchPage(int p) {
			base.SwitchPage(p);
			History.Push(CurrentPageId);
			WillSwitchPageTo = p;
		}

		public override void BackPage() {
			base.BackPage();
			if (History.Count == 0) return;
			WillSwitchPageTo = History.Pop();
		}

		public override void HomePage() {
			base.HomePage();
			if (History.Count == 0) return;
			WillSwitchPageTo = TopPageId;
			History.Clear();
		}

		/// <summary> Page switching is delayed until the next frame to prevent things drawing before they're first updated. </summary>
		private void CompletePageSwitch() {
			CurrentPage.DropFocus();
			CurrentPageId = WillSwitchPageTo.Value;
			WillSwitchPageTo = null;
		}
	}
}
