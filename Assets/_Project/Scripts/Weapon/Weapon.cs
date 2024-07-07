using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _config;
    public  float Damage => _config.Damage;
	public float AmmoÑonsumption => _config.AmmoÑonsumption;
    public string AmmoId => _config.ItemConfig.ItemId;

	public void DealDamage(Entity target)
    {
        target.GetDamage(_config.Damage);
    }
}
