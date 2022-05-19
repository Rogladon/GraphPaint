using UnityEngine;

namespace Components.Manager {
	/// <summary>
	/// Хранит все ссылки, которые из инспектора должны назначаться
	/// </summary>
	internal class ManagerHolder : MonoBehaviour {
		//Сюда все что ыло синглтонами

		private void Awake() {
			if(Manager._instance == null) {
				new Manager(this);
			} else {
				Debug.LogError($"Error! Второй ManagerHolder на сцене");
			}
		}
	}
}