using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Command {
	/// <summary>
	/// Выполняет последовательно набор команд без регистрации их в менеджере (как одну)
	/// </summary>
	public class CommandStack : ACommand {
		private List<ICommand> commands = new List<ICommand>();

		public CommandStack(params ICommand[] commands) {
			this.commands = commands.ToList();
		}

		protected internal override void OnExecute() {
			CommandManager.instance.Registory(this);
			commands.ForEach(p => p.Execute());
		}
	}
}
