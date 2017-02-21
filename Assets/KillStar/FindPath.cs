using UnityEngine;
using System.Collections;

public class FindPath : MonoBehaviour
{
	int[,]path = new int[,] {
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,1,0,0,0,0}
	}; //5*9
	bool[,]isFinded = new bool[5, 9] ;
	int min = 99999;
	int[,]pos = new int[4, 2]{
		{1,0},
		{-1,0},
		{0,1},
		{0,-1}
	} ;

	// Use this for initialization
	void Start ()
	{
		for (int i=0; i<5; i++)
			for (int j=0; j<9; j++)
				isFinded [i, j] = false;
        print(path[2, 3] + "is 23");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            FindShortPath(0, 0, 0, 6, 0);
            if (min == 99999)
                print("CantGo");
            else
                print(min + " min");
        }
	}
	void FindShortPath (int x, int y, int targetx, int targety, int step)
	{
		isFinded [x, y] = true;
		int xx = 0;
		int yy = 0;
		if (x == targetx && y == targety) {
			if (step < min)
				min = step;
			return;
		}
		for (int i=0; i<4; i++) {
			xx = x + pos [i, 0];
			yy = y + pos [i, 1];
			if (xx >= 0 && xx < 5 && yy >= 0 && yy < 9 && isFinded [xx, yy] == false && path [xx, yy] != 1) {

				FindShortPath (xx, yy, targetx, targety, step + 1);
				isFinded [xx, yy] = false;
			}
		}
		return;
	}

}
