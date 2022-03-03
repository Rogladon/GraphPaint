using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Creator {
	public static T Create<T>() where T: MonoBehaviour {
		return new GameObject($"{typeof(T).Name}").AddComponent<T>();
	}
}
