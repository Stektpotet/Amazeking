using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class doorInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))      //If pressed -goto next room:
        {
            SceneManager.LoadSceneAsync("level2");
            //SceneManager.LoadScene("level2");
        }
	}
}
