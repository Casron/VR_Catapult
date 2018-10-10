 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSign : ClickableSign
{
	public GameObject[] catapults;
	public GameObject[] blockCreationSigns;
	public GameObject towerGrid;
	Resetable given;

	public override void Click(PlayerController pC)
	{
		for(int i = 0; i < catapults.Length; i++)
		{
			catapults[i].SetActive(false);
		}
		for(int i = 0; i < blockCreationSigns.Length; i++)
		{
			blockCreationSigns[i].SetActive(true);
		}
		towerGrid.SetActive(true);
		Score.Reset();
		GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");
		for(int i = 0; i < balloons.Length; i++)
		{
			Destroy(balloons[i]);
		}
		Resetable[] o = (Resetable[])Object.FindObjectsOfTypeAll(typeof(Resetable));
		for(int i = 0; i < o.Length; i++)
		{
			o[i].Reset();
		}
		GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
		for(int i = 0; i < blocks.Length; i++)
		{
			if (blocks[i].gameObject.GetComponent<Block>().mode != BlockMode.Showcase)
			{
				Destroy(blocks[i]);
			}
		}
		
	}
 
}
