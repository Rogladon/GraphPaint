using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Algoritm {
    public enum ResStatus {
        READY = 0,
        EXECUTED = 1,
        FAILED = 2,
	}
    public interface IAlgoritm<res> where res:IResult {
        public res Execute(Graphs.Graph graph);
    }
    public interface IResult {
        public ResStatus status { get; }
        public void ExecuteCommand();
	}
}
