using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour {
    bool inTrigger = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        Scene activeScene = SceneManager.GetActiveScene();

        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))     
            {
                if (activeScene == SceneManager.GetSceneByName("level1"))
                {
                    if (GameObject.Find("ListeMannTrigger").GetComponent<ListeMannInteraction>().ListeMannInteracted)
                    {
                        SceneManager.LoadSceneAsync("level2");
                    }
                    else 
                    {
                        Debug.Log("You have not talked to ListeMann yet!");
                    }
                }
                else
                {
                    SceneManager.LoadSceneAsync("level3");
                }
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
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
