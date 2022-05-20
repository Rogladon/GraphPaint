using UnityEngine;

namespace Components.Workspace {
    public class ZoomCamera : MonoBehaviour {
		[SerializeField] private float sensetive;
		[SerializeField] private float minSize;
		[SerializeField] private float maxSize;
		private Camera cam;
		
		private void Start() {
			if(!TryGetComponent(out cam)) {
				Debug.LogError($"ZoomCamera: {name}: dont fount compoentns Camera");
			}
		}

		private void Update() {
			int layer = LayerMask.GetMask("UI");
			if (PhysicsExtentions.RaycastCamera(out RaycastHit hit, layer)) {
				return;
			}
			if (Input.mouseScrollDelta != Vector2.zero) {
				cam.orthographicSize -= Input.mouseScrollDelta.y * sensetive*Time.deltaTime;
				cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
			}
		}
	}
}