using UnityEngine;
using System.Collections;

public class ListeMannInteraction : MonoBehaviour
{
    bool inTrigger = false;
    public bool ListeMannInteracted = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))      //If pressed ListeMann will talk!
            {
                ListeMannInteracted = true;
                Debug.Log("I am thy ListeMann!");
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
