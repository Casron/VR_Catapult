using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A waterballoon catapult which uses spring physics to launch projectiles. Designed to work with VR controls
//Coded in C# for the VR Circus project
public class Catapult: Resetable
{
	protected bool turn;
	bool released = true;
	public GameObject balloonSpawn;
	public GameObject balloonPrefab;
	public Transform leftHook;
	public Transform rightHook;
	protected GameObject localBalloon;
	public LineRenderer leftLine;
	public LineRenderer rightLine;
	public Rigidbody rB;
	protected const float spring = 18.0f;
	public Catapult otherCatapult;

    //At the start of a players turn the location of their water balloon resets
	public virtual void CommenceTurn()
	{
		rB.velocity = Vector3.zero;
		balloonSpawn.transform.localPosition = new Vector3(0.0f,0.0f,3.0f);
		localBalloon = (GameObject)Instantiate(balloonPrefab, balloonSpawn.transform.position, Quaternion.identity);
		localBalloon.GetComponent<Waterballoon>().SetCatapult(this);
		turn = true;
	}
    //In case there's still a water balloon floating around, ensures that the balloon for this catapult is the one currently in use
	public GameObject GetLocalBalloon()
	{
		return localBalloon;
	}
	public bool GetTurn()
	{
		return turn;
	}

    //Allows the player to begin aiming the catapult
	public void CommencePull()
	{
		released = false;
		rB.constraints = RigidbodyConstraints.FreezeAll;
	}

    //Resets tbe aim
	public override void Reset()
	{
		released = true;
		turn = false;
		rB.velocity = Vector3.zero;
		balloonSpawn.transform.localPosition = new Vector3(0.0f,0.0f,3.0f);
	}

    //Inherited by the AI catapult class
	protected virtual void Aim()
	{

	}

    //Releases control from the player and allows the balloon to be affected by physics
	public void Release()
	{
		if (!released && rB != null)
		{
			rB.constraints = RigidbodyConstraints.None;
			released = true;
			if (localBalloon != null)
			{
				localBalloon.GetComponent<Waterballoon>().Detach();
			}
		}
	}

	void FixedUpdate()
	{
		Vector3 pos = balloonSpawn.transform.position;

        //Gets the distance from the left and right stakes holding up the water balloon catapult
		float leftDist = Vector3.Distance(leftHook.position, pos);
		float rightDist = Vector3.Distance(rightHook.position, pos);

        //Utilizes the spring force of the two strings attached to the waterballon to launch the balloon and accuractely portray the movements of the catapult
        if (released)
		{
			Vector3 leftForce = leftHook.position - pos;
			leftForce *= leftDist * spring;
			Vector3 rightForce = rightHook.position - pos;
			rightForce *= rightDist * spring;
			rB.AddForce(leftForce, ForceMode.Force);
			rB.AddForce(rightForce, ForceMode.Force);
		}
		else if (!released && localBalloon != null)
		{
			Aim();
		}
		if (localBalloon != null && localBalloon.GetComponent<Waterballoon>().attached)
		{
			localBalloon.transform.position = balloonSpawn.transform.position; 
		}


		Vector3[] leftPos = new Vector3[]{leftHook.position, pos};
		Vector3[] rightPos = new Vector3[]{rightHook.position, pos};


		leftLine.SetPositions(leftPos);
		rightLine.SetPositions(rightPos);
		float leftWidth = 0.2f * (3.5f/leftDist);
		float rightWidth = 0.2f * (3.5f/rightDist);
		leftLine.SetWidth(leftWidth, leftWidth);
		rightLine.SetWidth(rightWidth, rightWidth);
	}

    //When the balloon is released it is subject to the catapult force until is passes the center of the catapult
	public void GetForce(Vector3 pos, Rigidbody r)
	{
		float leftDist = Vector3.Distance(leftHook.position, pos);
		float rightDist = Vector3.Distance(rightHook.position, pos);
		Vector3 leftForce = leftHook.position - pos;
		leftForce *= leftDist * spring;
		Vector3 rightForce = rightHook.position - pos;
		rightForce *= rightDist * spring;
		r.AddForce(leftForce, ForceMode.Force);
		r.AddForce(rightForce, ForceMode.Force);		
	}

    //Switches to the other catapult's turn
	public void NextTurn()
	{
		if (otherCatapult == null)
		{
			CommenceTurn();
		}
		else
		{
			otherCatapult.CommenceTurn();
		}
	}
}
