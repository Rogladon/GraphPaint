using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Memento {
	/// <summary>
	/// Запоминаемый объект
	/// </summary>
	public interface IMementable {
		/// <summary>
		/// Создание слепка
		/// </summary>
		/// <returns></returns>
		public IMemento CreateMemento();
	}
}
