using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "New PlayerStats", menuName ="Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
	public SaveLoadManager.PlayerStatsData data;

	public void Reset()
	{
		data = new SaveLoadManager.PlayerStatsData();
	}

	public void LoadData()
	{
		this.data = SaveLoadManager.LoadPlayerStats();
	}
	public void SaveData()
	{
		SaveLoadManager.SavePlayerStats(this);
	}
}