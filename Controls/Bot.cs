using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour 
{
	GameObject[][] set;
	public GameObject[] easyBuilds;
	public GameObject[] mediumBuilds;
	public GameObject[] hardBuilds;
	public void Build()
	{
		set = new GameObject[3][];
		set[0] = easyBuilds;
		set[1] = mediumBuilds;
		set[2] = hardBuilds;

		int dif = BotBehaviour.difficulty;
		if (dif == 3)
		{
			dif = 2;
		}
		Instantiate(set[dif][0], new Vector3(-51.0f,0.0f,0.0f),Quaternion.identity);
	}

}
