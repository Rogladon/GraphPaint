using UnityEngine;

namespace Components.Manager {

	/// <summary>
	/// Общий менеджер программы
	/// </summary>
    public class Manager {
		#region Singlton
		internal static Manager _instance;
		public static Manager instance {
			get {
				if(_instance == null) {
					var mgh = new GameObject("ManagerHolder").AddComponent<ManagerHolder>();
					new Manager(mgh);
				}
				return _instance;
			}
		}
		#endregion

		#region Fields
		private ManagerHolder _mgh;
		private FileManager _fmg;

		private Graphs.Graph _graph;
		#endregion

		#region Properties
		public FileManager file => _fmg;
		public Graphs.Graph graph => _graph;
		#endregion

		internal Manager(ManagerHolder mgh) {
			_instance = this;
			_mgh = mgh;
			_graph = new Graphs.Graph();
			_fmg = new FileManager(() => graph, g => {
				_graph.Destroy();
				_graph = g;
			});
		}
	}
}