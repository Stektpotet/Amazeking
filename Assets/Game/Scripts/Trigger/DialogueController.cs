using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	[System.Serializable]
	public class DialogueStage
	{
		public string name;
		[TextArea()]
		public string[] texts;
		public string this[int i] { get { return texts[i]; } }
		public int textCount { get { return texts.Length; } }
		public int currentTextLength { get { return texts[textIndex].Length; } }
		public string currentText{ get { return texts[textIndex]; } }
		public int textIndex = -1;

	}

	public DialogueStage[] dialogueStages;
	int dialogueStage = 0;

	public UnityEvent onStartDialogue;
	public UnityEvent onEndDialogue;
	public float textTime = 0.05f, nextTextTime=1;

	public bool AutoEnd = true;

	public GameObject bubble;
	public Text bubbleText;


	void Awake()
	{
		onStartDialogue.AddListener(new UnityAction(ToggleVisibility));
		onStartDialogue.AddListener(new UnityAction(NextText));
		onEndDialogue.AddListener(new UnityAction(ToggleVisibility));
	}

	public void StartDialogue()
	{
		onStartDialogue.Invoke();
	}

	public void EndDialogue()
	{
		onEndDialogue.Invoke();
	}

	public void ToggleVisibility()
    {
		bubble.SetActive(!bubble.activeInHierarchy);
    }
	public void NextStage()
	{
		dialogueStage++;
	}
	public void SetStage(int i)
	{
		dialogueStage = i;
	}
	public void NextText()
	{
		StopAllCoroutines();
		if(dialogueStages[dialogueStage].textIndex < dialogueStages[dialogueStage].textCount-1)
		{
			bubbleText.text = dialogueStages[dialogueStage][++dialogueStages[dialogueStage].textIndex];
			StartCoroutine(ReadText());
		}
		else if(AutoEnd)
		{
			EndDialogue();
		}
	}
	IEnumerator ReadText()
	{
		string s = string.Empty;
		for(int i = 0; i < dialogueStages[dialogueStage].currentTextLength; i++)
		{
			s += dialogueStages[dialogueStage].currentText[i];
			bubbleText.text = s;
			yield return new WaitForSeconds(textTime);
		}
		yield return new WaitForSeconds(nextTextTime);
		NextText();
	}
}