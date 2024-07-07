using UnityEngine;

namespace Inventory
{
	public class InventoryGridView : MonoBehaviour
	{
		public void Setup(IReadOnlyInventoryGrid inventory)
		{
			IReadOnlyInventorySlot[,] slots = inventory.GetSlots();
			Vector2Int size = inventory.Size;
			for (int i = 0; i < size.x; i++)
			{
				for (int j = 0; j < size.y; j++)
				{
					IReadOnlyInventorySlot slot = slots[i, j];

					Debug.Log($"Slot ({i}:{j})");
				}
			}
		}
	}
}
