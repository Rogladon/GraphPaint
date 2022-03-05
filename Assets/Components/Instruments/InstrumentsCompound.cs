using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Instruments {
	public class InstrumentsCompound {
		public enum Type {
			CREATE_NODE,
			REMOVE_NODE,
			SELECT_AND_MOVE,
			CREATE_EDGE
		}

		public static IInstrument Get(Type type) {
			switch (type) {
				case Type.CREATE_NODE:
					return new _instruments.AddNode();
				case Type.REMOVE_NODE:
					return new _instruments.RemoveNode();
				case Type.SELECT_AND_MOVE:
					return new _instruments.SelectAndMove();
				case Type.CREATE_EDGE:

					break;
				default:
					Debug.LogError($"Возможности выбора инструмента: {type} не существует\n" +
						$"Проверте InstrumentCompound.GetInstrument");
					return null;
			}
			return null;
		}
	}
}