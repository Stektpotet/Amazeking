using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	[TextArea()]
	public string[] texts;

	private Image bubble;
	private Text bubbleText;
	
	int textIndex = 0;

	void Awake()
	{
		bubble = GetComponent<Image>();
		bubbleText = bubble.GetComponentInChildren<Text>();
	}

    public void SetVisibility(bool visible)
    {
		bubble.enabled = visible;
		bubbleText.enabled = visible;
    }

	public void NextText()
	{
		if(!gameObject.activeInHierarchy)
		{ SetVisibility(true); }
		if(textIndex < texts.Length)
		{
			bubbleText.text = texts[textIndex++];
		}
	}
}