using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gvr.Internal;

public class PlayerController : Resetable {

	Vector3 startPos;
	public TextMesh debugText;
	public GameObject pointerPen;
	public GameObject pointer;
	public LineRenderer line;
	public Material brightRed;
	public Material brightGreen;
	public GameObject redHalo;
	public GameObject greenHalo;
	public GameObject pointedObj;
	public int maxBlocks;
	public int blocks;
	int mode = 0;


	void Start()
	{
		blocks = maxBlocks;
		startPos = transform.position;
	}
	public override void Reset()
	{
		blocks = maxBlocks;
		transform.position = startPos;
		mode = 0;
	}
	void FixedUpdate()
	{
		UpdateHoveredSign();
	}
	public int GetBlocks()
	{
		return blocks;
	}
	public void RemoveBlock()
	{
		blocks--;
	}
	public void AddBlock()
	{
		if (blocks < maxBlocks)
		{
			blocks++;
		}
	}
	void MoveForward()
	{
		Vector3 tp = transform.position;
		float xPlus = -5.0f * Time.deltaTime;
		float yPlus = 0.0f;
		if (tp.y < 3.0f)
		{
			yPlus = 5.0f * Time.deltaTime;
		}
		if (tp.x > 45.0f)
		{
			transform.position = new Vector3(tp.x + xPlus, tp.y + yPlus, tp.z);
		}
		else
		{
			SetMode(3);
		}
	}
	void Update()
	{
		CheckClicks();
		PCMoveHead();
		if (mode == 2)
		{
			MoveForward();
		}
	}
	void UpdateHoveredSign()
	{
		Ray ray = new Ray(transform.position, pointerPen.transform.forward);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 200.0F);

		float dist = 220.0f;
		Vector3 pointerPos = ray.GetPoint(200.0f);
		pointedObj = null;
		for(int i = 0; i < hits.Length; i++)
		{
			Vector3 point =  hits[i].point;
			float locDist = Vector3.Distance(transform.position, point);
			if (locDist < dist)
			{
				pointedObj = hits[i].collider.gameObject;
				pointerPos = point;
				dist = locDist;
			}
		}
		pointer.transform.position = pointerPos;
		Vector3 basePos = new Vector3(pointerPen.transform.position.x, pointerPen.transform.position.y, pointerPen.transform.position.z);
		line.SetPosition(0, basePos);
		line.SetPosition(1, pointerPos);		
	}
	void CheckClicks()
	{
		if (GvrControllerInput.ClickButtonDown || Input.GetMouseButtonDown(0))
		{
			pointer.GetComponent<Light>().color = new Color(0.0f,255.0f,0.0f);
			line.material = brightGreen;
			greenHalo.SetActive(true);
			redHalo.SetActive(false);
			if (pointedObj != null)
			{
				ClickableSign cS = pointedObj.GetComponent<ClickableSign>();
				if (cS != null)
				{
					cS.Click(this);
				}
			}
		}
		else if (GvrControllerInput.ClickButtonUp || Input.GetMouseButtonUp(0))
		{
			pointer.GetComponent<Light>().color = new Color(255.0f,0.0f,0.0f);
			line.material = brightRed;
			greenHalo.SetActive(false);
			redHalo.SetActive(true);
		}		
	}
	public void SetMode(int m)
	{
		mode = m;
		if (m == 1)
		{
			transform.position = new Vector3(61.0f, 1.3f, 0.0f);
			transform.eulerAngles = new Vector3(0.0f,-90.0f,0.0f);
		}
	}
	void PCMoveHead()
	{
		float rot = 0;
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rot += 30.0f;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			rot -= 30.0f;
		}
		rot *= Time.deltaTime;
		Vector3 eA = transform.eulerAngles;
		transform.eulerAngles = new Vector3(eA.x,eA.y + rot, eA.z);
	}
	void MouseOver()
	{

	}
}
