using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	[SerializeField] private EnemyPlace _enemyPlace;

	public void ChangeWeapon(Weapon weapon)
	{
		_currentWeapon = weapon;
	}

	public override void DealDamage()
	{
		EntryPoint entryPoint = FindObjectOfType<EntryPoint>();
		InventoryService inventoryService = entryPoint.InventoryService;
		inventoryService.GetInventory();
		Vector2Int coords = inventoryService.GetItemIDCoords(_currentWeapon.AmmoId, (int)_currentWeapon.Ammo—onsumption);
		if (coords != new Vector2Int(40, 40))
		{
			inventoryService.RemoveItems(coords, _currentWeapon.AmmoId, (int)_currentWeapon.Ammo—onsumption);
			base.DealDamage();
		}
	}

	public void SetTarget(Entity target)
	{
		_target = target;
	}

	private void OnEnable()
	{
		_enemyPlace.EnemyPlaced += SetTarget;
	}

	private void OnDisable()
	{
		_enemyPlace.EnemyPlaced -= SetTarget;
	}

}
