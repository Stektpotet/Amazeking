using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public AudioMixer mixer;

	public void SetMasterVolume(float v)
	{
		mixer.SetFloat("masterVol", v);
	}
	public void SetMusicVolume(float v)
	{
		mixer.SetFloat("musicVol", v);
	}
	public void SetSFXVolume(float v)
	{
		mixer.SetFloat("sfxVol", v);
	}

	public void PlayMusic(AudioClip clip)
	{
		GameManager.audioSource.clip = clip;
		GameManager.audioSource.Play();
	}
}
