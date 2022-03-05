using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Абстрактный холдер для типа T, Имеет сериализованное поле для типа и свойство
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class UnityHolder<T> : AUnityHolder{
	[SerializeField] protected T _component;
	public virtual T component => _component;
	public override object obj => _component;
}