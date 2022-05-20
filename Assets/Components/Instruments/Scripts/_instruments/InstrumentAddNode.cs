using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	[System.Serializable]
	internal class AddNode : IInstrument {
		private IInstrumentManager mg => InstrumentManager.instance;
		private bool isStartOnWorkspace = false;
		public void FinishExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCameraForTag(out RaycastHit hit, mg.TAG_WORKESPACE) 
				&& isStartOnWorkspace)
				new Graph.CmdAddNode { graph = graph, position = hit.point.Vector2() }.Execute();
		}

		public void StartExecute( Graph graph, Selected selected) {
			if (PhysicsExtentions.RaycastCameraForTag(out RaycastHit hit, mg.TAG_WORKESPACE))
				isStartOnWorkspace = true;
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			
		}
	}
	[CreateAssetMenu(fileName = "AddNode", menuName = "Instruments/AddNode")]
	public class InstrumentAddNode : InstrumentHolder {
		[SerializeField] private AddNode _instrument;

		public override IInstrument component => _instrument;
	}
}
