using Inventory;
using UnityEngine;
using UnityEngine.UI;
using ItemsEnums;
using TMPro;
using System.Runtime.CompilerServices;

public class PopUpsManager : MonoBehaviour
{
	[SerializeField] private ClickToCloseZone _clickToCloseZone;

	[SerializeField] private Button _buyBtn;
	[SerializeField] private Button _deleteBtn;
	[SerializeField] private Button _equipBtn;
	[SerializeField] private Button _healBtn;

	[SerializeField] private GameObject _popUpWnd;

	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private Image _image;

	[SerializeField] private GameObject _armorPlace;
	[SerializeField] private TextMeshProUGUI _armor;

	[SerializeField] private GameObject _weightPlace;
	[SerializeField] private TextMeshProUGUI _weight;

	private IReadOnlyInventorySlot _slot;
	private ItemConfig _item;

	private void OnEnable()
	{
		EventBus.ViewClicked += ShowPopUp;
		_clickToCloseZone.Clicked += HidePopUp;
	}

	private void OnDisable()
	{
		EventBus.ViewClicked -= ShowPopUp;
		_clickToCloseZone.Clicked -= HidePopUp;
	}

	private void ShowPopUp(IReadOnlyInventorySlot slot)
	{
		_slot = slot;
		_item = ItemManager.Instance.GetItemConfig(_slot.ItemID);

		_image.sprite = _item.Icon;
		_name.text = _item.Name;
		_weight.text = (_item.Weight * _slot.Amount).ToString();

		if (_item.ItemType == ItemType.Ammo)
		{
			_buyBtn.gameObject.SetActive(false);
			_equipBtn.gameObject.SetActive(false);
			_deleteBtn.gameObject.SetActive(true);
			_healBtn.gameObject.SetActive(false);
			_armorPlace.SetActive(false);
		}	

		if (_item.ItemType == ItemType.Armor)
		{
			_buyBtn.gameObject.SetActive(false);
			_equipBtn.gameObject.SetActive(true);
			_deleteBtn.gameObject.SetActive(true);
			_healBtn.gameObject.SetActive(false);
			_armorPlace.SetActive(true);

			var armor = _item as ClothesConfig;

			_armor.text = (armor.Armor).ToString();
		}

		if (_item.ItemType == ItemType.Ñonsumables)
		{
			_buyBtn.gameObject.SetActive(false);
			_equipBtn.gameObject.SetActive(false);
			_deleteBtn.gameObject.SetActive(true);
			_healBtn.gameObject.SetActive(true);
			_armorPlace.SetActive(false);
		}

		_popUpWnd.SetActive(true);
	}

	private void HidePopUp()
	{
		_popUpWnd.SetActive(false);
	}

	public void Delete()
	{
		EntryPoint entryPoint = FindObjectOfType<EntryPoint>();
		InventoryService inventoryService = entryPoint.InventoryService;
		inventoryService.GetInventory();

		inventoryService.RemoveItems(_item.ItemId, _slot.Amount);
	}

	public void Add()
	{
		EntryPoint entryPoint = FindObjectOfType<EntryPoint>();
		InventoryService inventoryService = entryPoint.InventoryService;
		inventoryService.GetInventory();

		inventoryService.AddItemsToInventory(_item.ItemId, _item.MaxCapacity);
	}
}
