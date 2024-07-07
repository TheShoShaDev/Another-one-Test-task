using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
	public class InventoryGrid : IReadOnlyInventoryGrid
	{
		public event Action<string, int> ItemsAdded;
		public event Action<string, int> ItemsRemoved;
		public event Action<Vector2Int> SizeChanged;

		public Vector2Int Size
		{
			get => _data.Size;
			set
			{
				if (_data.Size != value)
				{
					_data.Size = value;
					SizeChanged?.Invoke(value);
				}
			}
		}

		private readonly InventoryGridData _data;
		private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

		public InventoryGrid(InventoryGridData data)
		{
			_data = data;
			
			Vector2Int size = data.Size;
			for (int i = 0; i < size.x; i++)
			{
				for (int j = 0; j < size.y; j++)
				{
					int index = i * size.y + j;
					InventorySlotData slotData = data.Slots[index];
					InventorySlot slot = new InventorySlot(slotData);
					Vector2Int position = new Vector2Int(i, j);

					_slotsMap[position] = slot;

				}
			}

		}

		public int GetAmount(string itemID)
		{
			int amount = 0;
			var slots = _data.Slots;

			foreach (var slot in slots)
			{
				if (slot.ItemID == itemID)
				{
					amount += slot.Amount;
				}
			}

			return amount;
		}

		public bool Has(string itemId, int amount)
		{
			return GetAmount(itemId) >= amount;
		}

		public void SwitchSlots(Vector2Int slotCoordA, Vector2Int slotCoordB)
		{
			var slotA = _slotsMap[slotCoordA];
			var slotB = _slotsMap[slotCoordB];

			string tempSlotItemsId = slotA.ItemID;
			int tempSlotitemAmount = slotA.Amount;

			slotA.ItemID = slotB.ItemID;
			slotA.Amount = slotB.Amount;
			slotB.ItemID = tempSlotItemsId;
			slotB.Amount = tempSlotitemAmount;
		}

		public IReadOnlyInventorySlot[,] GetSlots()
		{
			var array = new IReadOnlyInventorySlot[Size.x, Size.y];
			for (int i = 0; i < Size.x; i++)
			{
				for (int j = 0; j < Size.y; j++)
				{
					Vector2Int position = new Vector2Int(i, j);
					array[i, j] = _slotsMap[position];
				}
			}

			return array;
			
		}

		public AddItemsToInventoryGridResult AddItems(string itemID, int amount = 1)
		{
			int remainingAmount = amount;
			int itemAddedToSlotWithSameItemsAmount = AddToSlotWithSameItemsAmount(itemID, remainingAmount, out remainingAmount);

			if (remainingAmount <= 0)
			{
				return new AddItemsToInventoryGridResult(amount, itemAddedToSlotWithSameItemsAmount);
			}

			int itemsAddedToAvailableSlotAmount = AddToFirstAvailableSlots(itemID, remainingAmount, out remainingAmount);
			int totalAddedItemsAmount = itemAddedToSlotWithSameItemsAmount + itemsAddedToAvailableSlotAmount;

			return new AddItemsToInventoryGridResult(amount, totalAddedItemsAmount);
		}

		public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1)
		{
			var slot = _slotsMap[slotCoords];
			int newValue = slot.Amount + amount;
			int itemsAddedAmount = 0;
			
			if (slot.IsEmpty)
			{
				slot.ItemID = itemId;
			}

			var itemSlotCapacity = GetItemSlotCapacity(itemId);

			if (newValue > itemSlotCapacity)
			{
				int remainingItems = newValue - itemSlotCapacity;
				int itemsToAddAmount = itemSlotCapacity - slot.Amount;
				itemsAddedAmount += itemsToAddAmount;
				slot.Amount = itemSlotCapacity;

				AddItemsToInventoryGridResult result = AddItems(itemId, remainingItems);
				itemsAddedAmount += result.ItemsToAddAmount;
			}
			else
			{
				itemsAddedAmount = amount;
				slot.Amount = newValue;
			}

			return new AddItemsToInventoryGridResult(amount, itemsAddedAmount);
		}	

		public RemoveItemsFromInventoryGridResult RemoveItems(string itemID, int amount = 1)
		{
			if (!Has(itemID, amount))
			{
				return new RemoveItemsFromInventoryGridResult(amount, false);
			}

			int amountToRemove = amount;

			for (int i = 0; i < Size.x; i++)
			{
				for (int j = 0; j < Size.y; j++)
				{
					Vector2Int slotCoords = new Vector2Int(i, j);
					var slot = _slotsMap[slotCoords];

					if (slot.ItemID != itemID)
					{
						continue;
					}

					if (amountToRemove > slot.Amount)
					{
						amountToRemove -= slot.Amount;

						RemoveItems(slotCoords, itemID, slot.Amount);

					}
					else
					{
						RemoveItems(slotCoords, itemID, amountToRemove);

						return new RemoveItemsFromInventoryGridResult(amountToRemove, true);
					}
				}
			}

			throw new Exception($"Something went wrong, couldn't remove some items ({itemID})");

		}

		public Vector2Int GetItemIDCoords(string itemId,int amount = 1)
		{
			if (!Has(itemId, amount))
			{
				return new Vector2Int(40,40);
			}

			for (int i = 0; i < Size.x; i++)
			{
				for (int j = 0; j < Size.y; j++)
				{
					Vector2Int slotCoords = new Vector2Int(i, j);
					var slot = _slotsMap[slotCoords];

					if (slot.ItemID != itemId)
					{
						continue;
					}

					if (amount <= slot.Amount)
					{
						return slotCoords;
					}
				}

			}

			return new Vector2Int(40, 40);

		}

		public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
		{
			var slot = _slotsMap[slotCoords];

			if (slot.IsEmpty || slot.ItemID != itemId || slot.Amount < amount)
			{
				return new RemoveItemsFromInventoryGridResult(amount, false);
			}

			slot.Amount -= amount;

			if (slot.Amount == 0)
			{
				slot.ItemID = null;
			}

			return new RemoveItemsFromInventoryGridResult(amount, true);

		}

		private int GetItemSlotCapacity(string itemID)
		{
			return ItemManager.Instance.GetItemConfig(itemID).MaxCapacity;
		}

		private int AddToSlotWithSameItemsAmount(string itemID, int amount, out int remainingAmount)
		{
			int itemsAddedAmount = 0;
			remainingAmount = amount;

			for (int i = 0; i < Size.x; i++)
			{
				for (int j = 0; j < Size.y; j++)
				{
					Vector2Int coords = new Vector2Int(i, j);
					var slot = _slotsMap[coords];

					if (slot.IsEmpty)
					{
						continue;
					}

					int slotItemCapacity = GetItemSlotCapacity(slot.ItemID);
					if (slot.Amount >= slotItemCapacity || slot.ItemID != itemID)
					{
						continue;
					}

					int newValue = slot.Amount + remainingAmount;

					if (newValue > slotItemCapacity)
					{
						remainingAmount = newValue - slotItemCapacity;
						int itemsToAddAmount = slotItemCapacity - slot.Amount;
						itemsAddedAmount += itemsToAddAmount;
						slot.Amount = slotItemCapacity;

						if (remainingAmount == 0)
						{
							return itemsAddedAmount;
						}
					}
					else
					{
						itemsAddedAmount += remainingAmount;
						slot.Amount = newValue;
						remainingAmount = 0;

						return itemsAddedAmount;
					}
	
				}
			}

			return itemsAddedAmount;
		}

		private int AddToFirstAvailableSlots(string itemID, int amount, out int remainingAmount)
		{
			int itemsAddedAmount = 0;
			remainingAmount = amount;

			for (int i = 0; i < Size.x; i++)
			{
				for (int j = 0; j < Size.y; j++)
				{
					Vector2Int coords = new Vector2Int(i, j);
					var slot = _slotsMap[coords];

					if (!slot.IsEmpty)
					{
						continue;
					}

					slot.ItemID = itemID;
					int newValue = remainingAmount;
					int slotItemCapacity = GetItemSlotCapacity(slot.ItemID);

                    if (newValue > slotItemCapacity)
                    {
						remainingAmount = newValue - slotItemCapacity;
						int itemsToAddAmount = slotItemCapacity;
						itemsAddedAmount += itemsToAddAmount;
						slot.Amount = slotItemCapacity;
                    }
					else
					{
						itemsAddedAmount += remainingAmount;
						slot.Amount = newValue;
						remainingAmount = 0;

						return itemsAddedAmount;
					}
                }
			}

			return itemsAddedAmount;
		}

	}

}