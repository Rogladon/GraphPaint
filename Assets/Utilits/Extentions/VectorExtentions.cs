using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class VectorExtentions {
	public static Vector2 Vector2(this Vector3 v) {
		return new Vector2(v.x, v.y);
	}
	public static Vector3 Vector3(this Vector2 v, float z) {
		return new Vector3(v.x, v.y, z);
	}
	public static Vector3 Vector3(this Vector2 v) {
		return v.Vector3(0);
	}
}
