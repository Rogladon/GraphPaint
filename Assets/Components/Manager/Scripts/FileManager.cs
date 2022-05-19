using System.IO;
using System;
using Components.Graphs;
using UnityEngine;

namespace Components.Manager {
	public class FileManager {
		private Action<Graph> change;
		private Func<Graph> graph;
		public FileManager(Func<Graph> graph, Action<Graph> change) {
			this.graph = graph;
			this.change = change;
		}

		public void Open(string path) {
			if(!File.Exists(path)) {
				Debug.LogError($"FileManager: Don`t exist path: {path}");
				return;
			}
			StreamReader stream = new StreamReader(path);
			string str = stream.ReadToEnd();
			stream.Close();
			var g = new Graphs.File.ConvertColToGraph().Execute(str);
			change(g);
		}
	}
}
