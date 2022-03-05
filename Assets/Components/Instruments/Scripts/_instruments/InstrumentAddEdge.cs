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
		private LineRenderer line = null;
		public void FinishExecute(Graph graph, Selected selected) {
			if (line != null) Object.Destroy(line.gameObject);
			if (firstNode == null) return;
			NodeAppearance na = null;
			if ((PhysicsExtentions.GetRaycastCamera()?.TryGetComponentParent(out na)).Value) {
				Debug.Log($"{firstNode}, {na}, {graph}");
				new Graph.CmdAddEdge() { firstNode = firstNode, secondNode = na.node, graph = graph }
				.Execute();
			}
			firstNode = null;
		}
		public void StartExecute(Graph graph, Selected selected) {
			NodeAppearance na = null;
			if((PhysicsExtentions.GetRaycastCamera()?.TryGetComponentParent(out na)).Value) {
				firstNode = na.node;
				line = Object.Instantiate(appearanceEdge);
				line.positionCount = 2;
				line.SetPosition(0, na.position);
			}
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {
				if(line != null)
					line?.SetPosition(1, hit.point.Vector2());
			}
		}
	}
	[CreateAssetMenu(fileName ="AddEdge", menuName ="Instruments/AddEdge")]
	public class InstrumentAddEdge: InstrumentHolder {
		[SerializeField] private AddEdge instrument;

		public override IInstrument component => instrument;
	}
}
