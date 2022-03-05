using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Instruments {
	public enum InstrumentType {
		CREATE_NODE,
		REMOVE_NODE,
		SELECT_AND_MOVE,
		CREATE_EDGE,
		MOVE,
		SELECT
	}
	[CreateAssetMenu(fileName ="InstrumentCompound", menuName ="InstrumentsCompound", order =0)]
	public class InstrumentsCompound : ScriptableObject{
		[SerializeField] private List<InstrumentHolder> instruments;

		public IInstrument this[InstrumentType type] {
			get {
				var holder = instruments.FirstOrDefault(p => p.component.GetType() == GetInstrumentType(type));
				if (holder != null) return holder.component;
				Debug.LogError($"{this}:: Нет инструмента типа {type}");
				return null;
			}
		}

		public bool TryGetInstrument(InstrumentType type, out IInstrument instrument) {
			var holder = instruments.FirstOrDefault(p => p.GetType() == GetInstrumentType(type));
			if (holder != null) {
				instrument = holder.component;
				return true;
			}
			instrument = null;
			return false;			
		}
		public bool Contains(InstrumentType type) {
			var holder = instruments.FirstOrDefault(p => p.GetType() == GetInstrumentType(type));
			return holder != null;
		}

		public System.Type GetInstrumentType(InstrumentType type) {
			switch (type) {
				case InstrumentType.CREATE_NODE:
					return typeof(_instruments.AddNode);
				case InstrumentType.REMOVE_NODE:
					return typeof(_instruments.RemoveNode);
				case InstrumentType.SELECT_AND_MOVE:
					return typeof(_instruments.SelectAndMove);
				case InstrumentType.CREATE_EDGE:
					return typeof(_instruments.AddEdge);
				case InstrumentType.MOVE:
					return typeof(_instruments.Move);
				case InstrumentType.SELECT:
					return null;
				default:
					Debug.LogError($"Возможности выбора инструмента: {type} не существует\n" +
						$"Проверте InstrumentCompound.GetInstrument");
					return null;
			}
		}
		//public static IInstrument Get(Type type) {
		//	switch (type) {
		//		case Type.CREATE_NODE:
		//			return new _instruments.AddNode();
		//		case Type.REMOVE_NODE:
		//			return new _instruments.RemoveNode();
		//		case Type.SELECT_AND_MOVE:
		//			return new _instruments.SelectAndMove();
		//		case Type.CREATE_EDGE:

		//			break;
		//		default:
		//			Debug.LogError($"Возможности выбора инструмента: {type} не существует\n" +
		//				$"Проверте InstrumentCompound.GetInstrument");
		//			return null;
		//	}
		//	return null;
		//}
	}
}