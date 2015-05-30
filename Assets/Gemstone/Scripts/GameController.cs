using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Gemstone gemstone;
	public int rowNum = 7;
	public int columnNum = 10;
	public ArrayList gemstones;

	public AudioClip matchClip;
	public AudioClip errorClip;
	public AudioClip swapClip;

	private Gemstone curGemstone;

	private ArrayList matchGemstones;


	// Use this for initialization
	void Start () {
		gemstones = new ArrayList();
		for (int rowIndex = 0; rowIndex < rowNum; rowIndex ++) {
			ArrayList temp = new ArrayList();
			for ( int columnIndex = 0; columnIndex < columnNum; columnIndex ++ ) {
				Gemstone c = AddGemstone(rowIndex,columnIndex);
				temp.Add(c);

			}
			gemstones.Add(temp);
		}

		if (MatchHorizontal() || MatchVertical()) {
			RemoveMatches();
		}
	}

	public Gemstone AddGemstone(int rowIndex, int columnIndex){
		Gemstone c = Instantiate (gemstone) as Gemstone;
		c.transform.parent = this.transform;
		c.GetComponent<Gemstone>().RandomCreateGemstoneBg();
		c.GetComponent<Gemstone>().UpdatePostion(rowIndex, columnIndex);
		return c;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Select (Gemstone gemstone) {
		if (curGemstone == null) {
			curGemstone = gemstone;
			curGemstone.isSelected = true;
			return;
		} else {
			if (Mathf.Abs(curGemstone.rowIndex - gemstone.rowIndex)+ Mathf.Abs(curGemstone.columnIndex - gemstone.columnIndex) == 1){
				StartCoroutine(ExchangeAndMatch(curGemstone, gemstone));
			}else{
				audio.PlayOneShot (errorClip);
			}
			curGemstone.isSelected = false;
			curGemstone = null;
		}
	}

	IEnumerator ExchangeAndMatch(Gemstone c1, Gemstone c2){
		Exchange(c1, c2);
		yield return new WaitForSeconds (0.5f);
		if (MatchHorizontal () || MatchVertical ()) {
			RemoveMatches ();
		} else {
			Debug.Log("已经交换了");
			Exchange(c1, c2);
		}
	}

	bool MatchHorizontal (){
		bool isMatched = false;
		for(int rowIndex = 0; rowIndex < rowNum; rowIndex ++) {
			for (int columnIndex = 0; columnIndex < columnNum - 2; columnIndex ++){
				if ((GetGemstone(rowIndex,columnIndex).gemstoneType == GetGemstone(rowIndex,columnIndex+1).gemstoneType)
				    && (GetGemstone(rowIndex,columnIndex).gemstoneType == GetGemstone(rowIndex,columnIndex+2).gemstoneType)){
					Debug.Log("Match the Horizontal Gemstone is Same");
					AddMatches(GetGemstone(rowIndex, columnIndex));
					AddMatches(GetGemstone(rowIndex, columnIndex + 1));
					AddMatches(GetGemstone(rowIndex, columnIndex + 2));
					isMatched = true;
				}
			}
		}

		return isMatched;
	}

	bool MatchVertical (){
		bool isMatched = false;
		for(int columnIndex = 0; columnIndex < columnNum; columnIndex ++) {
			for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex ++){
				if ((GetGemstone(rowIndex,columnIndex).gemstoneType == GetGemstone(rowIndex+1,columnIndex).gemstoneType)
				    && (GetGemstone(rowIndex,columnIndex).gemstoneType == GetGemstone(rowIndex+2,columnIndex).gemstoneType)){
					Debug.Log("Match the Vertical Gemstone is Same");
					AddMatches(GetGemstone(rowIndex, columnIndex));
					AddMatches(GetGemstone(rowIndex + 1, columnIndex));
					AddMatches(GetGemstone(rowIndex + 2, columnIndex));
					isMatched = true;
				}
			}
		}
		return isMatched;
	}

	void AddMatches(Gemstone c){
		if (matchGemstones == null) {
			matchGemstones = new ArrayList();
		}
		int index = matchGemstones.IndexOf (c);
		if (index == -1) {
			matchGemstones.Add(c);
		}
	}

	void RemoveMatches (){
		for (int i = 0; i < matchGemstones.Count; i++) {
			Gemstone c = matchGemstones[i] as Gemstone;
			RemoveGemstone(c);
		}
		matchGemstones = new ArrayList ();
		StartCoroutine (WaitForCheckMatchAgain());
	}

	IEnumerator WaitForCheckMatchAgain(){
		yield return new WaitForSeconds (0.5f);
		if (MatchHorizontal() || MatchVertical()) {
			RemoveMatches();
		}
	}

	void RemoveGemstone(Gemstone c){
		c.Dispose ();
		audio.PlayOneShot (matchClip);
		// 消除宝石之后上面的宝石下来
		for (int i = c.rowIndex + 1; i < rowNum; i++) {
			Gemstone tempGemstone = GetGemstone(i,c.columnIndex);
			tempGemstone.rowIndex --;
			SetGemstone(tempGemstone, tempGemstone.rowIndex, tempGemstone.columnIndex);
			// tempGemstone.UpdatePostion(tempGemstone.rowIndex, tempGemstone.columnIndex);
			tempGemstone.TweenToPostion(tempGemstone.rowIndex, tempGemstone.columnIndex);
		}

		Gemstone newGemstone = AddGemstone (rowNum, c.columnIndex);
		newGemstone.rowIndex --;
		SetGemstone (newGemstone, newGemstone.rowIndex, newGemstone.columnIndex);
		// newGemstone.UpdatePostion (newGemstone.rowIndex, newGemstone.columnIndex);
		newGemstone.TweenToPostion (newGemstone.rowIndex, newGemstone.columnIndex);


	}

	public Gemstone GetGemstone(int rowIndex, int columnIndex){
		ArrayList temp = gemstones [rowIndex] as ArrayList;
		Gemstone c = temp [columnIndex] as Gemstone;
		return c;
	}
	
	public void SetGemstone(Gemstone gemstone, int rowIndex, int columnIndex){
		ArrayList temp = gemstones [rowIndex] as ArrayList;
		temp [columnIndex] = gemstone;
		
	}

	void Exchange (Gemstone c1, Gemstone c2)
	{
		audio.PlayOneShot (swapClip);
		SetGemstone (c2, c1.rowIndex, c1.columnIndex);
		SetGemstone (c1, c2.rowIndex, c2.columnIndex);

		int tempRowIndex;
		tempRowIndex = c1.rowIndex;
		c1.rowIndex = c2.rowIndex;
		c2.rowIndex = tempRowIndex;

		int tempColumnIndex;
		tempColumnIndex = c1.columnIndex;
		c1.columnIndex = c2.columnIndex;
		c2.columnIndex = tempColumnIndex;

		// c1.UpdatePostion (c1.rowIndex, c1.columnIndex);
		// c2.UpdatePostion (c2.rowIndex, c2.columnIndex);
		c1.TweenToPostion (c1.rowIndex, c1.columnIndex);
		c2.TweenToPostion (c2.rowIndex, c2.columnIndex);


	}
}
