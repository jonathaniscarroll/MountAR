using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextJuice : MonoBehaviour
{
	public GameObject UnhighlightedText;
	public GameObject HighlightedText;
	private bool IsHighlighted;

	public void Toggle()
	{
		IsHighlighted = !IsHighlighted;
		if (IsHighlighted)
		{
			Highlight();
		}
		else
		{
			StopHighlight();
		}
	}

	public void StopHighlight()
	{
		UnhighlightedText.SetActive(true);
		HighlightedText.SetActive(false);
	}
	public void Highlight()
	{
		UnhighlightedText.SetActive(false);
		HighlightedText.SetActive(true);
	}
}
