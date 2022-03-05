using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Components.CommandMemento.Command;

namespace Components.Graphs {
	public interface IMovable {
		public Vector2 position { get; }
		public void Move(Vector2 position);
		public ICommand[] GetCommands();
	}
}
