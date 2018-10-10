using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStack<T>
{

	Node head;

	public CatapultStack()
	{
		head = null;
	}
	private class Node
	{
		public T data;
		public Node next;
		public Node(T t)
		{
			data = t;
			next = null;
		}
		public T Pop()
		{
			return data;
		}
	}

	public void Push(T t)
	{
		Node n = new Node(t);
		Node prev = head;
		head = n;
		n.next = prev;
	}
	public T Pop()
	{
		if (head != null)
		{
			T data = head.data;
			head = head.next;
			return data;
		}
		else
		{
			return default(T);
		}
	}
}
