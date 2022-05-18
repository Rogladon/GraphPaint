using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments {
	public interface IInstrumentManager {
		public string TAG_WORKESPACE { get; }
		public void SetInstrumentLkm(InstrumentType type);
		public bool ContainsInstrument(InstrumentType type);
		public Graphs.Graph graph {get;}//TOODOO вынести в отдельную сущность
	}
}
