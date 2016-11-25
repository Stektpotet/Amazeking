using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class TextBufferer : MonoBehaviour
{
	[TextArea()]
	public string text;
	public int maxLineLength = 12;
	public float waitTime = 0.5f;

	public List<string> words;

	Text textComp;

	void OnValidate()
	{
		words = new List<string>(Regex.Split(text, @"\s+"));
		if(maxLineLength > 5)
		{
			for(int i = 0; i < words.Count; i++)
			{
				string subText = string.Empty;
				while(subText.Length + words[i].Length < maxLineLength)
				{
					if(words[i].Length > maxLineLength)
					{
						words[i] = words[i].Substring(0, maxLineLength - 1) + "-";
						words.Insert(i + 1, words[i].Substring(maxLineLength,words[i].Length-maxLineLength));
					}
					subText += words[i] + " ";
				}
			}
		}
	}

	public void StartBuffer()
	{
		textComp = GetComponent<Text>();
		StartCoroutine(Buffer());
	}

	IEnumerator Buffer()
	{
		

		for(int i = 0; i < words.Count; i++)
		{
			string subText = string.Empty;
			while(subText.Length < maxLineLength)
			{
				if(words[i].Length > maxLineLength)
				{
					words[i] = words[i].Substring(0, maxLineLength-1) + "-";
					words.Insert(i + 1, words[i + 1]);
				}
				subText += words[i] + " ";
			}
			textComp.text = subText;
			yield return new WaitForSeconds(waitTime);
		}
		StopCoroutine(Buffer());
	}
	
}

/*
	string subText;
	int wordIndex = 0;
	string[] words = text.Split(' ');
	for(int i=0; i < text.Length; i+=subText.Length)
	{
		
		while(subText.Length < length)
		{
			subtext += word[wordIndex++] + " ";
		}
	}

*/
