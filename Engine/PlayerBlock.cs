using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gvr.Internal;

public enum BlockMode{Showcase,Placement,Placed,Play};
public class PlayerBlock : Block
{

	GameObject pointer;
	bool validPlacement;
	int z;
	int y;

	void Awake()
	{
		material = gameObject.GetComponent<MeshRenderer>().material;
		mode = BlockMode.Showcase;
		alive = true;
		validPlacement = false;
	}
	public void StartPlace(GameObject p, GameObject pl)
	{
		mode = BlockMode.Placement;
		pointer = p;
		player = pl;
		PlayerController pC = player.GetComponent<PlayerController>();
		pC.RemoveBlock();
		if (pC.GetBlocks() < 0)
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (mode == BlockMode.Placement)
		{
			float yShift = 0.5f;
			float yMarker  = 0.25f;
			if (blockType == 3)
			{
				yShift = 0.0f;
				yMarker = 0.5f;
			}
			Vector3 tPos = pointer.transform.position;
			if (tPos.y > 9 || tPos.z < -13.5 || tPos.z > 13.5 || tPos.y <= 0 || Vector3.Distance(tPos, player.transform.position) < 6.5f)
			{
				transform.position = tPos;
			}
			else
			{
				Vector3 currentTrans = transform.position;
				float y = Mathf.Round(tPos.y) + yShift;
				{
					if (tPos.y - y >= yMarker)
					{
						y += 1.0f;
					}
					else if ((tPos.y - y <= -yMarker))
					{
						y -= 1.0f;
					}
				}
				transform.position = new Vector3(tPos.x, y,Mathf.Round(tPos.z));
			}
		}
		if (GvrControllerInput.ClickButtonUp)
		{
			if (mode == BlockMode.Placement)
			{
				try
				{
					BlockPlacement();
				}
				catch(System.Exception)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	void BlockPlacement()
	{
		GameObject pointedObj = player.GetComponent<PlayerController>().pointedObj;
		Vector3 pos = transform.position;
		z = (int)pos.z + 12;
		y = (int)(pos.y - 0.5f);
		if (blockType == 3)
		{
			y += 1;
		}
		TowerGrid t = null;
		if (pointedObj != null)
		{
			if (pointedObj.name == "placementBlock")
			{
				t = pointedObj.GetComponent<TowerGrid>();
				if (blockType == 0)
				{
					if (pos.y >= 2.0f)
					{
						bool errorFound = false;
						for(int i = z - 1; i <= z + 1; i++)
						{
							try
							{
								if (t.IsOccupied(i,y))
								{
									errorFound = true;
									break;
								}
							}
							catch (System.Exception)
							{
								errorFound = true;
								break;
							}
						}
						if (!errorFound)
						{
							validPlacement = true;
						}
					}
				}
				else if (blockType == 1)
				{
					if (pos.y >= 1.0f)
					{
						bool errorFound = false;
						for(int i = y - 1; i <= y + 1; i++)
						{
							try
							{
								if (t.IsOccupied(z,i))
								{
									errorFound = true;
									break;
								}
							}
							catch (System.Exception)
							{
								errorFound = true;
								break;
							}
						}
						if (!errorFound)
						{
							validPlacement = true;
						}
					}
				}
				else if (blockType == 2)
				{
					if (pos.y > 1.0f)
					{
						bool errorFound = false;
						for(int i = y - 1; i <= y + 1; i++)
						{
							try
							{
								if (t.IsOccupied(z,i))
								{
									errorFound = true;
									break;
								}
							}
							catch (System.Exception)
							{
								errorFound = true;
								break;
							}
						}	
						for(int i = z - 1; i <= z + 1; i++)
						{
							try
							{
								if (t.IsOccupied(i,y))
								{
									errorFound = true;
									break;
								}
							}
							catch (System.Exception)
							{
								errorFound = true;
								break;
							}
						}	
						if (!errorFound)
						{
							validPlacement = true;
						}								
					}
				}
				else if (blockType == 3)
				{
					if (pos.y > 0.5f)
					{
						bool errorFound = false;
						for(int i = z - 1; i <= z + 1; i++)
						{
							try
							{
								if (t.IsOccupied(i,y))
								{
									errorFound = true;
									break;
								}
							}
							catch (System.Exception)
							{
								errorFound = true;
								break;
							}
						}
						try
						{
							if (t.IsOccupied(z-1, y-1))
							{
								errorFound = true;
							}	
							if (t.IsOccupied(z+1,y-1))
							{
								errorFound = true;
							}
						}
						catch(System.Exception)
						{
							errorFound = true;
						}
						if (!errorFound)
						{
							validPlacement = true;
						}								
					}
				}
			}
		}
		if (validPlacement)
		{
			GameObject uS = GameObject.Find("UndoSign");
			uS.GetComponent<Undo>().Push(this.gameObject);
			CreateColliders();
			mode = BlockMode.Placed;
			if (t != null)
			{
				if (blockType == 0)
				{
					t.OccupySquare(z-1,y);
					t.OccupySquare(z,y);
					t.OccupySquare(z+1,y);
				}
				else if (blockType == 1)
				{
					t.OccupySquare(z,y-1);
					t.OccupySquare(z,y);
					t.OccupySquare(z,y+1);	
				}
				else if (blockType == 2)
				{
					t.OccupySquare(z-1,y);
					t.OccupySquare(z+1,y);
					t.OccupySquare(z,y-1);
					t.OccupySquare(z,y);
					t.OccupySquare(z,y+1);	
				}
				else if (blockType == 3)
				{
					t.OccupySquare(z-1,y-1);
					t.OccupySquare(z+1,y-1);
					t.OccupySquare(z-1,y);
					t.OccupySquare(z,y);
					t.OccupySquare(z+1,y);						
				}
			}

		}
		else if (!validPlacement)
		{
			Destroy(gameObject);
		}		
	}
	void OnDestroy()
	{
		if (player != null)
		{
			player.GetComponent<PlayerController>().AddBlock();
		}
		if (mode == BlockMode.Placed)
		{
			GameObject pb = GameObject.Find("placementBlock");
			if (pb != null)
			{
				TowerGrid t = pb.GetComponent<TowerGrid>();
				if (blockType == 0)
				{
					t.ClearSquare(z-1,y);
					t.ClearSquare(z,y);
					t.ClearSquare(z+1,y);
				}
				else if (blockType == 1)
				{
					t.ClearSquare(z,y-1);
					t.ClearSquare(z,y);
					t.ClearSquare(z,y+1);	
				}
				else if (blockType == 2)
				{
					t.ClearSquare(z-1,y);
					t.ClearSquare(z+1,y);
					t.ClearSquare(z,y-1);
					t.ClearSquare(z,y);
					t.ClearSquare(z,y+1);	
				}
				else if (blockType == 3)
				{
					t.ClearSquare(z-1,y-1);
					t.ClearSquare(z+1,y-1);
					t.ClearSquare(z-1,y);
					t.ClearSquare(z,y);
					t.ClearSquare(z+1,y);						
				}
			}
		}
	}
	protected override void RIP()
	{
		alive = false;
		gameObject.GetComponent<MeshRenderer>().material = deadMat;	
		Score.P1Down();
	}
}
