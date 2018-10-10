using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSign : ClickableSign
{
	public GameObject[] catapults;
	public GameObject[] blockCreationSigns;

	public override void Click(PlayerController pC)
	{
		if (pC.GetBlocks() < pC.maxBlocks)
		{
			GameObject pB = GameObject.Find("placementBlock");
			pB.SetActive(false);
			pC.SetMode(2);
			for(int i = 0; i < catapults.Length; i++)
			{
				catapults[i].SetActive(true);
				if (i == 0)
				{
					catapults[0].GetComponent<Catapult>().CommenceTurn();
				}
			}
			for(int i = 0; i < blockCreationSigns.Length; i++)
			{
				blockCreationSigns[i].SetActive(false);
			}
			GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
			if (blocks != null)
			{
				for(int i = 0; i < blocks.Length; i++)
				{
					Block b = blocks[i].GetComponent<Block>();
					b.Commence();	
					if (b is PlayerBlock)
					{
						Score.P1Add();
					}
					else if (b is BotBlock)
					{
						Score.BotAdd();
					}
				}
				Score.DisplayScore();
			}
			this.gameObject.SetActive(false);
		}
	}

}
