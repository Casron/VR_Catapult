using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : ClickableSign
{
	public override void Click(PlayerController pC)
	{
		Application.Quit();
	}

}
