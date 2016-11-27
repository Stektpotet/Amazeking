using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
	[Header("Steps")]
	public AudioGroup[] stepSounds;

	[Header("Idle")]
	public AudioGroup idleSounds;
	public float idleWaitTimeMin = 60f, idleWaitTimeMax = 120f;

	[Header("Interaction")]
	public AudioGroup interactionSounds;

	private AudioSource source;
	private PlayerController player;

	private float idleTimer = 0, nextIdleTime;
	private bool idle = false;

	void Awake()
	{
		source = GetComponent<AudioSource>();
		player = GetComponent<PlayerController>();

		nextIdleTime = Random.Range(idleWaitTimeMin, idleWaitTimeMax);
	}

	public void Step()
	{
		Collider2D col = Physics2D.OverlapCircle(player.groundPoint.position, player.groundRadius, player.groundMask);
		if(col != null)
		{
			PhysicsMaterial2D physMat = col.sharedMaterial;
			if(physMat != null)
			{
				for(int mat = 0; mat < stepSounds.Length; mat++)
				{
					if(physMat.name == stepSounds[mat].name)
					{
						stepSounds[mat].PlayRandomSound(ref source);
						return;
					}
				}
			}
		}
		stepSounds[0].PlayRandomSound(ref source);
	}

	void StartIdleTimer()
	{
		idle = true;
	}

	void ResetIdleTimer()
	{
		idle = false;
		idleTimer = 0;
	}

	void Update()
	{
		if(idle)
		{
			idleTimer += Time.deltaTime;
			if(idleTimer > nextIdleTime)
			{
				idleTimer = 0;
				nextIdleTime = Random.Range(idleWaitTimeMin, idleWaitTimeMax);
				idleSounds.PlayRandomSound(ref source);
			}
		}
	}
}

