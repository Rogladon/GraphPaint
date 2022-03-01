using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.CommandMemento.Memento {
	/// <summary>
	/// Менеджер паттерна Memento
	/// </summary>
	public class MementoManager : IMementable {
		#region Singlton
		private static MementoManager _instance;
		public static MementoManager instance {
			get {
				if (_instance == null)
					_instance = new MementoManager();
				return _instance;
			}
		}
		#endregion

		private List<IMementable> mementables = new List<IMementable>();
		/// <summary>
		/// Регистрация нового IMementable объекта
		/// </summary>
		/// <param name="mementable"></param>
		public void Registry(IMementable mementable) {
			mementables.Add(mementable);
		}

		#region Mementable
		public IMemento CreateMemento() {
			return new Memento() { 
				owner = this,
				mementables = mementables,
				mementos = mementables.Select(p => p.CreateMemento()).ToList()
			};
		}

		private class Memento : IMemento {
			public MementoManager owner;
			public List<IMementable> mementables;
			public List<IMemento> mementos;

			public void Restore() {
				owner.mementables.Clear();
				owner.mementables.AddRange(mementables);
				mementos.ForEach(p => p.Restore());
			}
		}
		#endregion
	}
}
