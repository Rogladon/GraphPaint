using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Memento {
	/// <summary>
	/// Слепок запоминаемого объекта
	/// </summary>
	public interface IMemento {
		/// <summary>
		/// Возврат к сохраненному состоянию
		/// </summary>
		public void Restore();
	}
}
