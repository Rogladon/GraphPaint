using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Algoritm;

namespace Components.HUD {
	public class AlgoritmHUD : MonoBehaviour {
		[System.Serializable]
		private class Button {
			public UnityEngine.UI.Button btn;
			public AlgoritmType type;

			public void Start(System.Action<AlgoritmType> action) {
				btn.onClick.AddListener(() => action(type));
			}
		}
		#region Fields
		[SerializeField] private List<Button> buttons;
		#endregion

		#region Properties

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods
		private void Start() {
			buttons.ForEach(p => p.Start(ClickAlgoritm));
		}
		private void ClickAlgoritm(AlgoritmType type) {
			var res = AlgoritmManager.instance.ExecuteDrawAlgoritm(type);

			if (res.status == ResStatus.EXECUTED) {
				HUD.StaticHUD.Log("Алгоритм", $"Хроматическое число = {res.chromaticNumber}");
			}
		}
		#endregion
	}
}