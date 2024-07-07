using System;
using UnityEngine;

namespace Inventory
{
	[Serializable]
	public class InventorySlotData : MonoBehaviour
	{
		public int Amount;
		public string ItemID;
		public Sprite Image;
	}
}
