using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Instruments {
	public class InstrumentManager : MonoBehaviour {
		private const string TAG_WORKESPACE = "WORKSPACE";

		#region Singlton
		private static InstrumentManager _instance;

		public static InstrumentManager instance {
			get {
				if (_instance == null)
					_instance = new GameObject("InstrumentManager").AddComponent<InstrumentManager>();
				return _instance;
			}
		}
		#endregion


		#region Fields
		private Graphs.Graph currentGraph;
		private IInstrument currentInstrumentLkm;
		#endregion


		#region Public Methods
		public void SetInstrumentLkm(IInstrument instrument) {
			currentInstrumentLkm = instrument;
		}
		#endregion

		#region Private Methods
		private void Awake() {
			if(_instance != null) {
				Debug.LogError($"Второй instrumentManager: {name}");
				Destroy(this);
			} else {
				currentGraph = new Graphs.Graph();
				var bcl = new GameObject("InstrumentManagerCollider").AddComponent<BoxCollider>();
				bcl.transform.SetParent(Camera.main.transform);
				bcl.size = new Vector3(1000, 1000, 0.1f);
				bcl.tag = TAG_WORKESPACE;
			}
		}
		private void Update() {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Debug.Log(hit);
				if (hit.transform.CompareTag(TAG_WORKESPACE)) {
					Lkm(hit.point.Vector2());
				}
			}
		}

		private void Lkm(Vector2 position) {
			Debug.Log(currentInstrumentLkm);
			if (currentInstrumentLkm == null) return;
			if (Input.GetMouseButtonDown(0)) currentInstrumentLkm.StartExecute(position, currentGraph);
			if (Input.GetMouseButton(0)) currentInstrumentLkm.UpdateExecute(position, currentGraph);
			if (Input.GetMouseButtonUp(0)) currentInstrumentLkm.FinishExecute(position, currentGraph);
		}
		#endregion
	}
}