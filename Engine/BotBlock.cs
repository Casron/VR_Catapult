using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBlock : Block 
{
	void Awake()
	{
		material = gameObject.GetComponent<MeshRenderer>().material;
		alive = true;
		player = GameObject.Find("Bot");
		CreateColliders();
	}
	protected override void RIP()
	{
		alive = false;
		gameObject.GetComponent<MeshRenderer>().material = deadMat;	
		Score.BotDown();
	}

}
