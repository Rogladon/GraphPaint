using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Components.Graphs;

namespace Components.Instruments {
	public interface IInstrument {
		public void StartExecute(Graph graph, Selected selected);
		public void UpdateExecute(Graph graph, Selected selected);
		public void FinishExecute(Graph graph, Selected selected);
	}
}