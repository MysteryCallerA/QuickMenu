using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu {
	public abstract class OuterElement:MenuElement {

		public int PaddingLeft, PaddingRight, PaddingTop, PaddingBot;

		public override Point GetSize() {
			return base.GetSize() + PaddingSize;
		}

		protected Point PaddingSize {
			get { return new Point(PaddingLeft + PaddingRight, PaddingTop + PaddingBot); }
		}

		protected Point PaddingOffset {
			get { return new Point(PaddingLeft, PaddingTop); }
		}

		public int Padding {
			set { PaddingLeft = value; PaddingRight = value; PaddingTop = value; PaddingBot = value; }
		}

	}
}
