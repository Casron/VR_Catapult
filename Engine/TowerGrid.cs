using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : Resetable
{
	public bool[][] grid;
	int height;
	int width;

	void Awake()
	{
		width = (int)transform.localScale.z;
		height = (int)transform.localScale.y;
		grid = new bool[width][];
		for(int i = 0; i < width; i++)
		{
			grid[i] = new bool[height];
		}
	}
	public override void Reset()
	{
		grid = new bool[width][];
		for(int i = 0; i < width; i++)
		{
			grid[i] = new bool[height];
		}
	}
	public void OccupySquare(int x, int y)
	{
		grid[x][y] = true;
	}
	public void ClearSquare(int x, int y)
	{
		grid[x][y] = false;
	}
	public bool IsOccupied(int x, int y)
	{
		return grid[x][y];
	}

}
