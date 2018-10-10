using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignText : MonoBehaviour 
{

	MeshRenderer rend;
	void Awake()
	{
		rend = gameObject.GetComponent<MeshRenderer>();
	}
	void Update()
	{
		if (Camera.main.transform.position.z > transform.position.z)
		{
			rend.enabled = false;
		}
		else
		{
			rend.enabled = true;
		}
	}
}
