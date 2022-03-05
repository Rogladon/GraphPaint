using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	public class RemoveNode : IInstrument {
		public void FinishExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {
				if(hit.transform.TryGetComponentParent(out NodeAppearance na)) {
					new Graph.CmdRemoveNode() { graph = graph, node = na.node }.Execute();
				}
			}
		}

		public void StartExecute(Graph graph, Selected selected) {
			
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			
		}
	}
}
