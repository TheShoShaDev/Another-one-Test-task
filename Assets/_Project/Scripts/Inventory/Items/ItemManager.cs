using UnityEngine;
using System.Collections.Generic;
using ItemsEnums;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{
	private Dictionary<string, ItemConfig> itemConfigs = new Dictionary<string, ItemConfig>();

	public static ItemManager Instance;

	private void Awake()
	{
		LoadItemConfigs();
		Instance = this;
	}

	private void LoadItemConfigs()
	{
		LoadAllConfigsInFolder("Assets\\_Project\\Config\\Items");
	}

	private void LoadAllConfigsInFolder(string path)
	{
		string[] filePaths = Directory.GetFiles(path, "*.asset", SearchOption.AllDirectories);

		foreach (string filePath in filePaths)
		{
			ItemConfig config = AssetDatabase.LoadAssetAtPath<ItemConfig>(filePath);

			if (config != null)
			{
				itemConfigs.Add(config.ItemId, config);
			}
		}
	}

	public ItemConfig GetItemConfig(string itemId)
	{
		if (itemConfigs.ContainsKey(itemId))
		{
			ItemConfig itemConfig = itemConfigs[itemId];

			if(itemConfig.ItemType == ItemType.Armor)
			{
				return itemConfig as ClothesConfig;
			}
			return itemConfig;
		}
		else
		{
			Debug.LogError($"ItemConfig with ItemId '{itemId}' not found!");
			return null;
		}
	}

	public List<ItemConfig> GetConfigList()
	{
		List<ItemConfig> itemConfigList = new List<ItemConfig>();
		foreach (ItemConfig itemConfig in itemConfigs.Values) 
		{
			itemConfigList.Add(itemConfig);
		}

		return itemConfigList;
	}

	public string GetRandomItemId()
	{
		string randomID = string.Empty;
		int gettingChance = 40;

		foreach (ItemConfig itemConfig in itemConfigs.Values)
		{
			if (Random.Range(0,100) <= gettingChance)
			{
				randomID = itemConfig.ItemId;
			}
		}

		if (randomID == string.Empty) 
		{
			randomID = GetRandomItemId();
		}

		return randomID;
	}
}
