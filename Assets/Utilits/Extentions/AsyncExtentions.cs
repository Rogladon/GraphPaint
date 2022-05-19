using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class AsyncExtention {
	public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp) {
		var tcs = new TaskCompletionSource<object>();
		asyncOp.completed += obj => { tcs.SetResult(null); };
		return ((Task)tcs.Task).GetAwaiter();
	}
}
