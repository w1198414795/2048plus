using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
	int[,] pos = new int[4, 2]  {
		{0,1},
		{0,-1},
		{1,0},
		{-1,0}
	};
	int[,]test = {
		{0,0,1,1,0,1,2,3,2},
		{0,1,1,0,2,2,2,3,1},
		{1,0,1,0,0,1,2,1,1},
		{1,0,1,2,0,1,3,2,0},
		{0,0,0,1,3,3,2,2,1}
	};  //9*5
	bool[,] isFinded = new bool[5, 9] ;
	int number = 0;
	// Use this for initialization
	void Start ()
	{
		for (int i=0; i<5; i++)
			for (int j=0; j<9; j++)
				isFinded [i, j] = false;
		SearchArray (1, 6);

		print (number);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void SearchArray (int x, int y)
	{
		isFinded [x, y] = true;
		int xx = 0;
		int yy = 0;

		int now = test [x, y];
		for (int n=0; n<4; n++) {
			xx = x + pos [n, 0];
			yy = y + pos [n, 1];

			if (xx >= 0 && xx < 5 && yy >= 0 && yy < 9 && isFinded [xx, yy] == false) {

				if (now == test [xx, yy]) {
					number++;
					SearchArray (xx, yy);
				}

			}

		}
		return;
	}
}
