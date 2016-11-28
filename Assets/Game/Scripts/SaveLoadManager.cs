using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
	private static FileStream CreateFileStream(string fileName, FileMode fileMode)
	{
		return new FileStream(Application.persistentDataPath + "/"+fileName + ".hornes", fileMode);
	}
	public static void SavePlayerStats(PlayerStats stats)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fs = CreateFileStream("stats", FileMode.Create);
		bf.Serialize(fs, new PlayerStatsData());
		fs.Close();
    }
	public static PlayerStatsData LoadPlayerStats()
	{
		PlayerStatsData data = new PlayerStatsData() ;
        if(File.Exists(Application.persistentDataPath + "/stats.hornes"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = CreateFileStream("stats", FileMode.Open);
			data = bf.Deserialize(fs) as PlayerStatsData;

			fs.Close();
		}
		return data;
	}

	[System.Serializable]
	public class PlayerStatsData
	{
		public string playerName = "Hornes";
		public int[] stats;
		public int jumpCount { get { return stats[0]; } }
		public int killCount { get { return stats[1]; } }
	}
}