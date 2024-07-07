using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryService
    {
		private InventoryGrid _inventory;

		public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
		{
			_inventory = new InventoryGrid(inventoryData);

			return _inventory;
		}

		public AddItemsToInventoryGridResult AddItemsToInventory(string itemId, int amount = 1)
		{
			return _inventory.AddItems(itemId, amount);
		}

		public AddItemsToInventoryGridResult AddItemsToInventory(Vector2Int slotCoords, string itemId, int amount = 1)
		{
			return _inventory.AddItems(slotCoords, itemId, amount);
		}

		public RemoveItemsFromInventoryGridResult RemoveItems(string itemId, int amount = 1)
		{
			return _inventory.RemoveItems(itemId, amount);
		}

		public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
		{
			return _inventory.RemoveItems(slotCoords, itemId, amount);
		}

		public Vector2Int GetItemIDCoords(string itemId, int amount = 1)
		{
			return _inventory.GetItemIDCoords(itemId, amount);
		}

		public void SwitchSlots(Vector2Int slotCoordA, Vector2Int slotCoordB)
		{
			_inventory.SwitchSlots(slotCoordA, slotCoordB);
		}

		public bool Has(string itemId, int amount)
		{
			return _inventory.Has(itemId, amount);
		}

		public IReadOnlyInventoryGrid GetInventory()
		{
			return _inventory;
		}
	}
}
