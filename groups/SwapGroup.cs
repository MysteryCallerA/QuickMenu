
using Utils.text;

namespace QuickMenu.groups {

	public class SwapGroup:MetaGroup {

		public MenuGroup ContentGroup;

		public SwapGroup(Font f, MenuGroup content):base(f) {
			ContentGroup = content;
		}

		protected override MenuGroup GetGroup() {
			return ContentGroup;
		}
	}
}
