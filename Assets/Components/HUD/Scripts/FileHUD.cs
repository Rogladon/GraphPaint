using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Utilits;

namespace Components.HUD{
	public class FileHUD : MonoBehaviour {

		#region Fields
		[Header("Prefabs")]
		[SerializeField] private Button btnFolder;
		[SerializeField] private Button btnFile;
		[Header("Components")]
		[SerializeField] private Transform dirSocket;
		[SerializeField] private TMP_InputField path;
		[SerializeField] private TMP_InputField fileName;
		[Header("Buttons")]
		[SerializeField] private Button btnUndo;
		[SerializeField] private Button btnRedo;
		[SerializeField] private Button btnOk;
		[SerializeField] private Button btnCancel;

		private List<GameObject> buttons = new List<GameObject>();
		private Entry entry;
		#endregion

		#region Properties
		public event System.Action<string> OnOk;
		#endregion

		#region Public Methods
		public void Show() {
			gameObject.SetActive(true);
		}
		#endregion

		#region Private Methods
		private void Start() {
			//TOODOO пока так, потом передлать по нормальному
			OnOk += (str) => Manager.Manager.instance.file.Open(str);
			UpdateDir();
			btnUndo.onClick.AddListener(() => Undo());
			btnRedo.onClick.AddListener(() => Redo());
			btnOk.onClick.AddListener(() => Ok());
			btnCancel.onClick.AddListener(() => Cancel());
		}
		private void UpdateDir() {
			if (entry == null) entry = new Entry();
			buttons.ForEach(p => Destroy(p));
			buttons.Clear();

			btnUndo.interactable = entry.isUndo;
			btnRedo.interactable = entry.isRedo;

			path.text = entry.path;

			entry.GetDirectiries().ForEach(p => buttons.Add(CreateBtnDirectory(p).gameObject));

			entry.GetFiles().ForEach(p => buttons.Add(CreateBtnFile(p).gameObject));
		}

		private Button CreateBtnDirectory(System.IO.DirectoryInfo dir) {
			var btn = Instantiate(btnFolder, dirSocket);
			btn.onClick.AddListener(() => {
				entry.OpenFolder(dir.FullName);
				UpdateDir();
			});
			btn.GetComponentInChildren<TextMeshProUGUI>().text = dir.Name;
			return btn;
		}
		private Button CreateBtnFile(System.IO.FileInfo file) {
			var btn = Instantiate(btnFolder, dirSocket);
			btn.onClick.AddListener(() => {
				fileName.text = file.Name;
			});
			btn.GetComponentInChildren<TextMeshProUGUI>().text = file.Name;
			return btn;
		}
		private void Undo() {
			entry.Undo();
			UpdateDir();
		}
		private void Redo() {
			entry.Redo();
			UpdateDir();
		}
		private void Ok() {
			if (!entry.Exist(fileName.text)) {
				HUD.StaticHUD.Error("File Error!", "Такого файла не существует!");
				return;
			}
			gameObject.SetActive(false);
			OnOk(entry.GetFullPath(fileName.text));
		}
		private void Cancel() {
			gameObject.SetActive(false);
		}
		#endregion
	}
}