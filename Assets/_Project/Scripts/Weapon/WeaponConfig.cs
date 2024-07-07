using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
	public float Damage;
	public float Ammo—onsumption;
	public ItemConfig ItemConfig;
}
