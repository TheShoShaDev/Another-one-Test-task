using System.IO;
using UnityEngine;

public class PlayerHealth : Health
{
	protected override void Start()
	{
		base.Start();
		Load();
	}

	protected override void OnDecreseHealth(float value)
	{
		base.OnDecreseHealth(value);

		if (IsDead)
		{
			EventBus.OnGameOver();
		}
	}

	protected virtual void Save()
	{
		_gameData.PlayerHP = CurrentHealth;
		string jsonData = JsonUtility.ToJson(_gameData);
		File.WriteAllText($"{Application.persistentDataPath}/{_saveFileName}", jsonData);
	}

	protected virtual void Load() 
	{
		string filePath = $"{Application.persistentDataPath}/{_saveFileName}";
		if(File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);
			_gameData = JsonUtility.FromJson<GameData>(jsonData);

			CurrentHealth = _gameData.PlayerHP;
			if(CurrentHealth <= 0)
			{
				CurrentHealth = 100;
			}
			HealthChangedInvoke();
		}	
	}

	protected virtual void OnApplicationQuit()
	{
		Save();
	}
}
    