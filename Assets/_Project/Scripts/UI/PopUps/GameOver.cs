using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	[SerializeField] private GameObject _gameOverPanel;

	private void OnEnable()
	{
		EventBus.GameOver += ShowGameOver;
	}

	private void OnDisable()
	{
		EventBus.GameOver -= ShowGameOver;
	}

	private void ShowGameOver()
	{
		_gameOverPanel.SetActive(true);
	}

	public void Restart(string sceneName)
	{
		_gameOverPanel.SetActive(false);
		SceneManager.LoadScene(sceneName);
	}
}
