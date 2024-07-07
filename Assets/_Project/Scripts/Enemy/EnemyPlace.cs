using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlace : MonoBehaviour
{
	[SerializeField] private Enemy _currentEnemy;

	public event Action<Entity> EnemyPlaced;

	public void SetEnemy(Enemy enemy)
	{
		if(_currentEnemy != null)
		{
			RemoveEnemy(_currentEnemy.gameObject);
		}
		_currentEnemy = Instantiate(enemy, transform);
		EnemyPlaced?.Invoke(_currentEnemy);
	}

	private void RemoveEnemy(GameObject enemy)
	{
		Destroy(enemy);
	}
}
