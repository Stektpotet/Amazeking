﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour {
    bool inTrigger = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))      //If pressed -goto next room:
            {
                SceneManager.LoadSceneAsync("level2");
                Debug.Log("Try to open door");
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