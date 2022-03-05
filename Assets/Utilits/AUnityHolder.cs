using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Абстрактный холдер, имеет только свойство возвращающее object
/// </summary>
public abstract class AUnityHolder : MonoBehaviour {
	public abstract object obj { get; }
}