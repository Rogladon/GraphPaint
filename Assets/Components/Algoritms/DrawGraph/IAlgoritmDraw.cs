using Components.CommandMemento.Command;
using Components.Graphs;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;

namespace Components.Algoritm.DrawGraph {
	public interface IAlgoritmDraw : IAlgoritm<ResultDraw> {
	}
	public abstract class AAlgoritmDraw : IAlgoritmDraw {
		public async Task<ResultDraw> Execute(Graph graph) {
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			var res = await OnExecute(graph);
			stopwatch.Stop();
			return new ResultDraw(res.chromaticNumber, res.command, stopwatch.ElapsedMilliseconds);
		}
		protected abstract Task<ResultDraw> OnExecute(Graph graph);
	}
	public class ResultDraw : IResult {
		private ResStatus _status;
		private int _chromaticNumber;
		private ICommand _command;
		private double _time;

		internal ICommand command => _command;
		public ResStatus status => _status;
		public double time => _time;
		public int chromaticNumber => _chromaticNumber;

		public static ResultDraw Failed => new ResultDraw() {
			_status = ResStatus.FAILED,
			_chromaticNumber = -1,
			_time = -1
		};

		

		public ResultDraw(int number, ICommand command, double time = -1) {
			_chromaticNumber = number;
			_status = ResStatus.READY;
			_command = command;
			_time = time;
		}
		private ResultDraw() { }

		public void ExecuteCommand() {
			try {
				_command.Execute();
				_status = ResStatus.EXECUTED;
			} catch {
				_status = ResStatus.FAILED;
				UnityEngine.Debug.Log($"Failed Execute alg.");
			}			
		}
		public override string ToString() {
			return $"Статус выполения - {status}\n" +
				$"Хроматическое число = {chromaticNumber}\n" +
				(time != -1 ? $"Время выполенения = {time}" : "");
		}
	}
}
