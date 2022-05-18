using UnityEngine;
using System.Collections.Generic;

namespace Utilits {
	public static class Colors {
		private static Dictionary<int, Color> usesColors = new Dictionary<int, Color>();
		public static Color Get(int index) {
			if (index == -1) return Color.white;
			if (!usesColors.ContainsKey(index))
				//Random.ColorHSV(0,1,0,1,0,1,1,1)
				usesColors.Add(index, new Color(Random.value, Random.value, Random.value,1));	
			return usesColors[index];

		}
	}
}
