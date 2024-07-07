using System;
using UnityEngine;
using ItemsEnums;

namespace Inventory
{
	public interface IReadOnlyInventorySlot
	{
		event Action<string> ItemIDChanged;
		event Action<int> ItemAmountChanged;

		string ItemID { get; }
		int Amount { get; }
		bool IsEmpty {  get; }
		Sprite Image { get; }
		ItemType ItemType { get; }
	}
}
