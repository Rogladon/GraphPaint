using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments._instruments {
	[System.Serializable]
	internal class Move : IInstrument {
		private class MoveDelta {
			public IMovable movable;
			public Vector2 delta;

			public MoveDelta(IMovable m, Vector2 curPos) {
				movable = m;
				delta = curPos - m.position;
			}
			public void Move(Vector2 curPos) {
				movable.Move(curPos - delta);
			}
			public void Commands() {
				movable.GetCommands().ForEach(p => p.Execute());
			}
		}
		private List<MoveDelta> movables;
		public void FinishExecute(Graph graph, Selected selected) {
			movables.ForEach(p => p.Commands());
		}

		public void StartExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {
				Vector2 hitPoint = hit.point.Vector2();
				movables = selected.Where(p => p is IMovable)
				.Select(p => new MoveDelta(p as IMovable, hitPoint)).ToList();
			}
			
		}

		public void UpdateExecute(Graph graph, Selected selected) {
			if(PhysicsExtentions.RaycastCamera(out RaycastHit hit)) {
				movables.ForEach(p => p.Move(hit.point.Vector2()));
			}
		}
	}
	[CreateAssetMenu(fileName = "Move", menuName = "Instruments/Moves")]
	public class InstrumentMove : InstrumentHolder {
		[SerializeField] private Move _instrument;

		public override IInstrument component => _instrument;
	}
}
