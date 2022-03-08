using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Colors {
	public static Color GetColorForInt(int color) {
		switch (color) {
			case -1: return Color.white;
			case 0: return Color.red;
			case 1: return Color.blue;
			case 2: return Color.green;
			case 3: return Color.gray;
			case 4: return Color.cyan;
			case 5: return Color.yellow;
			case 6: return Color.magenta;
			default: return Color.black;
		}
	}
}
