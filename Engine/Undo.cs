using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : ClickableSign
{

	CatapultStack<GameObject> blockOrder;
	void Awake()
	{
		blockOrder = new CatapultStack<GameObject>();
	}
	public void Reset()
	{
		blockOrder = new CatapultStack<GameObject>();
	}
	public override void Click(PlayerController pC)
	{
		Destroy(blockOrder.Pop());
	}
	public void Push(GameObject g)
	{
		blockOrder.Push(g);
	}
}
