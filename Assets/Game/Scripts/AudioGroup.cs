using UnityEngine;

[System.Serializable]
public class AudioGroup
{
	[System.Serializable]
	public struct PitchRange { public float min, max; }

	public string name;
	public PitchRange pitchRange;
	public AudioClip[] audioClips;


	public AudioClip this[int i] { get { return audioClips[i]; } }
	public int Size { get { return audioClips.Length; } }

	public void PlaySound(ref AudioSource source, int i)
	{
		source.clip = this[i];
		source.pitch = Random.Range(pitchRange.min, pitchRange.max);
		source.Play();
	}

	public void PlayRandomSound(ref AudioSource source)
	{
		PlaySound(ref source, Random.Range(0, Size - 1));
	}
}