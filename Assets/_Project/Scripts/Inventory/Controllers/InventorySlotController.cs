using UnityEngine;

namespace Inventory
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;
        private IReadOnlyInventorySlot _slot;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view)
        { 
            slot.ItemIDChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;

            if (view.Image != null)
            {
                view.Image.sprite = slot.Image;
			}
            view.Amount = slot.Amount;

            _slot = slot;
			_view = view;
            _view.Clicked += OnViewClicked;

		}

        private void OnSlotItemIdChanged(string newId)
        {
            _view.Image.sprite = ItemManager.Instance.GetItemConfig(newId).Icon;
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.Amount = newAmount;
        }

        private void OnViewClicked(InventorySlotView view)
        {
            if (_view == view)
            {
                EventBus.OnViewClicked(_slot);   
            }
        }

	}
}
