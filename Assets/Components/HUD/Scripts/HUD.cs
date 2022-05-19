using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Components.HUD{
	public class HUD : MonoBehaviour {
		private static HUD _instance;

		#region Fields
		[SerializeField] private MessageBox messageBox;
		[SerializeField] private MessageBox errorBox;
		[SerializeField] private GameObject loader;
		#endregion

		private void Awake() {
			_instance = this;
		}

		public class StaticHUD {
			public static void Log(string title, string message,
				string buttonOk = "Ok", Action actionOk = null) {
				if (!IsHud()) return;
				_instance.messageBox
					.SetTittle(title)
					.SetMessage(message)
					.SetTxtButtonOk(buttonOk)
					.SetActionOk(actionOk)
					.ShowLog();
			}
			//TOODOO пока так, потом сделать отдельный объект
			public static void Error(string title, string message,
				string buttonOk = "Ok") => Log(title, message, buttonOk);

			public static void Loader(bool active) => _instance.loader.SetActive(active);

			private static bool IsHud() {
				if(_instance == null) {
					Debug.LogError($"Error: Dont instance HUD");
					return false;
				}
				return true;
			}
		}
	}

	
}
