using UnityEngine;
using System.Collections;

public class Gemstone : MonoBehaviour {

	public float xOffset = -5.5f;
	public float yOffset = -3.5f;

	public int rowIndex = 0;
	public int columnIndex = 0;

	public GameObject[] gemstoneBgs;
	public int gemstoneType;

	public GameObject gemstoneBg;

	public GameController gameController;
	public SpriteRenderer spriteRenderer;

	public bool isSelected {
		set{
			if (value) {
				spriteRenderer.color = Color.gray;
			}else{
				spriteRenderer.color = Color.white;
			}
		}
	}

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
		spriteRenderer = gemstoneBg.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePostion (int _rowIndex, int _columnIndex){
		rowIndex = _rowIndex;
		columnIndex = _columnIndex;
		this.transform.position = new Vector3 (columnIndex * 1.2f + xOffset, rowIndex * 1.2f + yOffset);
	}

	public void TweenToPostion(int _rowIndex, int _columnIndex){
		rowIndex = _rowIndex;
		columnIndex = _columnIndex;
		iTween.MoveTo(this.gameObject,iTween.Hash("x",columnIndex * 1.2f + xOffset,"y", rowIndex * 1.2f + yOffset, "time", 0.3f));
	}

	public void RandomCreateGemstoneBg(){
		if (gemstoneBg != null) {
			return;
		}
		gemstoneType = Random.Range(0,gemstoneBgs.Length);
		gemstoneBg = Instantiate (gemstoneBgs [gemstoneType]) as GameObject;
		gemstoneBg.transform.parent = this.transform;
	}

	public void OnMouseDown(){
		gameController.Select (this);
	}

	public void Dispose (){
		Destroy (this.gameObject);
		Destroy (gemstoneBg.gameObject);
		gameController = null;
	}
}
