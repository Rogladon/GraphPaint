using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	public class AddNode : IInstrument {
		public void FinishExecute(Vector2 position, Graph graph) {
			new Graph.CmdAddNode { graph = graph, position = position }.Execute();
		}

		public void StartExecute(Vector2 position, Graph graph) {
			
		}

		public void UpdateExecute(Vector2 position, Graph graph) {
			
		}
	}
}
