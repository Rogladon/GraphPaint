using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Utilits {
	public class Entry {
		private string _path;
		private DirectoryInfo dir;
		private Stack<string> history = new Stack<string>();
		private Stack<string> historyRedo = new Stack<string>();

		public string path => _path;
		public bool isRedo => historyRedo.Count > 0;
		public bool isUndo => history.Count > 0;
		public Entry() {
			dir = null;
		}
		
		public void OpenFolder(string path) {
			historyRedo.Clear();
			history.Push(this.path);
			UpdateDirectory(path);
		}
		public DirectoryInfo[] GetDirectiries() => dir != null
			? dir.GetDirectories()
			: DriveInfo.GetDrives().Select(p => p.RootDirectory).ToArray();
		public FileInfo[] GetFiles() => dir != null
			? dir.GetFiles("*.col")
			: new FileInfo[0];
		public void Undo() {
			if (history.Count > 0) {
				historyRedo.Push(path);
				UpdateDirectory(history.Pop());
			} else
				dir = null;
		}
		public void Redo() {
			if (historyRedo.Count > 0) {
				history.Push(path);
				UpdateDirectory(historyRedo.Pop());
			}
		}
		public bool Exist(string name) => File.Exists(GetFullPath(name));
		public string GetFullPath(string name) => Path.Combine(path, name);
		private void UpdateDirectory(string path) {
			_path = path;
			dir = path == null ? null : Directory.CreateDirectory(path);
		}
	}
}
