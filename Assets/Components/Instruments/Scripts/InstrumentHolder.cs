using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Instruments {
	/// <summary>
	/// UnityHolder для IInstrument, Без сериализванного поля типа
	/// </summary>
	public abstract class InstrumentHolder : ScriptableHolder<IInstrument> {

	}
}