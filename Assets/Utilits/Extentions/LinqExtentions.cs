using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class LinqExtentions {
	public static IEnumerable<T> ForEach<T>(this IEnumerable<T> e, Action<T> action) {
		foreach(var i in e) {
			action(i);
		}
		return e;
	}
	public static IEnumerable<T> Take<T>(this IEnumerable<T> e, int start, int finish) {
		return e.Where((p, i) => i >= start && i < finish);
	}

	public static IEnumerable<T> Copy<T>(this IEnumerable<T> e) {
		return Enumerable.Range(0, e.Count()).Select(p => e.ElementAt(p));
	}
}
