﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Algoritm.DrawGraph;

namespace Components.Algoritm {
	public enum AlgoritmType {
		SIMPLE_DRAW,
	}
	public class AlgoritmManager : MonoBehaviour {
		#region Singlton
		private static AlgoritmManager _instance;
		public static AlgoritmManager instance {
			get {
				if (_instance == null)
					_instance = new GameObject("NewAlgoritmManager").AddComponent<AlgoritmManager>();
				return _instance;
			}
		}
		#endregion

		#region Fields

		#endregion

		#region Properties
		private Graphs.Graph graph => Instruments.InstrumentManager.instance.graph;
		#endregion

		#region Public Methods
		public ResultDraw ExecuteDrawAlgoritm(AlgoritmType type) { //TOODOO как то придумать, чтоб возвращало нужный тип, но название функции одно
			ResultDraw res;
			switch (type) {
				case AlgoritmType.SIMPLE_DRAW:
					res = new SimpleDraw().Execute(graph);
					break;
				default:
					res = null;
					break;
			}
			if (res != null) res.ExecuteCommand();
			return res;
		}

		#endregion

		#region Private Methods
		private void Awake() {
			if (_instance) {
				Debug.LogError($"Второй instrumentManager: {name}");
				Destroy(this);
				return;
			}
			_instance = this;

		}
		#endregion
	}
}