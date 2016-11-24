using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour
{
    public Canvas messageCanvas;
    public void ToggleVisibility()
    {
        messageCanvas.enabled = !messageCanvas.enabled;
    }
}