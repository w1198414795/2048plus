using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Cube : MonoBehaviour
{
	public int value;
	public bool needChanged = false;
	Text number;

	public int x;
	public int y;
	public Cube otherCube;

	// Use this for initialization
	void Start ()
	{
		this.value = 2;
		number = transform.GetChild (0).GetComponent<Text> ();
		number.text = value.ToString ();
		otherCube = null;
		LeanTween.scale (this.gameObject, new Vector3 (1, 1, 1), 0.3f);


	}
	

	public  void Move (int i, int j)
	{
		if (this.x != i || this.y != j) {
			LeanTween.move (this.gameObject.GetComponent<RectTransform> (), new Vector3 (106 + 196 * i, 406 + 200 * j, 0), 0.3f);
			ManagerClassic.cubeList [this.x, this.y] = null;
			ManagerClassic.isBlank [this.x, this.y] = true;
			this.x = i;
			this.y = j;
			ManagerClassic.cubeList [this.x, this.y] = this;
			ManagerClassic.isBlank [this.x, this.y] = false;
		}

		if (needChanged) {
			LeanTween.move (otherCube.GetComponent<RectTransform> (), new Vector3 (106 + 196 * i, 406 + 200 * j, 0), 0.3f);
			Destroy (otherCube.gameObject, 0.3f);
			int xx = otherCube.x;
			int yy = otherCube.y;
			ManagerClassic.isBlank [xx, yy] = true;
			LeanTween.rotateAround (gameObject, Vector3.up, 360f, 0.3f);
			StartCoroutine (ChangeImage ());
		}

	
		this.needChanged = false;


	}
	IEnumerator ChangeImage ()
	{
		yield return new WaitForSeconds (0.3f);

		this.GetComponent<Image> ().sprite = ManagerClassic.pic [(int)Mathf.Log (value, 2) - 1];
		number.text = value.ToString ();
		ManagerClassic.score += value;
	}
}
