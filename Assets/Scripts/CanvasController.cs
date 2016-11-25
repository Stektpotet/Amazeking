using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CanvasController : MonoBehaviour
{
	public List<Image> UIElements = new List<Image>();

    public void ToggleVisibility(int i)
    {
        UIElements[i].gameObject.SetActive(!UIElements[i].gameObject.activeInHierarchy);
    }

	void OnValidate()
	{
		if(UIElements.Count > 0)
		{
			foreach(Image i in UIElements)
			{
				i.gameObject.SetActive(false);
            }
		}
    }

}