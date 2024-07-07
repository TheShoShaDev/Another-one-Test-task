using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotControllers = new();
        public InventoryGridController(IReadOnlyInventoryGrid inventory, InventoryView view) 
        {
            Vector2Int size = inventory.Size;
            IReadOnlyInventorySlot[,] slots = inventory.GetSlots();
            int lineLenght = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * lineLenght + j;
                    var slotView = view.GetInventorySlotView(index);
                    IReadOnlyInventorySlot slot = slots[i, j];
                    _slotControllers.Add(new InventorySlotController(slot, slotView));

				}
            }
        }
    }
}
