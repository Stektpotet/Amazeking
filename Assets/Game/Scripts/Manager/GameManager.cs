using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
			
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		levelManager.LoadLevel("Main Menu");
	}

	private static LevelManager m_levelManager = null;
	public static LevelManager levelManager
	{
		get
		{
			if(m_levelManager == null)
			{
				m_levelManager = FindObjectOfType<LevelManager>();
			}
			return m_levelManager;
		}
	}

	private static AudioSource m_audioSource = null;
	public static AudioSource audioSource
	{
		get
		{
			if(m_audioSource == null)
			{
				m_audioSource = instance.GetComponent<AudioSource>();
			}
			return m_audioSource;
		}
	}

	private static AudioManager m_audioManager = null;
	public static AudioManager audioManager
	{
		get
		{
			if(m_levelManager == null)
			{
				m_audioManager = FindObjectOfType<AudioManager>();
			}
			return m_audioManager;
		}
	}

}
