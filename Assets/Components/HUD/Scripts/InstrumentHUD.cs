using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Components.Instruments;
using Components.CommandMemento.Command;
using Components.CommandMemento.Memento;
using System.Linq;

namespace Components.HUD {
    public class InstrumentHUD : MonoBehaviour {
		#region classes
		[System.Serializable]
		private class InstrumentButton {
			[SerializeField] private Button _button;
			[SerializeField] private InstrumentType _type;

			private bool isActive = false;
			private bool disabled = false;

			public Button button => _button;
			public InstrumentType type => _type;

			public void Start(System.Action<InstrumentType> setter) {
				Debug.Log($"start {button}");
				if (InstrumentManager.instance.ContainsInstrument(type)) {
					Debug.Log($"find type: {type}");
					button.onClick.AddListener(() => setter(type));
					var colors = button.colors;
					colors.disabledColor = colors.selectedColor;
					button.colors = colors;
				} else {
					Debug.Log($"unfaind instr {type}");
					disabled = true;
					button.interactable = false;
				}
			}
			public void Active(bool active) {
				if (disabled) return;
				isActive = active;
				button.interactable = !active;
			}
		}
		#endregion

		#region Fields
		[SerializeField] private List<InstrumentButton> buttons;
		#endregion

		private void Start() {
			buttons.ForEach(p => p.Start(SetInstrument));
		}

		#region Public Methods
		public void SetInstrument(InstrumentType type) {
			InstrumentManager.instance.SetInstrumentLkm(type);
			buttons.Where(p => p.type != type).ForEach(p => p.Active(false));
			buttons.First(p => p.type == type).Active(true);
		}
		#endregion
	}
}
