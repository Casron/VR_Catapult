using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSignParent : MonoBehaviour 
{
	public GameObject blockPrefab;
	GameObject blockShowcase;
	public GameObject spawnPos;

	void Awake()
	{
		GameObject g = (GameObject)Instantiate(blockPrefab, spawnPos.transform.position, blockPrefab.transform.rotation);
		float sX = g.transform.localScale.x;
		float sY = g.transform.localScale.y;
		float sZ = g.transform.localScale.z;
		g.transform.localScale = new Vector3(sX * 0.15f, sY * 0.15f,sZ * 0.15f);
		Vector3 eRot = g.transform.eulerAngles;
		float yRot = transform.eulerAngles.y;
		g.transform.eulerAngles = new Vector3(eRot.x, yRot, eRot.z);
		g.transform.SetParent(transform);
		Destroy(g.GetComponent<Collider>());
	}
}
