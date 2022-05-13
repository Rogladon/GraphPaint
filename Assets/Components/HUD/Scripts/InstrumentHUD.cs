using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Components.Instruments;
using Components.CommandMemento.Command;
using Components.CommandMemento.Memento;

namespace Components.HUD {
    public class InstrumentHUD : MonoBehaviour, IMementable {
		#region classes
		[System.Serializable]
		private class InstrumentButton {
			[SerializeField] private Button _button;
			[SerializeField] private InstrumentType _type;

			private bool isActive = false;

			public Button button => _button;
			public InstrumentType type => _type;

			public void Start(System.Action<InstrumentType> setter) {
				if (InstrumentManager.instance.ContainsInstrument(type))
					button.onClick.AddListener(() => setter(type));
				else
					button.interactable = false;
			}
			private void Active(bool active) {
				isActive = active;
				if (active) button.targetGraphic.color = button.colors.selectedColor;
				else button.targetGraphic.color = button.colors.normalColor;
			}
		}
		#endregion

		#region Fields
		[SerializeField] private List<InstrumentButton> buttons;
		#endregion

		#region Public Methods
		public void SetInstrument(InstrumentType type) {

		}
		#endregion

		#region Command
		private class CmdSetInstruments : ACommand {
			public InstrumentType type;
			protected override void OnExecute() {
				InstrumentManager.instance.SetInstrumentLkm(type);
			}
		}
		#endregion

		#region Memento
		public IMemento CreateMemento() {
			throw new System.NotImplementedException();
		}

		private class Memento : IMemento {

			public void Restore() {
				throw new System.NotImplementedException();
			}
		}
		#endregion
	}
}
