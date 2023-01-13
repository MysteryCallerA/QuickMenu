using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Toybox;
using Utils.input;
using Utils.text;

namespace QuickMenu {
	public class PagedMenuGroup:MenuGroup {

		private Dictionary<int, MenuGroup> Pages = new Dictionary<int, MenuGroup>();
		private Stack<int> History = new Stack<int>();
		public int TopPageId;
		private int CurrentPageId;

		private int? WillSwitchPageTo;

		public PagedMenuGroup(Font f, UnpagedMenuGroup top, int topid):base(f) {
			Pages.Add(topid, top);
			CurrentPageId = TopPageId = topid;
		}

		public MenuGroup CurrentPage {
			get { return Pages[CurrentPageId]; }
		}

		public void AddPage(int id, UnpagedMenuGroup page) {
			Pages.Add(id, page);
		}

		protected override void FirstUpdate(Camera c, MenuGroup top) {
			if (WillSwitchPageTo.HasValue) {
				CompletePageSwitch();
			}
			CurrentPage.FirstUpdate(c);
		}

		protected override void SecondUpdate(Camera c, MenuGroup top) {
			CurrentPage.SecondUpdate(c);
		}

		protected override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			CurrentPage.ThirdUpdate(m, c);
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
