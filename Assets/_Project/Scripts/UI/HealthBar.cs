using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] protected Image HealthBarImage;
	[SerializeField] private Health _health;
	[SerializeField] private TextMeshPro _hpText;

	protected void DrawHealth(float currentHp, float maxHp)
	{
		HealthBarImage.fillAmount = currentHp / maxHp;
		_hpText.text = currentHp.ToString("0");
	}

	private void OnEnable()
	{
		_health.HealthChanged += DrawHealth;
	}

	private void OnDisable()
	{
		_health.HealthChanged -= DrawHealth;
	}

	private void OnValidate()
	{
		_health ??= GetComponentInParent<Health>();
	}
}
