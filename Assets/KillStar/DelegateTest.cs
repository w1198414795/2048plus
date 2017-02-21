using UnityEngine;
using System.Collections;

public class DelegateTest : MonoBehaviour
{
	delegate void PrintNum (int n);
	PrintNum printDelegate;

	// Use this for initialization
	void Start ()
	{
		printDelegate += Print1;
		printDelegate += Print2;
		printDelegate += Print3;

		//printDelegate = Print1; 单一赋值会被覆盖
		printDelegate (100);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void Print1 (int n)
	{
		print (n);
	}
	void Print2 (int  a)
	{
		print ("a" + "  " + a);
	}
	void Print3 (int b)
	{
		print ("b" + "  " + b);
	}
	void Print4 (string a)
	{
		print ("hahah");
	}
}
