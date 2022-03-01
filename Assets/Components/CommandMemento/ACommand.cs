using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Command {
	/// <summary>
	/// Команда (регистрация при Execute)
	/// </summary>
	public abstract class ACommand : ICommand {
		public void Execute() {
			CommandManager.instance.Registory(this);
			OnExecute();
		}
		/// <summary>
		/// Срабатывает при выполнении после регистрации
		/// </summary>
		protected abstract void OnExecute();
	}
}
