using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "New PlayerStats", menuName ="Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
	public string playerName = "Hornes";
	public int jumpCount;
	public int killCount;

	public void LoadData(SaveLoadManager.PlayerStatsData data)
	{
		playerName = data.playerName;
		jumpCount = data.jumpCount;
		jumpCount = data.killCount;
	}
	public void SaveData()
	{
		SaveLoadManager.SavePlayerStats(this);
	}
}