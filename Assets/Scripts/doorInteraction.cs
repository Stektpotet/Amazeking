using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour {

	bool inTrigger = false;

	public ListeMannInteraction interaction;
	
	// Update is called once per frame
	void Update () {
        Scene activeScene = SceneManager.GetActiveScene();

        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))     
            {
				if(interaction.ListeMannInteracted)
				{
					SceneManager.LoadSceneAsync("level2");
				}
				else
				{
					Debug.Log("You have not talked to ListeMann yet!");
				}
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
