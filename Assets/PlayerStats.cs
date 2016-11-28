using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "New PlayerStats", menuName ="Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
	public string playerName = "Hornes";
	public int jumpCount;
	public int killCount;

	public void SaveData()
	{

	}
}
[System.Serializable]
public class PlayerStatsData
{
	public string playerName = "Hornes";
	public int[] ints;

	public void SaveData()
	{

	}
}