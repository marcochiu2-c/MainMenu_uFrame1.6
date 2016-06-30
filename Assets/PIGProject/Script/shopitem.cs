using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shopitem : MonoBehaviour {
	
	public Text firstTime, monthCard, oneStarDust, twoStarDust, threeStarDust, fourStarDust, fiveStarDust, sixStarDust,
	oneResource, twoResource, threeResource, fourResource, fiveResource, sixResource,
	oneSilverFeather, twoSilverFeather, threeSilverFeather, fourSilverFeather, fiveSilverFeather, sixSilverFeather,
	firstTimePrice, monthCardPrice, oneStarDustPrice, twoStarDustPrice, threeStarDustPrice, fourStarDustPrice, fiveStarDustPrice, sixStarDustPrice,
	oneResourcePrice, twoResourcePrice, threeResourcePrice, fourResourcePrice, fiveResourcePrice, sixResourcePrice,
	oneSilverFeatherPrice, twoSilverFeatherPrice, threeSilverFeatherPrice, fourSilverFeatherPrice, fiveSilverFeatherPrice, sixSilverFeatherPrice;
	
	public TextAsset textFile;
	public string[] textLines;
	
	// Use this for initialization
	void Start () 
	{
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}
		
		print(textFile.text);

		firstTime.text = (textLines [0] + "\n" + textLines [1]);
		firstTimePrice.text = (textLines [2]);

		monthCard.text = (textLines [4]);
		monthCardPrice.text = (textLines [5]);

		oneStarDust.text = (textLines [7]);
		oneStarDustPrice.text = (textLines [8]);

		twoStarDust.text = (textLines [10]);
		twoStarDustPrice.text = (textLines [11]);

		threeStarDust.text = (textLines [13]);
		threeStarDustPrice.text = (textLines [14]);

		fourStarDust.text = (textLines [16]);
		fourStarDustPrice.text = (textLines [17]);

		fiveStarDust.text = (textLines [19]);
		fiveStarDustPrice.text = (textLines [20]);

		sixStarDust.text = (textLines [22]);
		sixStarDustPrice.text = (textLines [23]);

		oneResource.text = (textLines [25]);
		oneResourcePrice.text = (textLines [26]);

		twoResource.text = (textLines [28]);
		twoResourcePrice.text = (textLines [29]);

		threeResource.text = (textLines [31]);
		threeResourcePrice.text = (textLines [32]);

		fourResource.text = (textLines [34]);
		fourResourcePrice.text = (textLines [35]);

		fiveResource.text = (textLines [37]);
		fiveResourcePrice.text = (textLines [38]);

		sixResource.text = (textLines [40]);
		sixResourcePrice.text = (textLines [41]);

		oneSilverFeather.text = (textLines [43]);
		oneSilverFeatherPrice.text = (textLines [44]);

		twoSilverFeather.text = (textLines [46]);
		twoSilverFeatherPrice.text = (textLines [47]);

		threeSilverFeather.text = (textLines [49]);
		threeSilverFeatherPrice.text = (textLines [50]);

		fourSilverFeather.text = (textLines [52]);
		fourSilverFeatherPrice.text = (textLines [53]);

		fiveSilverFeather.text = (textLines [55]);
		fiveSilverFeatherPrice.text = (textLines [56]);

		sixSilverFeather.text = (textLines [58]);
		sixSilverFeatherPrice.text = (textLines [59]);		
	}
	
}