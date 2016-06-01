using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using Facebook.Unity;

public class CharacterPage : MonoBehaviour {
	public Image img;
	public Text levelText;
	public Text IQText;
	public Text LeadershipText;
	public Text PrestigeText;
	public static Sprite avatar = null;
	Game game;
	// Use this for initialization
	void Start () {
		CallCharacterPage ();
	}

	public void CallCharacterPage(){
		game = Game.Instance;
		if (FB.IsLoggedIn) {
			FB.API("me/picture?type=square&height=200&width=200", HttpMethod.GET, FbGetPicture);
		}
		levelText.text = UserLevelCalculator (game.login.exp).ToString();
		IQText.text = game.login.attributes ["IQ"];
		LeadershipText.text = game.login.attributes ["Leadership"];
		PrestigeText.text = game.login.attributes ["Prestige"];
	}

	// Update is called once per frame
	void Update () {
		if (CharacterPage.avatar != null) {
			img.sprite = CharacterPage.avatar;
			CharacterPage.avatar = null;
		}
	}

	public void FbGetPicture(IGraphResult result){
		if (result.Error == null) {
			Sprite avatar = Sprite.Create (result.Texture, new Rect (0, 0, 200, 200), new Vector2 ());
			CharacterPage.avatar = avatar;
		}else{
			Debug.Log(result.Error);
		}
	}

	public static int UserLevelCalculator(int exp){
		int level = 0;
		if (exp <41) {
			level = 0;   
		}else if (exp <93) {
			level = 1;   
		}else if (exp <121) {
			level = 2;   
		}else if (exp <157) {
			level = 3;   
		}else if (exp <203) {
			level = 4;   
		}else if (exp <264) {
			level = 5;   
		}else if (exp <343) {
			level = 6;   
		}else if (exp <445) {
			level = 7;   
		}else if (exp <578) {
			level = 8;   
		}else if (exp <751) {
			level = 9;   
		}else if (exp <976) {
			level = 10;   
		}else if (exp <1269) {
			level = 11;   
		}else if (exp <1650) {
			level = 12;   
		}else if (exp <2145) {
			level = 13;   
		}else if (exp <2788) {
			level = 14;   
		}else if (exp <3623) {
			level = 15;   
		}else if (exp <4710) {
			level = 16;   
		}else if (exp <6123) {
			level = 17;   
		}else if (exp <7959) {
			level = 18;   
		}else if (exp <10347) {
			level = 19;   
		}else if (exp <13451) {
			level = 20;   
		}else if (exp <17486) {
			level = 21;   
		}else if (exp <22731) {
			level = 22;   
		}else if (exp <29550) {
			level = 23;   
		}else if (exp <38415) {
			level = 24;   
		}else if (exp <49939) {
			level = 25;   
		}else if (exp <64920) {
			level = 26;   
		}else if (exp <84395) {
			level = 27;   
		}else if (exp <109714) {
			level = 28;   
		}else if (exp <142628) {
			level = 29;   
		}else if (exp <185416) {
			level = 30;   
		}else if (exp <241041) {
			level = 31;   
		}else if (exp <313353) {
			level = 32;   
		}else if (exp <407358) {
			level = 33;   
		}else if (exp <529565) {
			level = 34;   
		}else if (exp <688434) {
			level = 35;   
		}else if (exp <894964) {
			level = 36;   
		}else if (exp <1163453) {
			level = 37;   
		}else if (exp <1512488) {
			level = 38;   
		}else if (exp <1966235) {
			level = 39;   
		}else if (exp <2556106) {
			level = 40;   
		}else if (exp <3322937) {
			level = 41;   
		}else if (exp <4319817) {
			level = 42;   
		}else if (exp <5615762) {
			level = 43;   
		}else if (exp <7300491) {
			level = 44;   
		}else if (exp <9490638) {
			level = 45;   
		}else if (exp <12337828) {
			level = 46;   
		}else if (exp <16039176) {
			level = 47;   
		}else if (exp <20850929) {
			level = 48;   
		}else if (exp <27106207) {
			level = 49;   
		}else if (exp <35238069) {
			level = 50;   
		}else if (exp <45809490) {
			level = 51;   
		}else if (exp <59552336) {
			level = 52;   
		}else if (exp <77418036) {
			level = 53;   
		}else if (exp <100643447) {
			level = 54;   
		}else if (exp <130836481) {
			level = 55;   
		}else if (exp <170087425) {
			level = 56;   
		}else if (exp <221113652) {
			level = 57;   
		}else if (exp <287447748) {
			level = 58;   
		}else if (exp <373682072) {
			level = 59;   
		}else if (exp <485786693) {
			level = 60;   
		}else if (exp <631522701) {
			level = 61;   
		}else if (exp <820979511) {
			level = 62;   
		}else if (exp <1067273364) {
			level = 63;   
		}else if (exp <1387455373) {
			level = 64;   
		}else if (exp <1803691984) {
			level = 65;   
		}else if (exp <2344799579) {
			level = 66;   
		}else if (exp <3048239453) {
			level = 67;   
		}else if (exp <3962711288) {
			level = 68;   
		}else if (exp <5151524674) {
			level = 69;   
		}else if (exp <6696982076) {
			level = 70;   
		}else if (exp <8706076698) {
			level = 71;   
		}else if (exp <11317899707) {
			level = 72;   
		}else if (exp <14713269620) {
			level = 73;   
		}else if (exp <19127250506) {
			level = 74;   
		}else if (exp <24865425657) {
			level = 75;   
		}else if (exp <32325053354) {
			level = 76;   
		}else if (exp <42022569360) {
			level = 77;   
		}else if (exp <54629340167) {
			level = 78;   
		}else if (exp <71018142217) {
			level = 79;   
		}else if (exp <92323584882) {
			level = 80;   
		}else if (exp <120020660346) {
			level = 81;   
		}else if (exp <156026858449) {
			level = 82;   
		}else if (exp <202834915983) {
			level = 83;   
		}else if (exp <263685390778) {
			level = 84;   
		}else if (exp <342791008011) {
			level = 85;   
		}else if (exp <445628310414) {
			level = 86;   
		}else if (exp <579316803538) {
			level = 87;   
		}else if (exp <753111844600) {
			level = 88;   
		}else if (exp <979045397980) {
			level = 89;   
		}else if (exp <1272759017373) {
			level = 90;   
		}else if (exp <1654586722585) {
			level = 91;   
		}else if (exp <2150962739361) {
			level = 92;   
		}else if (exp <2796251561169) {
			level = 93;   
		}else if (exp <3635127029519) {
			level = 94;   
		}else if (exp <4725665138374) {
			level = 95;   
		}else if (exp <6143364679886) {
			level = 96;   
		}else if (exp <7986374083852) {
			level = 97;   
		}else if (exp <10382286309007) {
			level = 98;   
		}else{
			level = 99;   
		}
		return level+1;
	}
}
