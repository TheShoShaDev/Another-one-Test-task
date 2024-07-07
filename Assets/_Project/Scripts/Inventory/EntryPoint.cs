using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ScreenView _screenView;

    public InventoryService InventoryService { get; private set; }

	private List<ItemConfig> itemConfigs; 

	private void Start()
	{
		InventoryService = new InventoryService();

		InventoryGridData inventoryData = CreateInventory();
		InventoryService.RegisterInventory(inventoryData);

		new ScreenController(InventoryService, _screenView);

		itemConfigs = ItemManager.Instance.GetConfigList();
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			var rIndex = Random.Range(0, itemConfigs.Count);
			var rItemID = itemConfigs[rIndex].ItemId;
			var rAmount = 1;
			var result = InventoryService.AddItemsToInventory(rItemID, rAmount);

		}
	}

	private void LoadInventory()
	{

	}

	private InventoryGridData CreateInventory()
	{
		Vector2Int size = new Vector2Int(6, 5);
		List<InventorySlotData> createdInventorySlots = new List<InventorySlotData>();
		int lenght = size.x * size.y;
		for (int i = 0; i < lenght; i++)
		{
			createdInventorySlots.Add(new InventorySlotData());
		}

		InventoryGridData createdInventoryData = new InventoryGridData
		{
			Size = size,
			Slots = createdInventorySlots
		};

		return createdInventoryData;
	}
}
