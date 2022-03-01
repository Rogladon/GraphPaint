using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Components.CommandMemento.Command {
	/// <summary>
	/// Менеджре паттерна Command
	/// </summary>
	public class CommandManager {
		/// <summary>
		/// Через какой промежуток будет создаваться бекап
		/// </summary>
		private const int BACKUP_MULTIPLICITY = 5;

		#region Singlton
		private static CommandManager _instance;
		public static CommandManager instance {
			get {
				if (_instance == null)
					_instance = new CommandManager();
				return _instance;
			}
		}
		#endregion

		private List<ICommand> commands = new List<ICommand>();
		private bool lockCM = false;
		/// <summary>
		/// Регистрация команды
		/// </summary>
		/// <param name="command"></param>
		public void Registory(ICommand command) {
			if (lockCM) return;
			if (commands.Count % BACKUP_MULTIPLICITY == 0) commands.Add(new Backup());
			commands.Add(command);
		}
		/// <summary>
		/// Назад
		/// </summary>
		public void Undo() {
			if (commands.Count <= 1) return;
			lockCM = true;
			commands.RemoveAt(commands.Count - 1);
			int lastBackupId = commands.FindLastIndex(p => p is Backup);
			if (lastBackupId == commands.Count - 1)
				Undo();
			else
				commands.Take(lastBackupId, commands.Count).ForEach(p => p.Execute());
			lockCM = false;
		}

		#region Commands
		/// <summary>
		/// Создает слепок всего мементо менеджера со всеми объектами
		/// </summary>
		private class Backup: ICommand {
			private Memento.IMemento memento = Memento.MementoManager.instance.CreateMemento();

			public void Execute() { memento.Restore(); }
		}
		#endregion
	}
}
