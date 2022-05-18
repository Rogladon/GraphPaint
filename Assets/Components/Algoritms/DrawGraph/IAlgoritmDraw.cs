using Components.CommandMemento.Command;
using UnityEngine;

namespace Components.Algoritm.DrawGraph {
	public interface IAlgoritmDraw : IAlgoritm<ResultDraw> {
	}
	public class ResultDraw : IResult {
		private ResStatus _status;
		private int _chromaticNumber;
		private ICommand _command;

		public ResStatus status => _status;

		public int chromaticNumber => _chromaticNumber;

		public static ResultDraw Failed => new ResultDraw() {
			_status = ResStatus.FAILED,
			_chromaticNumber = -1
		};

		

		public ResultDraw(int number,ICommand command) {
			_chromaticNumber = number;
			_status = ResStatus.READY;
			_command = command;
		}
		private ResultDraw() { }

		public void ExecuteCommand() {
			try {
				_command.Execute();
				_status = ResStatus.EXECUTED;
			} catch {
				_status = ResStatus.FAILED;
				Debug.Log($"Failed Execute alg.");
			}			
		}
	}
}
