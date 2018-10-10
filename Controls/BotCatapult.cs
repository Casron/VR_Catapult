using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCatapult : Catapult 
{

    //Since we dont need to worry about user input for the bot's turn, it will start pulling the water balloon automatically
	public override void CommenceTurn()
	{
		rB.velocity = Vector3.zero;
		balloonSpawn.transform.localPosition = new Vector3(0.0f,0.0f,3.0f);
		localBalloon = (GameObject)Instantiate(balloonPrefab, balloonSpawn.transform.position, Quaternion.identity);
		localBalloon.GetComponent<Waterballoon>().SetCatapult(this);
		turn = true;
		Invoke("CommencePull",0.5f);
	}

    //Allows the bot to aim the catapult
	protected override void Aim()
	{
		GameObject[] blockSet = GameObject.FindGameObjectsWithTag("Block");
		GameObject target = null;
		float value = 0;
		float[] values = ValueSet();
		for(int i = 0; i < blockSet.Length; i++)
		{
			Block b = blockSet[i].GetComponent<Block>();
			if (b is PlayerBlock)
			{
				if (b.alive && b.mode == BlockMode.Play)
				{
					int bT = b.blockType;
					float locVal = values[bT] + GetHeightValue(b.gameObject.transform.position.y);
					if (locVal > value)
					{
						target = b.gameObject;
						value = locVal;
					}
				}
			}
		}

        //The bot will be more accurate depending on its difficulty
		if (target != null)
		{
			float yy = Mathf.Abs(transform.position.z - target.transform.position.z);
			float xx = Mathf.Abs(transform.position.x - target.transform.position.x);
			float rot = Mathf.Atan(yy/xx) * Mathf.Rad2Deg;
			float shift = Random.Range(-0.2f, 0.2f);
			float degShift = Random.Range(-3.0f, 3.0f);
			float heightShift = target.transform.position.y * -0.1f;
			if (BotBehaviour.difficulty == 1)
			{
				shift *= 0.75f;
				degShift *= 0.75f;
			}
			else if (BotBehaviour.difficulty == 2)
			{
				shift *= 0.5f;
				degShift *= 0.5f;
			}
			else if (BotBehaviour.difficulty == 3)
			{
				shift *= 0.05f;
				degShift *= 0.05f;
			}
			if (target.transform.position.z > 0)
			{
				rot *= -1.0f;
			}
			transform.eulerAngles = new Vector3(-90.0f, 0.0f, rot + degShift);

			balloonSpawn.transform.localPosition = new Vector3(-4.0f + shift + heightShift,0.0f,2.5f);
			localBalloon.transform.position = balloonSpawn.transform.position;
			Release();
		}
	}

    //The bot will prefer to target certain pieces based on its difficulty
	float[] ValueSet()
	{
		int dif = BotBehaviour.difficulty;
		if (dif == 0)
		{
			return new float[]{1.0f,1.0f,1.0f,1.0f};
		}
		else if (dif == 1)
		{
			return new float[]{0.5f,1.0f,1.0f,1.0f};
		}
		else if (dif == 2)
		{
			return new float[]{0.5f,0.75f, 1.0f,1.0f};
		}
		else
		{
			return new float[]{0.5f, 0.5f,1.0f,0.75f};
		}
	}
	float GetHeightValue(float y)
	{
		int dif = BotBehaviour.difficulty;
		if (dif == 0)
		{
			return 0;
		}
		else if (dif == 1)
		{
			return 0;
		}
		else if (dif == 2)
		{
			return 0.15f * (20.0f-y);
		}
		else
		{
			return 1.0f * (20.0f - y);
		}
	}
}
