using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySign : ClickableSign {

	public int difficulty;

	public override void Click(PlayerController pC)
	{
		BotBehaviour.SetDifficulty(difficulty);
		GameObject[] g = GameObject.FindGameObjectsWithTag("Sign");
		for(int i = 0 ; i < g.Length; i++)
		{
			if (g[i].GetComponent<DifficultySign>() != null)
			{
				g[i].transform.GetChild(0).gameObject.GetComponent<TextMesh>().color = new Color(0.0f,0.0f,0.0f);
			}
		}
		transform.GetChild(0).gameObject.GetComponent<TextMesh>().color = new Color(1.0f,0.0f,0.0f);
	}
}
