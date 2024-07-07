using Inventory;
using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{ 
	public static event Action EnemyDied;
	public static event Action<IReadOnlyInventorySlot> ViewClicked;
	public static void OnEnemyDied()
	{
		EnemyDied?.Invoke();
	}

	public static void OnViewClicked(IReadOnlyInventorySlot slot)
	{
		ViewClicked?.Invoke(slot);
	}
}
