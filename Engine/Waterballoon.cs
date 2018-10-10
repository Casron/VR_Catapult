using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterballoon : ClickableSign 
{
	public bool spent;
	Catapult catapult;
	public bool attached;
	int side;
	// Use this for initialization
	void Awake () 
	{
		spent = false;
		attached = true;
	}
	public void SetCatapult(Catapult c)
	{
		catapult = c;
	}
	
	public override void Click(PlayerController pC)
	{
		if (catapult != null && !spent && catapult.GetTurn())
		{
			spent = true;
			catapult.CommencePull();
		}
	}

	void FixedUpdate()
	{
		if (transform.position.y < -10.0f)
		{
			Kill();
		}
		if (!attached)
		{
			if (side == 1 && transform.position.x > catapult.gameObject.transform.position.x)
			{
				catapult.GetForce(transform.position, gameObject.GetComponent<Rigidbody>());
			}
			else if (side == -1 && transform.position.x < catapult.gameObject.transform.position.x)
			{
				catapult.GetForce(transform.position, gameObject.GetComponent<Rigidbody>());
			}
		}
		if (catapult.GetLocalBalloon() != this.gameObject)
		{
			Destroy(gameObject);
		}

	}
	public void Detach()
	{
		Rigidbody rB = gameObject.AddComponent<Rigidbody>();
		rB.mass = 0.75f;
		rB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
		attached = false;
		if (transform.position.x < catapult.gameObject.transform.position.x)
		{
			side = -1;
		}
		else if (transform.position.x > catapult.gameObject.transform.position.x)
		{
			side = 1;
		}
		else
		{
			side = 0;
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Block")
		{
			Debug.Log("HIt!");
		}
		Invoke("Kill", 1.0f);
		gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	void Kill()
	{
		CancelInvoke("Kill");
		Destroy(gameObject);
		catapult.NextTurn();		
	}
}
