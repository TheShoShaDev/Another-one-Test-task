using System.Collections;
using System.Collections.Generic;

namespace Inventory
{
    public class ScreenController 
    {
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;

        private InventoryGridController _currentInventoryController;

		public ScreenController(InventoryService inventoryService, ScreenView view) 
        {
            _inventoryService = inventoryService;  
            _view = view;
            _currentInventoryController = new InventoryGridController(_inventoryService.GetInventory(), _view.InventoryView);
        }

    }
}
