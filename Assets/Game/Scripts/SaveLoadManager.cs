using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
	public static FileStream CreateFileStream(string fileName)
	{
		return new FileStream(Application.persistentDataPath + "/"+fileName + ".hornes", FileMode.Create);
	}
	public static void SavePlayerStats(PlayerStats stats)
	{
		
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
}