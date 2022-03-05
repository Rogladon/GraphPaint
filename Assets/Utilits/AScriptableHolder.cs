using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Абстрактный холдер для ScriptableObject, имеет только свойство возвращающее object
/// </summary>
public abstract class AScriptableHolder : ScriptableObject {
	public abstract object obj { get; }
}