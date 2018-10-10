using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
	static int p1Score;
	static int botScore;
	static bool gameOver = false;

	public static void Reset()
	{
		p1Score = 0;
		botScore = 0;
		gameOver = false;
	}
	public static void P1Add()
	{
		p1Score++;
	}
	public static void BotAdd()
	{
		botScore++;
	}
	public static void P1Down()
	{
		p1Score--;
		DisplayScore();
	}
	public static void BotDown()
	{
		botScore--;
		DisplayScore();
	}
	public static void DisplayScore()
	{
		GameObject g = GameObject.Find("DebugText");
		if (!gameOver)
		{
			TextMesh tM = g.GetComponent<TextMesh>();
			tM.text = "Player Blocks Left: " + p1Score + "| Bot Blocks Left: " + botScore;
			if (p1Score <= 0)
			{
				gameOver = true;
				tM.text = "Bot wins!";
			}
			else if (botScore <= 0)
			{
				gameOver = true;
				tM.text = "Player wins!";
			}
		}
	}
}
