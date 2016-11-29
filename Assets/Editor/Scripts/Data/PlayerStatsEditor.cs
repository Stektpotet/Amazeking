using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
	PlayerStats stats;
	void OnEnable()
	{
		stats = target as PlayerStats;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Reset Stats"))
		{
			stats.Reset();
		}
		if(GUILayout.Button("Save stats"))
		{
			stats.SaveData();
		}
		if(GUILayout.Button("Load stats"))
		{
			stats.LoadData();
		}
	}
}
