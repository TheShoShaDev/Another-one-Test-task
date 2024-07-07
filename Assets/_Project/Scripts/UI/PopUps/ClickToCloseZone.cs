using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToCloseZone : MonoBehaviour, IPointerClickHandler
{
	public event Action Clicked;

	public void OnPointerClick(PointerEventData eventData)
	{
		Clicked?.Invoke();
	}
}
