using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSign : ClickableSign
{
	public override void Click(PlayerController pC)
	{
		BlockSignParent bSP = transform.parent.GetComponent<BlockSignParent>();
		GameObject blockPrefab = bSP.blockPrefab;
		GameObject g = (GameObject)Instantiate(blockPrefab, transform.position, blockPrefab.transform.rotation);
		g.GetComponent<PlayerBlock>().StartPlace(pC.pointer, pC.gameObject);
	}


}
