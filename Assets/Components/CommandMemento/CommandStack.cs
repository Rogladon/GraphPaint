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
		private List<ACommand> commands = new List<ACommand>();

		public CommandStack(params ACommand[] commands) {
			this.commands = commands.ToList();
		}

		protected internal override void OnExecute() {
			commands.ForEach(p => p.OnExecute());
		}
	}
}
