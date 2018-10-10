using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{	
	public BlockMode mode;
	protected Material material;
	public int blockType;
	public bool alive;
	public Material deadMat;
	protected GameObject player;
	void OnTriggerStay(Collider other)
	{
		if (alive && mode == BlockMode.Play)
		{
			if (other.gameObject.tag == "Floor"  || other.gameObject.tag == "Fence")
			{
				RIP();
			}
			else if (other.gameObject.tag == "Block" && !other.isTrigger)
			{
				Block pB = other.gameObject.GetComponent<Block>();
				if (!pB.alive)
				{
					RIP();
				}
			}
		}
	}
	public void Commence()
	{
		mode = BlockMode.Play;
		Rigidbody rB = gameObject.AddComponent<Rigidbody>();
		if (blockType < 2)
		{
			rB.mass = 3.0f;
		}
		else
		{
			rB.mass = 5.0f;
		}
		rB.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}
	protected void CreateColliders()
	{
		if (blockType < 2)
		{
			gameObject.AddComponent<BoxCollider>();
		}
		else if (blockType == 2)
		{
			BoxCollider bc1 = gameObject.AddComponent<BoxCollider>();
			BoxCollider bc2 = gameObject.AddComponent<BoxCollider>();
			bc1.size = new Vector3(1.0f,3.0f,1.0f);
			bc2.size = new Vector3(1.0f,1.0f,3.0f);
			
		}
		else if (blockType == 3)
		{
			BoxCollider bc1 = gameObject.AddComponent<BoxCollider>();
			bc1.size = new Vector3(1.0f,1.0f,1.0f);
			bc1.center = new Vector3(0.0f,0.0f,0.5f);
			BoxCollider bc2 = gameObject.AddComponent<BoxCollider>();	
			bc2.size = new Vector3(1.0f,1.0f,2.0f);
			bc2.center = new Vector3(0.0f,1.0f,0.0f);
			BoxCollider bc3 = gameObject.AddComponent<BoxCollider>();	
			bc3.size = new Vector3(1.0f,1.0f,2.0f);
			bc3.center = new Vector3(0.0f,-1.0f,0.0f);	
		}		
	}
	protected virtual void RIP()
	{
		alive = false;
		gameObject.GetComponent<MeshRenderer>().material = deadMat;		
	}
}
