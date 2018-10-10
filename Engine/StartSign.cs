using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSign : ClickableSign
{
	public GameObject placeBlock;
	public Bot bot;
	public Undo undo;
	public override void Click(PlayerController pC)
	{
		placeBlock.SetActive(true);
		bot.Build();
		undo.Reset();
		pC.SetMode(1);
	}

}
