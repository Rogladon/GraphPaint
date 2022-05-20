using System.Threading.Tasks;

namespace Components.Algoritm {
    public enum ResStatus {
        READY = 0,
        EXECUTED = 1,
        FAILED = 2,
	}
    //TOODOO Заменить потом на мою систему асинхронных операций (из TryToSurvive)
    public interface IAlgoritm<res> where res:IResult {
        public Task<res> Execute(Graphs.Graph graph);
    }
    public interface IResult {
        public ResStatus status { get; }
        public double time { get; }
        public void ExecuteCommand();
	}
}
