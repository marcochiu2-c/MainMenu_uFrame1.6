using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class PageUpDown : MonoBehaviour {

	public Button fSecondPage,fThirdPage, bSecondPage, bFirstPage;

	public Transform Page1, Page2, Page3, SecondPageLeft, SecondPageRight, ThirdPageLeft, FirstPageRight;
	
	public void toSecondPage(){
//		PageContent.DOLocalMoveX (0, 0.5f, PageContent.DOLocalMoveX (-680, 0.5f));
		Page1.DOLocalMoveX (-860 * 2, 0.1f);
		Page2.DOLocalMoveX (-860, 0.1f);
		Page3.DOLocalMoveX (0, 0.1f);
		FirstPageRight.DOLocalMoveX (-860, 0.1f);
		SecondPageLeft.DOLocalMoveX (-420, 0.1f);
		SecondPageRight.DOLocalMoveX (420, 0.1f);
		ThirdPageLeft.DOLocalMoveX (450*3, 0.1f);
	}

	public void toThirdPage(){
//		PageContent.DOLocalMoveX (0, 0.5f, PageContent.DOLocalMoveX (680, 0.5f));
		Page1.DOLocalMoveX (-860 * 3, 0.1f);
		Page2.DOLocalMoveX (-860*2, 0.1f);
		Page3.DOLocalMoveX (-860, 0.1f);
		FirstPageRight.DOLocalMoveX (-840, 0.1f);
		SecondPageLeft.DOLocalMoveX (-420*2, 0.1f);
		SecondPageRight.DOLocalMoveX (420*3, 0.1f);
		ThirdPageLeft.DOLocalMoveX (-420, 0.1f);
	}

	public void backToSecondPage(){
		Page1.DOLocalMoveX (-860 * 2, 0.1f);
		Page2.DOLocalMoveX (-860, 0.1f);
		Page3.DOLocalMoveX (0, 0.1f);
		FirstPageRight.DOLocalMoveX (-860, 0.1f);
		ThirdPageLeft.DOLocalMoveX (450*3, 0.1f);
		SecondPageRight.DOLocalMoveX (420, 0.1f);
		SecondPageLeft.DOLocalMoveX (-420, 0.1f);
	}

	public void backToFirstPage(){
		Page1.DOLocalMoveX (-860, 0.1f);
		Page2.DOLocalMoveX (0, 0.1f);
		Page3.DOLocalMoveX (860, 0.1f);
		FirstPageRight.DOLocalMoveX (420, 0.1f);
		SecondPageLeft.DOLocalMoveX (450*3, 0.1f);
		SecondPageRight.DOLocalMoveX (1280, 0.1f);
		ThirdPageLeft.DOLocalMoveX (1310 , 0.1f);
	}

	// Use this for initialization
/*	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
*/}
