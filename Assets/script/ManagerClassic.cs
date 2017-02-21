using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum Dir
{
    none,
    right,
    left,
    up,
    down

}

public class ManagerClassic : MonoBehaviour
{
    public Image cube;
    public Sprite[] picture;
    public static Sprite[] pic;
    public RectTransform canvas;
    int width = 4;
    List<Cube>[] cubeBox;
    Vector2[] mousePos = new Vector2[2];
    Image cache;
    public static Cube[,] cubeList;
    Dir touchDir = Dir.none;
    public static bool[,] isBlank;
    int cacheNum;
    public static int score;
    public Text scoreLabel;
    bool canTouch = true;
    // Use this for initialization  new Vector3 (106 + 196 * i, 406 + 200 * j, 0)
    void Awake()
    {
        int n = picture.Length;
        pic = new Sprite[n];
        for (int m = 0; m < n; m += 1)
            pic[m] = picture[m];
    }
    IEnumerator Start()
    {

        score = 0;
        scoreLabel.text = score.ToString();
        LeanTween.init();
        yield return new WaitForSeconds(0.5f);
        cubeList = new Cube[width, width];
        cubeBox = new List<Cube>[width];
        isBlank = new bool[width, width];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < width; j++)
                isBlank[i, j] = true;
        for (int j = 0; j < width; j++)
            cubeBox[j] = new List<Cube>();
        CreateNum();
        CreateNum();


    }

    // Update is called once per frame
    void Update()
    {
        touchDir = TouchReact();
        if (canTouch)
        {
            switch (touchDir)
            {
                case Dir.right:
                    {
                        MoveRight();
                        StartCoroutine(CreateCube());
                        break;
                    }
                case Dir.left:
                    {
                        MoveLeft();
                        StartCoroutine(CreateCube());
                        break;
                    }
                case Dir.down:
                    {
                        MoveDown();
                        StartCoroutine(CreateCube());
                        break;
                    }
                case Dir.up:
                    {
                        MoveUp();
                        StartCoroutine(CreateCube());
                        break;
                    }
                case Dir.none:
                    break;
            }
        }

    }

    Dir TouchReact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos[0] = Input.mousePosition;
            return Dir.none;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePos[1] = Input.mousePosition;
            if (Mathf.Abs(mousePos[1].x - mousePos[0].x) > Mathf.Abs(mousePos[1].y - mousePos[0].y))
            {
                if (mousePos[1].x - mousePos[0].x > 0)
                    return Dir.right;
                else
                    return Dir.left;
            }
            else
            {
                if (mousePos[1].y - mousePos[0].y > 0)
                    return Dir.up;
                else if (mousePos[1].y - mousePos[0].y < 0)
                    return Dir.down;

            }
        }
        return Dir.none;
    }

    void CreateNum()
    {
        while (0 == 0)
        {
            if (IsFilled() == true)
            {
                SecondCheck();
                return;
            }

            int x = Random.Range(0, width);
            int y = Random.Range(0, width);

            if (isBlank[x, y] == true)
            {
                cache = (Image)Instantiate(cube, new Vector3(106 + 196 * x, 406 + 200 * y, 0), Quaternion.identity);
                cache.transform.SetParent(canvas, false);
                cubeList[x, y] = cache.GetComponent<Cube>();
                cubeList[x, y].x = x;
                cubeList[x, y].y = y;
                isBlank[x, y] = false;

                return;
            }
        }
    }

    void MoveRight()
    {

        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (isBlank[i, j] == false)
                {
                    if (i == width - 1)
                    {
                        cubeBox[j].Add(cubeList[i, j]);
                        break;
                    }
                    else
                        cacheNum = cubeList[i, j].value;
                    for (int k = i + 1; k < width; k += 1)
                    {
                        if (isBlank[k, j] == true)
                            if (k == width - 1)
                                cubeBox[j].Add(cubeList[i, j]);
                            else
                                continue;
                        else if (cubeList[k, j].value == cacheNum)
                        {
                            cubeList[k, j].value += cacheNum;
                            cubeList[k, j].needChanged = true;
                            cubeList[k, j].otherCube = cubeList[i, j];
                            cubeBox[j].Add(cubeList[k, j]);
                            i = k;

                            break;
                        }
                        else
                        {
                            cubeBox[j].Add(cubeList[i, j]);

                            break;
                        }

                    }

                }
                else
                    continue;
            }
        }

        for (int j = 0; j < width; j += 1)
        {
            int count = cubeBox[j].Count;
            int lastPos = width - 1;
            if (count > 0)
            {

                for (int n = count - 1; n >= 0; n -= 1)
                {
                    cubeBox[j][n].Move(lastPos, j);
                    lastPos -= 1;
                }
                cubeBox[j].Clear();
            }

        }
    }

    void MoveLeft()
    {
        for (int j = 0; j < width; j++)
        {
            for (int i = width - 1; i >= 0; i--)
            {
                if (isBlank[i, j] == false)
                {
                    if (i == 0)
                    {
                        cubeBox[j].Add(cubeList[i, j]);
                        break;
                    }
                    else
                        cacheNum = cubeList[i, j].value;
                    for (int k = i - 1; k >= 0; k -= 1)
                    {
                        if (isBlank[k, j] == true)
                            if (k == 0)
                                cubeBox[j].Add(cubeList[i, j]);
                            else
                                continue;
                        else if (cubeList[k, j].value == cacheNum)
                        {
                            cubeList[k, j].value += cacheNum;
                            cubeList[k, j].needChanged = true;
                            cubeList[k, j].otherCube = cubeList[i, j];
                            cubeBox[j].Add(cubeList[k, j]);
                            i = k;

                            break;
                        }
                        else
                        {
                            cubeBox[j].Add(cubeList[i, j]);

                            break;
                        }

                    }

                }
                else
                    continue;
            }
        }

        for (int j = 0; j < width; j += 1)
        {
            int count = cubeBox[j].Count;
            int lastPos = 0;
            if (count > 0)
            {

                for (int n = count - 1; n >= 0; n -= 1)
                {
                    cubeBox[j][n].Move(lastPos, j);
                    lastPos += 1;
                }
                cubeBox[j].Clear();
            }

        }
    }

    void MoveUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j += 1)
            {
                if (isBlank[i, j] == false)
                {
                    if (j == width - 1)
                    {
                        cubeBox[i].Add(cubeList[i, j]);
                        break;
                    }
                    else
                        cacheNum = cubeList[i, j].value;
                    for (int k = j + 1; k < width; k += 1)
                    {
                        if (isBlank[i, k] == true)
                            if (k == width - 1)
                                cubeBox[i].Add(cubeList[i, j]);
                            else
                                continue;
                        else if (cubeList[i, k].value == cacheNum)
                        {
                            cubeList[i, k].value += cacheNum;
                            cubeList[i, k].needChanged = true;
                            cubeList[i, k].otherCube = cubeList[i, j];
                            cubeBox[i].Add(cubeList[i, k]);
                            j = k;

                            break;
                        }
                        else
                        {
                            cubeBox[i].Add(cubeList[i, j]);

                            break;
                        }

                    }

                }
                else
                    continue;
            }
        }

        for (int j = 0; j < width; j += 1)
        {
            int count = cubeBox[j].Count;
            int lastPos = width - 1;
            if (count > 0)
            {

                for (int n = count - 1; n >= 0; n -= 1)
                {
                    cubeBox[j][n].Move(j, lastPos);
                    lastPos -= 1;
                }
                cubeBox[j].Clear();
            }

        }

    }

    void MoveDown()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = width - 1; j >= 0; j--)
            {
                if (isBlank[i, j] == false)
                {
                    if (j == 0)
                    {
                        cubeBox[i].Add(cubeList[i, j]);
                        break;
                    }
                    else
                        cacheNum = cubeList[i, j].value;
                    for (int k = j - 1; k >= 0; k -= 1)
                    {
                        if (isBlank[i, k] == true)
                            if (k == 0)
                                cubeBox[i].Add(cubeList[i, j]);
                            else
                                continue;
                        else if (cubeList[i, k].value == cacheNum)
                        {
                            cubeList[i, k].value += cacheNum;
                            cubeList[i, k].needChanged = true;
                            cubeList[i, k].otherCube = cubeList[i, j];
                            cubeBox[i].Add(cubeList[i, k]);
                            j = k;

                            break;
                        }
                        else
                        {
                            cubeBox[i].Add(cubeList[i, j]);

                            break;
                        }

                    }

                }
                else
                    continue;
            }
        }

        for (int j = 0; j < width; j += 1)
        {
            int count = cubeBox[j].Count;
            int lastPos = 0;
            if (count > 0)
            {

                for (int n = count - 1; n >= 0; n -= 1)
                {
                    cubeBox[j][n].Move(j, lastPos);
                    lastPos += 1;
                }
                cubeBox[j].Clear();
            }

        }
    }

    IEnumerator CreateCube()
    {
        canTouch = false;
        yield return new WaitForSeconds(0.3f);
        scoreLabel.text = score.ToString();
        CreateNum();
        canTouch = true;
    }
    bool IsFilled()
    {
        for (int i = 0; i < width; i += 1)
            for (int j = 0; j < width; j += 1)
                if (isBlank[i, j] == true)
                    return false;

        return true;
    }
    void SecondCheck()
    {
        for (int i = 0; i < width; i += 1)
            for (int j = 0; j < width - 1; j += 1)
                if (cubeList[i, j].value == cubeList[i, j + 1].value)
                    return;
        for (int j = 0; j < width; j += 1)
            for (int i = 0; i < width - 1; i += 1)
                if (cubeList[i, j].value == cubeList[i + 1, j].value)
                    return;
        print("gameover");
    }
}
