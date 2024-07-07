using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
	[SerializeField] private EnemyConfig _enemyConfig;

	private void Start()
	{
		_currentWeapon = _enemyConfig.Weapon;
		_target = FindObjectOfType<Player>();
	}

	public override void GetDamage(float value)
	{
		base.GetDamage(value);

		if(gameObject.activeSelf != false)
		{
			DealDamage();
		}
	}
}
