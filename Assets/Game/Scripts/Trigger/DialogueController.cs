using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	[System.Serializable]
	public class Dialogue
	{
		public string name;
		[TextArea()]
		public string[] texts;
		public string this[int i] { get { return texts[i]; } }
		public int textCount { get { return texts.Length; } }
		public int currentTextLength { get { return texts[textIndex].Length; } }
		public string currentText{ get { return texts[textIndex]; } set { texts[textIndex] = value; } }
		public int textIndex = -1;

		public UnityEvent onStartDialogueStage = new UnityEvent();
		public UnityEvent onEndDialogueStage = new UnityEvent();

		public bool AutoEnd = true;

		public void StartDialogue()
		{
			onStartDialogueStage.Invoke();
		}

		public void EndDialogue()
		{
			onEndDialogueStage.Invoke();
		}

	}

	public Dialogue[] dialogueStages;
	int dialogueIndex = 0;
	public Dialogue dialogue { get { return dialogueStages[dialogueIndex]; } } //current dialogue
	
	public static float textTime = 0.05f, nextTextTime=1; //Add in settings?

	public GameObject textBox;
	public Text bubbleText;

	public PlayerStats stats;

	public void Awake()
	{
		foreach(Dialogue d in dialogueStages)
		{
			d.onStartDialogueStage.AddListener(() => textBox.SetActive(true));
			d.onStartDialogueStage.AddListener(NextText);
		}
	}

	public void StartDialogue()
	{
		dialogue.onStartDialogueStage.Invoke();
	}

	public void StartDialogueStage(int i)
	{
		dialogueIndex = i;
		StartDialogue();
	}

	public void EndDialogue()
	{
		dialogue.onEndDialogueStage.Invoke();
	}
	public void StartNextStage()
	{
		dialogueIndex++;
		dialogue.StartDialogue();
	}

	public void NextText()
	{
		StopAllCoroutines();
		if(dialogue.textIndex < dialogue.textCount-1)
		{
			bubbleText.text = dialogue[++dialogue.textIndex];
			StartCoroutine(ReadText());
		}
		else if(dialogue.AutoEnd)
		{
			dialogue.EndDialogue();
		}
	}
	IEnumerator ReadText()
	{
		string s = string.Empty;
		//Dictionary<int, string> markdownDictionary = new Dictionary<int, string>();
		//Dictionary<int, string> specialWordsDictionary = new Dictionary<int, string>();
		//Match m = Regex.Match(dialogue.currentText, @"(<\w+>).+(<\/\w+>)"); //may need \g
		Match specialWords = Regex.Match(dialogue.currentText, @"\[\w*\]");

		if(specialWords.Success)
		{
			string fieldName = specialWords.Value.Substring(1, specialWords.Length-2);
			object statsObj = stats.data.GetType().GetProperty(fieldName).GetValue(stats.data, null);
			if(statsObj != null)
			{
				dialogue.currentText = dialogue.currentText.Replace(specialWords.Value, statsObj.ToString());
			}
			
		}
		//while(m.Success)
		//{
		//	markdownDictionary.Add(m.Index, m.Value);
		//	m.NextMatch();
		//}
		for(int i = 0; i < dialogue.currentTextLength; i++)
		{
			//if(markdownDictionary.TryGetValue(i, out markdown))
			//{
			//	s += markdown;
			//	i += markdown.Length;
			//}
			//else
			//{
				s += dialogue.currentText[i];
			//}
			
			bubbleText.text = s;
			yield return new WaitForSeconds(textTime);
		}
		yield return new WaitForSeconds(nextTextTime);
		NextText();
	}
}