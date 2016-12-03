using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool timed = false;

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

	private LevelManager m_levelManager = null;
	public LevelManager levelManager
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

	private MenuOverlayManager m_menuOverlayManager = null;
	public MenuOverlayManager menuOverlayManager
	{
		get
		{
			if(m_menuOverlayManager == null)
			{
				m_menuOverlayManager = FindObjectOfType<MenuOverlayManager>();
			}
			return m_menuOverlayManager;
		}
	}

	//private TimerManager m_timer = null;
	//public TimerManager timer
	//{
	//	get
	//	{
	//		if(m_timer == null)
	//		{
	//			m_timer = GetComponent<TimerManager>();
	//		}
	//		return m_timer;
	//	}
	//}

	private AudioSource m_audioSource = null;
	public AudioSource audioSource
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

	private AudioManager m_audioManager = null;
	public AudioManager audioManager
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
