using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.text;

namespace QuickMenu {

	/// <summary>
	/// MenuGroup that doesn't have pages. Exact same functionality as MenuGroup.
	/// <br></br>Only exists to limit PagedMenuGroup to containing UnpagedMenuGroups.
	/// </summary>
	public abstract class UnpagedMenuGroup:MenuGroup {

		public UnpagedMenuGroup(Font f) : base(f) {
		}
	}
}
