using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Workspace {
    public class MoveCamera : MonoBehaviour {
        [SerializeField] private float sensetive;
		[Tooltip("Минмиальная точка левого нижнего угла камеры")]
        [SerializeField] private Vector2 min;
		[Tooltip("Максимальня точка правого верхнего угла камеры")]
		[SerializeField] private Vector2 max;

		private Vector3 lastPos;
		private Camera cam;
		private void Start() {
			if (!TryGetComponent(out cam)) {
				Debug.LogError($"ZoomCamera: {name}: dont fount compoentns Camera");
			}
		}
		private void LateUpdate() {
			int layer = LayerMask.GetMask("UI");
			if (PhysicsExtentions.RaycastCamera(out RaycastHit hit, layer)) {
				return;
			}
			if (Input.GetMouseButton(2)) {
				Vector3 delta = (Input.mousePosition - lastPos).Vector2();
				delta = new Vector3(delta.x / (float)Screen.width, delta.y / (float)Screen.height, 0) * cam.orthographicSize;
				transform.position -= delta * sensetive * Time.deltaTime;
				
			}
			transform.position = CheckPosAndClamp(transform.position);

			lastPos = Input.mousePosition;
		}

		private Vector3 CheckPosAndClamp(Vector3 pos) {
			Vector2 size = GetSizeCamera();
			Vector2 _min = new Vector2(
				GetValueWithOffset(min.x, size.x / 2),
				GetValueWithOffset(min.y, size.y / 2));
			Vector2 _max = new Vector2(
				GetValueWithOffset(max.x, size.x / 2),
				GetValueWithOffset(max.y, size.y / 2));

			return new Vector3(
					Mathf.Clamp(pos.x, _min.x, _max.x),
					Mathf.Clamp(pos.y, _min.y, _max.y),
					pos.z);
		}
		private float GetValueWithOffset(float value, float offset) {
			return (Mathf.Abs(value) - offset) * Mathf.Sign(value);
		}

		private Vector2 GetSizeCamera() {
			float h = cam.orthographicSize * 2f;
			float w = h * ((float)Screen.width / (float)Screen.height);
			return new Vector2(w,h);
		}
	}
}