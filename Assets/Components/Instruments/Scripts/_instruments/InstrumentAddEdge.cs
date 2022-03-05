using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	[System.Serializable]
	internal class AddEdge : IInstrument {
		[SerializeField] private LineRenderer appearanceEdge;
		private Node firstNode = null;
		private Node secondNode = null;
		private Edge tempEdge;
		public void FinishExecute(Graph graph, Selected selected) {
			throw new System.NotImplementedException();
		}

		public void StartExecute(Graph graph, Selected selected) {
			NodeAppearance na = null;
			if((PhysicsExtentions.GetRaycastCamera()?.TryGetComponentParent(out na)).HasValue) {
				firstNode = na.node;
			}
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {

			}
		}
	}
	[CreateAssetMenu(fileName ="AddEdge", menuName ="Instruments/AddEdge")]
	public class InstrumentAddEdge: InstrumentHolder {
		[SerializeField] private AddEdge instrument;

		public override IInstrument component => instrument;
	}
}
