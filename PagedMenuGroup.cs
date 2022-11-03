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

		public PagedMenuGroup(Text t, UnpagedMenuGroup top, int topid) {
			Pages.Add(topid, top);
			CurrentPageId = TopPageId = topid;
			Text = t;
		}

		public MenuGroup CurrentPage {
			get { return Pages[CurrentPageId]; }
		}

		public void AddPage(int id, UnpagedMenuGroup page) {
			Pages.Add(id, page);
		}

		protected internal override void Update(MouseInputManager m, Camera c, MenuGroup top) {
			if (WillSwitchPageTo.HasValue) {
				CompletePageSwitch();
			}

			CurrentPage.Update(m, c, this);
		}

		protected internal override List<MenuElement> GetElements() {
			return CurrentPage.GetElements();
		}

		public override Text Text {
			get { return CurrentPage.Text; }
			set { CurrentPage.Text = value; }
		}

		public override Rectangle Bounds {
			get { return CurrentPage.Bounds; }
			set { CurrentPage.Bounds = value; }
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
