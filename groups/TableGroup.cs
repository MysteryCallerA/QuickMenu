using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Utils.data;
using Utils.input;
using Utils.text;

namespace QuickMenu.groups {

	public class TableGroup:UnpagedMenuGroup {

		private List<List<MenuElement>> Table = new List<List<MenuElement>>(); //Table[col][row]
		private Dictionary<Point, Point> Spans = new Dictionary<Point, Point>();

		private bool ContentsChanged = true;
		private List<MenuElement> Contents = new List<MenuElement>();
		private List<int> ColSizes = new List<int>();
		private List<int> RowSizes = new List<int>();

		public TableGroup(Font f) : base(f) {

		}

		protected internal override List<MenuElement> GetElements() {
			if (ContentsChanged) {
				ContentsChanged = false;
				Contents.Clear();
				foreach (var column in Table) {
					foreach (var cell in column) {
						if (cell == null) continue;
						Contents.Add(cell);
					}
				}
			}
			return Contents;
		}

		protected override void FirstUpdate(Camera c, MenuGroup top) {
			ColSizes.Clear();
			RowSizes.Clear();
			var elements = GetElements();

			foreach (var e in elements) {
				e.FirstUpdate(top);
			}
			for (int col = 0; col < Table.Count; col++) {
				if (Table[col].Count == 0) ColSizes.Add(0);
				for (int row = 0; row < Table[col].Count; row++) {
					if (Table[col][row] == null) {
						if (col >= ColSizes.Count) ColSizes.Add(0);
						if (row >= RowSizes.Count) RowSizes.Add(0);
						continue;
					}
					if (Spans.ContainsKey(new Point(col, row))) continue;
					SolveCellSizeNormally(col, row);
				}
			}
			foreach (var cell in Spans) {
				SolveCellSizeWithSpan(cell.Key.X, cell.Key.Y, cell.Value);
			}

			AutoSize = new Point(ColSizes.Sum(), RowSizes.Sum());
		}

		protected override void SecondUpdate(Camera c, MenuGroup top) {
			Point origin = Position;
			origin += GetOriginOffset();

			for (int col = 0; col < Table.Count; col++) {
				for (int row = 0; row < Table[col].Count; row++) {
					if (Table[col][row] != null) Table[col][row].SecondUpdate(top, origin, new Point(ColSizes[col], RowSizes[row]));
					origin.Y += RowSizes[row];
				}
				origin = new Point(origin.X + ColSizes[col], Bounds.Location.Y);
			}
		}

		protected override void ThirdUpdate(MouseInputManager m, Camera c, MenuGroup top) {
			var elements = GetElements();
			foreach (var e in elements) {
				e.ThirdUpdate(top, c, m);
			}
		}

		private void SolveCellSizeNormally(int col, int row) {
			var size = Table[col][row].GetSize();

			if (col >= ColSizes.Count) {
				ColSizes.Add(size.X);
			} else if (size.X > ColSizes[col]) {
				ColSizes[col] = size.X;
			}

			if (row >= RowSizes.Count) {
				RowSizes.Add(size.Y);
			} else if (size.Y > RowSizes[row]) {
				RowSizes[row] = size.Y;
			}
		}

		private void SolveCellSizeWithSpan(int col, int row, Point span) {
			var size = Table[col][row].GetSize();
			while (col + span.X - 1 >= ColSizes.Count) {
				ColSizes.Add(0);
			}
			while (row + span.Y - 1 >= RowSizes.Count) {
				RowSizes.Add(0);
			}

			int spaceneeded = size.X;
			for (int i = 0; i < span.X; i++) {
				spaceneeded -= ColSizes[col + i];
			}
			if (spaceneeded > 0) ColSizes[col + span.X - 1] += spaceneeded;

			spaceneeded = size.Y;
			for (int i = 0; i < span.Y; i++) {
				spaceneeded -= RowSizes[row + i];
			}
			if (spaceneeded > 0) RowSizes[row + span.Y - 1] += spaceneeded;
		}

		public void AddElement(MenuElement e, int col, int row) {
			ContentsChanged = true;
			while (col >= Table.Count) {
				Table.Add(new List<MenuElement>());
			}
			while (row >= Table[col].Count) {
				Table[col].Add(null);
			}

			Table[col][row] = e;
		}

		public void AddElement(MenuElement e, int col, int row, int colspan, int rowspan) {
			AddElement(e, col, row);

			if (colspan < 1) colspan = 1;
			if (rowspan < 1) rowspan = 1;

			Spans.Add(new Point(col, row), new Point(colspan, rowspan));
		}

		public void Clear() {
			ContentsChanged = true;
			Table.Clear();
			Spans.Clear();
		}
	}
}
