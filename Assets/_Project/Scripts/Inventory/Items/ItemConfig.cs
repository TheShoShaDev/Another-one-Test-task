using ItemsEnums;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig")]
public class ItemConfig : ScriptableObject
{
	public string ItemId;
	public float Weight;
	public int MaxCapacity;
	public Sprite Icon;
	public ItemType ItemType;
	public string Name;
}
