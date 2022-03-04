using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Components.Graphs;

namespace Components.Instruments {
	public interface IInstrument {
		public void StartExecute(Vector2 position, Graph graph);
		public void UpdateExecute(Vector2 position, Graph graph);
		public void FinishExecute(Vector2 position, Graph graph);
	}
}