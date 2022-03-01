using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Command {
	/// <summary>
	/// Команда для выполнения
	/// </summary>
	public interface ICommand {
		/// <summary>
		/// Выполнить
		/// </summary>
		public void Execute();
	}
}
