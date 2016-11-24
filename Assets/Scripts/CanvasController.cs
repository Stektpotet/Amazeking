using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour
{
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}