using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
	public class InventorySlotView : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Image _image;
		[SerializeField] private TMP_Text _textAmount;

		public event Action<InventorySlotView> Clicked;

		public int Amount
		{
			get => Convert.ToInt32(Amount);
			set
			{
				_textAmount.text = value < 2 ? "" : value.ToString();
			}
		}

		public Image Image 
		{	get => _image; 
			set
			{
				_image = value;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Clicked?.Invoke(this);
		}
	}
}
