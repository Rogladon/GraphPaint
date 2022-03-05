using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	public class SelectAndMove : IInstrument {
		private IInstrument instrument;
		public void FinishExecute(Graph graph, Selected selected) {
			instrument?.FinishExecute(graph, selected);
		}

		public void StartExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {
				if(hit.transform.TryGetComponentParent(out ISelectable selectable)) {
					selected.ClearAndAdd(selectable);
					instrument = new Move();
				} else {
					selected.Clear();
				}
				instrument?.StartExecute(graph, selected);
			}
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			instrument?.UpdateExecute(graph, selected);
		}
	}
}
