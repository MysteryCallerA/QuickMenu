using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMenu {
	
	public class ColorTable {

		public enum Field {
			Back, Outline, Text, Image
		}

		public enum State {
			Normal, Hover, Press, Disabled, Toggled, ToggledHover
		}

		private Dictionary<Tuple<Field, State>, Color> Colors = new Dictionary<Tuple<Field, State>, Color>();

		public ColorTable() {
			Set(Field.Back, State.Normal, Color.Black);
			Set(Field.Outline, State.Normal, Color.White);
			Set(Field.Text, State.Normal, Color.White);
			Set(Field.Image, State.Normal, Color.White);
		}

		public void Set(Field f, State s, Color c) {
			var key = new Tuple<Field, State>(f, s);
			if (Colors.ContainsKey(key)) Colors[key] = c;
			else Colors.Add(key, c);
		}

		public Color Get(Field f, State s) {
			if (Colors.TryGetValue(new Tuple<Field, State>(f, s), out Color c)) {
				return c;
			}

			if (s == State.ToggledHover && Colors.TryGetValue(new Tuple<Field, State>(f, State.Hover), out c)) {
				return c;
			}
			if (s == State.Toggled && Colors.TryGetValue(new Tuple<Field, State>(f, State.Press), out c)) {
				return c;
			}

			return Colors[new Tuple<Field, State>(f, State.Normal)];
		}
	
	}
}
