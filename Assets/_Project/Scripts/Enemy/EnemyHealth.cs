using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{	
	protected override void OnDecreseHealth(float value)
	{
		base.OnDecreseHealth(value);
		
		if(IsDead)
		{
			GetItemOnDead();
			Destroy(gameObject);
			gameObject.SetActive(false);
			EventBus.OnEnemyDied();
		}
	}

	protected virtual void GetItemOnDead()
	{
		EntryPoint entryPoint = FindObjectOfType<EntryPoint>();
		InventoryService inventoryService = entryPoint.InventoryService;
		inventoryService.GetInventory();

		var rItemID = ItemManager.Instance.GetRandomItemId();;
		inventoryService.AddItemsToInventory(rItemID, 1);

	}
}
