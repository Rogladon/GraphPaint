using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Components.HUD{
	public class MessageBox : MonoBehaviour {

		#region Fields
		[SerializeField] private TextMeshProUGUI tittle;
		[SerializeField] private TextMeshProUGUI message;
		[SerializeField] private TextMeshProUGUI txtButtonOk;
		[SerializeField] private TextMeshProUGUI txtButtonCancel;
		[SerializeField] private Button buttonOk;
		[SerializeField] private Button buttonCancel;
		#endregion

		#region Properties

		#endregion

		#region Public Methods
		public MessageBox Clear() {
			tittle.text = "Tittle";
			message.text = "Message";
			txtButtonOk.text = "Ok";
			txtButtonCancel.text = "Cancel";
			buttonCancel.onClick.RemoveAllListeners();
			buttonOk.onClick.RemoveAllListeners();
			return this;
		}

		public MessageBox SetTittle(string tittle) {
			this.tittle.text = tittle;
			return this;
		}
		public MessageBox SetMessage(string msg) {
			message.text = msg;
			return this;
		}
		public MessageBox SetTxtButtonOk(string txt) {
			txtButtonOk.text = txt;
			return this;
		}
		public MessageBox SetTxtButtonCancel(string txt) {
			txtButtonCancel.text = txt;
			return this;
		}
		public MessageBox SetActionOk(Action action) {
			if (action != null)
				buttonOk.onClick.AddListener(() => action());
			return this;
		}
		public MessageBox SetActionCancel(Action action) {
			if(action != null)
				buttonCancel.onClick.AddListener(() => action());
			return this;
		}
		public void Show() {
			buttonOk.onClick.AddListener(() => {
				gameObject.SetActive(false);
				Clear();
			});
			buttonOk.onClick.AddListener(() => {
				gameObject.SetActive(false);
				Clear();
			});
			gameObject.SetActive(true);
		}
		public void ShowLog() {
			Show();
			buttonCancel.gameObject.SetActive(false);
		}
		#endregion
	}
}