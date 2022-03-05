using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class TransformExtentions {
	public static bool TryGetComponentParent<T>(this Transform t, out T component) {
		if((component = t.GetComponentInParent<T>()) != null){
			return true;
		}
		return false;
	}
	public static Bounds GetSummaryBoundsSprite(this Transform t) {
		var r = t.GetComponentsInChildren<SpriteRenderer>();
		Bounds bounds = new Bounds();
		Vector3 min = Vector3.zero;
		Vector3 max = Vector3.zero;
		r.AsParallel().ForEach(p => {
			min = Vector3.Min(min, p.bounds.min);
			max = Vector3.Max(max, p.bounds.max);
		});

		bounds.SetMinMax(min, max);
		return bounds;
	}
}
