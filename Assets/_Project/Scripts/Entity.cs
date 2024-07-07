using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageble
{
	public event Action<float> GotDamage;

	[SerializeField] protected Entity _target;
	[SerializeField] protected Weapon _currentWeapon;

	public virtual void DealDamage()
	{
		_currentWeapon.DealDamage(_target);
	}

	public virtual void GetDamage(float value)
	{
		GotDamage?.Invoke(value);
	}
}
