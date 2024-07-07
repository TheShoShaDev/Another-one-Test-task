using System;
using UnityEngine;

namespace Inventory
{
	public interface IReadOnlyInventory
	{
		event Action<string, int> ItemsAdded;
		event Action<string, int> ItemsRemoved;
		
		int GetAmount(string itemID);
		bool Has(string itemId, int Amount);
	}
}

