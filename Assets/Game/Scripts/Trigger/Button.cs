using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Button : Trigger
{
	public Sprite up, down;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void ButtonUp()
	{
		spriteRenderer.sprite = up;
	}
	public void ButtonDown()
	{
		spriteRenderer.sprite = down;
	}
}
