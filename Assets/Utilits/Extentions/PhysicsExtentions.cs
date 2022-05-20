using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PhysicsExtentions {
	public static bool RaycastCamera(out RaycastHit hit) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			return true;
		}
		return false;
	}
	public static bool RaycastCamera(out RaycastHit hit, int layerMask) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, float.MaxValue,layerMask)) {
			return true;
		}
		return false;
	}
	public static Transform GetRaycastCamera() {
		if (RaycastCamera(out RaycastHit hit)) {
			return hit.transform;
		} else return null;
	}
	public static bool RaycastCameraForTag(out RaycastHit hit, string tag) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			if (hit.transform.CompareTag(tag))
				return true;
		}
		return false;
	}
}
