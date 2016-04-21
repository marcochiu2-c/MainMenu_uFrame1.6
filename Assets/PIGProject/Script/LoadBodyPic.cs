using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Utility script to load head pic sprite to the script for later use.


public class LoadBodyPic : MonoBehaviour
{
	public Dictionary<int,Sprite> imageDict;
	public Dictionary<int,string> nameDict;
	// Use this for initialization

	private string url;

	void Start ()
	{
		url = System.IO.Path.Combine(Application.dataPath, "Resources/Characters/也先帖木兒");

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public LoadBodyPic(){
	}

	private static readonly LoadBodyPic s_Instance = new LoadBodyPic();
	
	public static LoadBodyPic Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public Sprite YaSinTipMukYi;
	public Sprite YuHim;
	public Sprite NgTseSeui;
	public Sprite BakPei;
	public Sprite SinJan;
	public Sprite SinYing;
	public Sprite LauBakWan;
	public Sprite LauBingChung;
	public Sprite SiNeiGin;
	public Sprite SiSiMing;
	public Sprite SiManShui;
	public Sprite SiMaYi;
	public Sprite NgSaamGwai;
	public Sprite NgHei;
	public Sprite LuiBo;
	public Sprite ChowAhFu;
	public Sprite ChowYu;
	public Sprite ChowSauYing;
	public Sprite JitBit;
	public Sprite SinHongShun;
	public Sprite WongShekGong;
	public Sprite HaHouDun;
	public Sprite GeungTseNga;
	public Sprite SuenMo;
	public Sprite SuenBan;
	public Sprite OnLukShan;
	public Sprite JungJaak;
	public Sprite WaiLiuTse;
	public Sprite WaiChiGingDak;
	public Sprite NgokFei;
	public Sprite SheungYuChun;
	public Sprite LimPaul;
	public Sprite CheungShukYe;
	public Sprite CheungLeung;
	public Sprite CheungLiu;
	public Sprite CheungHop;
	public Sprite CheungFei;
	public Sprite ChuiTat;
	public Sprite NgouLoi;
	public Sprite MoYungYinChiu;
	public Sprite ChikGaiGwong;
	public Sprite SiLong;
	public Sprite ChoGwai;
	public Sprite TsengKwokFan;
	public Sprite ChuBunDeui;
	public Sprite LeeSaiJik;
	public Sprite LeeKwongLee;
	public Sprite LeeManChung;
	public Sprite LeeGingLung;
	public Sprite LeeMuk;
	public Sprite LeeSauNing;
	public Sprite LeeJiShing;
	public Sprite YeungJiCheung;
	public Sprite YeungKwokChung;
	public Sprite YeungYip;
	public Sprite YeungSou;
	public Sprite YeungHou;
	public Sprite LokNgai;
	public Sprite DikChing;
	public Sprite WongBakDong;
	public Sprite WongFaChing;
	public Sprite WongYuenMou;
	public Sprite WongYi;
	public Sprite WongChin;
	public Sprite GumNing;
	public Sprite TinYeungJeui;
	public Sprite ButJoiYu;
	public Sprite PakHei;
	public Sprite ChunLeungYuk;
	public Sprite ChingNgauGam;
	public Sprite MukGwaiYing;
	public Sprite GunChung;
	public Sprite YeLuChucai;
	public Sprite FaMukLan;
	public Sprite MiuFun;
	public Sprite FanTseng;
	public Sprite FanManChing;
	public Sprite FanManFu;
	public Sprite FanYi;
	public Sprite FanYung;
	public Sprite MongTim;
	public Sprite SitYanGwai;
	public Sprite WaiChing;
	public Sprite YuenShungWun;
	public Sprite YuenYingTai;
	public Sprite ChuGotLeung;
	public Sprite HoYeukBut;
	public Sprite ChoiSheungA;
	public Sprite ChiuChe;
	public Sprite ChiuTim;
	public Sprite ChiuWan;
	public Sprite SanYeungSau;
	public Sprite KwokTseYi;
	public Sprite ChengYanTai;
	public Sprite ChengShingGong;
	public Sprite KwanYu;
	public Sprite ChanHingChi;
	public Sprite FokHuiBang;
	public Sprite HonSaiChung;
	public Sprite HongYu;
	public Sprite FeiLim;
	public Sprite MaChiu;
	public Sprite SinYuChungTong;
	public Sprite WongChung;
	public Sprite PongDak;
	public Sprite PongGyun;
	public Sprite PongTung;
	public Sprite CheungSaiKit;

	public static LoadBodyPic SetCharacters(){
		LoadBodyPic bodyPic = LoadBodyPic.Instance;
		bodyPic.YaSinTipMukYi  = Instantiate( Resources.Load<Sprite>("Characters/也先帖木兒/YaSinTipMukYi")) as Sprite;
		bodyPic.YuHim  = Instantiate( Resources.Load<Sprite>("Characters/于謙/YuHim")) as Sprite;
		bodyPic.NgTseSeui  = Instantiate( Resources.Load<Sprite>("Characters/伍子胥/NgTseSeui")) as Sprite;
		bodyPic.BakPei  = Instantiate( Resources.Load<Sprite>("Characters/伯嚭/BakPei")) as Sprite;
		bodyPic.SinJan  = Instantiate( Resources.Load<Sprite>("Characters/先軫/SinJan")) as Sprite;
		bodyPic.SinYing  = Instantiate( Resources.Load<Sprite>("Characters/冼英/SinYing")) as Sprite;
		bodyPic.LauBakWan  = Instantiate( Resources.Load<Sprite>("Characters/劉基/LauBakWan")) as Sprite;
		bodyPic.LauBingChung  = Instantiate( Resources.Load<Sprite>("Characters/劉秉忠/LauBingChung")) as Sprite;
		bodyPic.SiNeiGin  = Instantiate( Resources.Load<Sprite>("Characters/史彌堅/SiNeiGin")) as Sprite;
		bodyPic.SiSiMing  = Instantiate( Resources.Load<Sprite>("Characters/史思明/SiSiMing")) as Sprite;
		bodyPic.SiManShui  = Instantiate( Resources.Load<Sprite>("Characters/史萬歲/SiManShui")) as Sprite;
		bodyPic.SiMaYi  = Instantiate( Resources.Load<Sprite>("Characters/司馬懿/SiMaYi")) as Sprite;
		bodyPic.NgSaamGwai  = Instantiate( Resources.Load<Sprite>("Characters/吳三桂/NgSaamGwai")) as Sprite;
		bodyPic.NgHei  = Instantiate( Resources.Load<Sprite>("Characters/吳起/NgHei")) as Sprite;
		bodyPic.LuiBo  = Instantiate( Resources.Load<Sprite>("Characters/呂布/LuiBo")) as Sprite;
		bodyPic.ChowAhFu  = Instantiate( Resources.Load<Sprite>("Characters/周亞夫/ChowAhFu")) as Sprite;
		bodyPic.ChowYu = Instantiate( Resources.Load<Sprite>("Characters/周瑜/ChowYu-Final2")) as Sprite;
		bodyPic.ChowSauYing  = Instantiate( Resources.Load<Sprite>("Characters/周秀英/ChowSauYing")) as Sprite;
		bodyPic.JitBit  = Instantiate( Resources.Load<Sprite>("Characters/哲別/JitBit")) as Sprite;
		bodyPic.SinHongShun  = Instantiate( Resources.Load<Sprite>("Characters/單雄信/SinHongShun")) as Sprite;
		bodyPic.WongShekGong  = Instantiate( Resources.Load<Sprite>("Characters/圮上老人.黃石公/WongShekGong")) as Sprite;
		bodyPic.HaHouDun = Instantiate( Resources.Load<Sprite>("Characters/夏侯惇/HaHouDun")) as Sprite;
		bodyPic.GeungTseNga  = Instantiate( Resources.Load<Sprite>("Characters/姜子牙/GeungTseNga")) as Sprite;
		bodyPic.SuenMo  = Instantiate( Resources.Load<Sprite>("Characters/孫武/SuenMo")) as Sprite;
		bodyPic.SuenBan  = Instantiate( Resources.Load<Sprite>("Characters/孫臏/SuenBan")) as Sprite;
		bodyPic.OnLukShan  = Instantiate( Resources.Load<Sprite>("Characters/安祿山/OnLukShan")) as Sprite;
		bodyPic.JungJaak = Instantiate( Resources.Load<Sprite>("Characters/宗澤/JungJaak")) as Sprite;
		bodyPic.WaiLiuTse  = Instantiate( Resources.Load<Sprite>("Characters/尉繚子/WaiLiuTse")) as Sprite;
		bodyPic.WaiChiGingDak  = Instantiate( Resources.Load<Sprite>("Characters/尉遲敬德/WaiChiGingDak")) as Sprite;
		bodyPic.NgokFei  = Instantiate( Resources.Load<Sprite>("Characters/岳飛/NgokFei")) as Sprite;
		bodyPic.SheungYuChun  = Instantiate( Resources.Load<Sprite>("Characters/常遇春/SheungYuChun")) as Sprite;
		bodyPic.LimPaul = Instantiate( Resources.Load<Sprite>("Characters/廉頗/LimPaul")) as Sprite;
		bodyPic.CheungShukYe  = Instantiate( Resources.Load<Sprite>("Characters/張叔夜/CheungSukYe")) as Sprite;
		bodyPic.CheungLeung  = Instantiate( Resources.Load<Sprite>("Characters/張良/CheungLeung")) as Sprite;
		bodyPic.CheungLiu  = Instantiate( Resources.Load<Sprite>("Characters/張遼/CheungLiu")) as Sprite;
		bodyPic.CheungHop  = Instantiate( Resources.Load<Sprite>("Characters/張郃/CheungHop")) as Sprite;
		bodyPic.CheungFei = Instantiate( Resources.Load<Sprite>("Characters/張飛/CheungFei")) as Sprite;
		bodyPic.ChuiTat  = Instantiate( Resources.Load<Sprite>("Characters/徐達/ChuiTat")) as Sprite;
		bodyPic.NgouLoi  = Instantiate( Resources.Load<Sprite>("Characters/惡來/NgouLoi")) as Sprite;
		bodyPic.MoYungYinChiu  = Instantiate( Resources.Load<Sprite>("Characters/慕容延釗/MoYungYinChiu")) as Sprite;
		bodyPic.ChikGaiGwong  = Instantiate( Resources.Load<Sprite>("Characters/戚繼光/ChikGaiGwong")) as Sprite;
		bodyPic.SiLong = Instantiate( Resources.Load<Sprite>("Characters/施琅/SiLong")) as Sprite;
		bodyPic.ChoGwai  = Instantiate( Resources.Load<Sprite>("Characters/曹劌/ChoGwai")) as Sprite;
		bodyPic.TsengKwokFan  = Instantiate( Resources.Load<Sprite>("Characters/曾國藩/TsengKwokFan")) as Sprite;
		bodyPic.ChuBunDeui  = Instantiate( Resources.Load<Sprite>("Characters/朱般懟/ChuBunDeui")) as Sprite;
		bodyPic.LeeSaiJik  = Instantiate( Resources.Load<Sprite>("Characters/李勣/LeeSaiJik")) as Sprite;
		bodyPic.LeeKwongLee = Instantiate( Resources.Load<Sprite>("Characters/李廣利/LeeKwongLee")) as Sprite;
		bodyPic.LeeManChung  = Instantiate( Resources.Load<Sprite>("Characters/李文忠/LeeManChung")) as Sprite;
		bodyPic.LeeGingLung  = Instantiate( Resources.Load<Sprite>("Characters/李景隆/LeeGingLung")) as Sprite;
		bodyPic.LeeMuk  = Instantiate( Resources.Load<Sprite>("Characters/李牧/LeeMuk")) as Sprite;
		bodyPic.LeeSauNing  = Instantiate( Resources.Load<Sprite>("Characters/李秀寧/LeeSauNing")) as Sprite;
		bodyPic.LeeJiShing = Instantiate( Resources.Load<Sprite>("Characters/李自成/LeeJiShing")) as Sprite;
		bodyPic.YeungJiCheung  = Instantiate( Resources.Load<Sprite>("Characters/楊嗣昌/YeungJiCheung")) as Sprite;
		bodyPic.YeungKwokChung  = Instantiate( Resources.Load<Sprite>("Characters/楊國忠/YeungKwokChung")) as Sprite;
		bodyPic.YeungYip  = Instantiate( Resources.Load<Sprite>("Characters/楊業/YeungYip")) as Sprite;
		bodyPic.YeungSou  = Instantiate( Resources.Load<Sprite>("Characters/楊素/YeungSou")) as Sprite;
		bodyPic.YeungHou = Instantiate( Resources.Load<Sprite>("Characters/楊鎬/YeungHou")) as Sprite;
		bodyPic.LokNgai  = Instantiate( Resources.Load<Sprite>("Characters/樂毅/LokNgai")) as Sprite;
		bodyPic.DikChing  = Instantiate( Resources.Load<Sprite>("Characters/狄青/DikChing")) as Sprite;
		bodyPic.WongBakDong  = Instantiate( Resources.Load<Sprite>("Characters/王伯當/WongBakDong")) as Sprite;
		bodyPic.WongFaChing  = Instantiate( Resources.Load<Sprite>("Characters/王化貞/WongFaChing")) as Sprite;
		bodyPic.WongYuenMou = Instantiate( Resources.Load<Sprite>("Characters/王玄謨/WongYuenMou")) as Sprite;
		bodyPic.WongYi  = Instantiate( Resources.Load<Sprite>("Characters/王異/WongYi")) as Sprite;
		bodyPic.WongChin  = Instantiate( Resources.Load<Sprite>("Characters/王翦/WongChin")) as Sprite;
		bodyPic.GumNing  = Instantiate( Resources.Load<Sprite>("Characters/甘寧/GumNing")) as Sprite;
		bodyPic.TinYeungJeui  = Instantiate( Resources.Load<Sprite>("Characters/田穰苴/TinYeungJeui")) as Sprite;
		bodyPic.ButJoiYu = Instantiate( Resources.Load<Sprite>("Characters/畢再遇/ButJoiYu")) as Sprite;
		bodyPic.PakHei  = Instantiate( Resources.Load<Sprite>("Characters/白起/PakHei-1")) as Sprite;
		bodyPic.ChunLeungYuk  = Instantiate( Resources.Load<Sprite>("Characters/秦良玉/ChunLeungYuk")) as Sprite;
		bodyPic.ChingNgauGam  = Instantiate( Resources.Load<Sprite>("Characters/程咬金/ChingNgauGam")) as Sprite;
		bodyPic.MukGwaiYing  = Instantiate( Resources.Load<Sprite>("Characters/穆桂英/MukGwaiYing")) as Sprite;
		bodyPic.GunChung = Instantiate( Resources.Load<Sprite>("Characters/管仲/GunChung")) as Sprite;
		bodyPic.YeLuChucai  = Instantiate( Resources.Load<Sprite>("Characters/耶律楚材/YeLuChucai")) as Sprite;
		bodyPic.FaMukLan  = Instantiate( Resources.Load<Sprite>("Characters/花木蘭/FaMukLan2")) as Sprite;
		bodyPic.MiuFun  = Instantiate( Resources.Load<Sprite>("Characters/苗訓/MiuFun")) as Sprite;
		bodyPic.FanTseng  = Instantiate( Resources.Load<Sprite>("Characters/范增/FanTseng")) as Sprite;
		bodyPic.FanManChing = Instantiate( Resources.Load<Sprite>("Characters/范文程/FanManChing")) as Sprite;
		bodyPic.FanManFu  = Instantiate( Resources.Load<Sprite>("Characters/范文虎/FanManFu")) as Sprite;
		bodyPic.FanYi  = Instantiate( Resources.Load<Sprite>("Characters/范蠡/FanYi")) as Sprite;
		bodyPic.FanYung  = Instantiate( Resources.Load<Sprite>("Characters/范雍/FanYung")) as Sprite;
		bodyPic.MongTim  = Instantiate( Resources.Load<Sprite>("Characters/蒙恬/MongTim")) as Sprite;
		bodyPic.SitYanGwai = Instantiate( Resources.Load<Sprite>("Characters/薛仁貴/SitYanGwai")) as Sprite;
		bodyPic.WaiChing  = Instantiate( Resources.Load<Sprite>("Characters/衛青/WaiChing")) as Sprite;
		bodyPic.YuenShungWun  = Instantiate( Resources.Load<Sprite>("Characters/袁崇煥/YuenShungWun")) as Sprite;
		bodyPic.YuenYingTai  = Instantiate( Resources.Load<Sprite>("Characters/袁應泰/YuenYingTai")) as Sprite;
		bodyPic.ChuGotLeung  = Instantiate( Resources.Load<Sprite>("Characters/諸葛亮/ChuGotLeung")) as Sprite;
		bodyPic.HoYeukBut = Instantiate( Resources.Load<Sprite>("Characters/賀若弼/HoYeukBut")) as Sprite;
		bodyPic.ChoiSheungA  = Instantiate( Resources.Load<Sprite>("Characters/賽尚阿/ChoiSheungA")) as Sprite;
		bodyPic.ChiuChe  = Instantiate( Resources.Load<Sprite>("Characters/趙奢/ChiuChe")) as Sprite;
		bodyPic.ChiuTim  = Instantiate( Resources.Load<Sprite>("Characters/趙括/ChiuTim2")) as Sprite;
		bodyPic.ChiuWan  = Instantiate( Resources.Load<Sprite>("Characters/趙雲/ChiuWan2")) as Sprite;
		bodyPic.SanYeungSau = Instantiate( Resources.Load<Sprite>("Characters/辰漾守/SanYeungSau")) as Sprite;
		bodyPic.KwokTseYi  = Instantiate( Resources.Load<Sprite>("Characters/郭子儀/KwokTseYi")) as Sprite;
		bodyPic.ChengYanTai  = Instantiate( Resources.Load<Sprite>("Characters/鄭仁泰/ChengYanTai")) as Sprite;
		bodyPic.ChengShingGong  = Instantiate( Resources.Load<Sprite>("Characters/鄭成功/ChengShingGong3")) as Sprite;
		bodyPic.ChanHingChi  = Instantiate( Resources.Load<Sprite>("Characters/陳慶之/ChanHingChi")) as Sprite;
		bodyPic.FokHuiBang = Instantiate( Resources.Load<Sprite>("Characters/霍去病/FokHuiBang")) as Sprite;
		bodyPic.HonSaiChung  = Instantiate( Resources.Load<Sprite>("Characters/韓世忠/HonSaiChung")) as Sprite;
		bodyPic.HongYu  = Instantiate( Resources.Load<Sprite>("Characters/項羽/HongYu2")) as Sprite;
		bodyPic.FeiLim  = Instantiate( Resources.Load<Sprite>("Characters/飛廉/FeiLim")) as Sprite;
		bodyPic.MaChiu  = Instantiate( Resources.Load<Sprite>("Characters/馬超/MaChiu")) as Sprite;
		bodyPic.SinYuChungTong = Instantiate( Resources.Load<Sprite>("Characters/鮮于仲通/SinYuChungTong")) as Sprite;
		bodyPic.WongChung  = Instantiate( Resources.Load<Sprite>("Characters/黃忠/WongChung")) as Sprite;
		bodyPic.PongDak  = Instantiate( Resources.Load<Sprite>("Characters/龐德/PongDak")) as Sprite;
		bodyPic.PongGyun  = Instantiate( Resources.Load<Sprite>("Characters/龐涓/PongGyun")) as Sprite;
		bodyPic.PongTung  = Instantiate( Resources.Load<Sprite>("Characters/龐統/PongTung")) as Sprite;
		bodyPic.KwanYu = Instantiate( Resources.Load<Sprite>("Characters/關羽/關公")) as Sprite;
		bodyPic._setCharacters ();

		return bodyPic;
	}

	public void _setCharacters(){
		imageDict = new Dictionary<int,Sprite>();
		nameDict = new Dictionary<int,string> ();
		// Add some images.
		imageDict.Add (/*"也先帖木兒",*/1047, YaSinTipMukYi);
		imageDict.Add (/*"于謙",*/25, YuHim);
		imageDict.Add (/*"伍子胥",*/4, NgTseSeui);
		imageDict.Add (/*"伯嚭",*/1074, BakPei);
		imageDict.Add (/*"先軫",*/1068, SinJan);
		imageDict.Add (/*"冼英",*/1059, SinYing);
		imageDict.Add (/*"劉基",*/11, LauBakWan);
		imageDict.Add (/*"劉秉忠",*/14, LauBingChung);
		imageDict.Add (/*"史彌堅",*/1066, SiNeiGin);
		imageDict.Add (/*"史思明",*/1028, SiSiMing);
		imageDict.Add (/*"史萬歲",*/1075, SiManShui);
		imageDict.Add (/*"司馬懿",*/23, SiMaYi);
		imageDict.Add (/*"吳三桂",*/1054, NgSaamGwai);
		imageDict.Add (/*"吳起 ",*/2, NgHei);
		imageDict.Add (/*"呂布",*/1019, LuiBo);
		imageDict.Add (/*"周亞夫",*/1011, ChowAhFu);
		imageDict.Add (/*"周瑜",*/24, ChowYu);
		imageDict.Add (/*"周秀英",*/1056, ChowSauYing);
		imageDict.Add (/*"哲別",*/1035, JitBit);
		imageDict.Add (/*"單雄信",*/1070, SinHongShun);
		imageDict.Add (/*"黃石公",*/18, WongShekGong);
		imageDict.Add (/*"夏侯惇",*/1020, HaHouDun);
		imageDict.Add (/*"姜子牙",*/1, GeungTseNga);
		imageDict.Add (/*"孫武",*/3, SuenMo);
		imageDict.Add (/*"孫臏",*/9, SuenBan);
		imageDict.Add (/*"安祿山",*/1027, OnLukShan);
		imageDict.Add (/*"宗澤",*/1063, JungJaak);
		imageDict.Add (/*"尉繚子",*/19, WaiLiuTse);
		imageDict.Add (/*"尉遲敬德",*/1025, WaiChiGingDak);
		imageDict.Add (/*"岳飛",*/1032, NgokFei);
		imageDict.Add (/*"常遇春",*/1038, SheungYuChun);
		imageDict.Add (/*"廉頗",*/1007, LimPaul);
		imageDict.Add (/*"張叔夜",*/1065, CheungShukYe);
		imageDict.Add (/*"張良",*/10, CheungLeung);
		imageDict.Add (/*"張遼",*/1078, CheungLiu);
		imageDict.Add (/*"張郃",*/1022, CheungHop);
		imageDict.Add (/*"張飛",*/1015, CheungFei);
		imageDict.Add (/*"徐達",*/1036, ChuiTat);
		imageDict.Add (/*"惡來",*/1073, NgouLoi);
		imageDict.Add (/*"慕容延釗",*/1067, MoYungYinChiu);
		imageDict.Add (/*"戚繼光",*/1039, ChikGaiGwong);
		imageDict.Add (/*"施琅",*/1061, SiLong);
		imageDict.Add (/*"曹劌",*/5, ChoGwai);
		imageDict.Add (/*"曾國藩",*/17, TsengKwokFan);
		imageDict.Add (/*"朱般懟",*/28, ChuBunDeui);
		imageDict.Add (/*"李勣",*/1024, LeeSaiJik);
		imageDict.Add (/*"李廣利",*/1042, LeeKwongLee);
		imageDict.Add (/*"李文忠",*/1037, LeeManChung);
		imageDict.Add (/*"李景隆",*/1048, LeeGingLung);
		imageDict.Add (/*"李牧",*/1008, LeeMuk);
		imageDict.Add (/*"李秀寧",*/1058, LeeSauNing);
		imageDict.Add (/*"李自成",*/1055, LeeJiShing);
		imageDict.Add (/*"楊嗣昌",*/1052, YeungJiCheung);
		imageDict.Add (/*"楊國忠",*/26, YeungKwokChung);
		imageDict.Add (/*"楊業",*/1030, YeungYip);
		imageDict.Add (/*"楊素",*/1077, YeungSou);
		imageDict.Add (/*"楊鎬",*/1049, YeungHou);
		imageDict.Add (/*"樂毅",*/1003, LokNgai);
		imageDict.Add (/*"狄青",*/1031, DikChing);
		imageDict.Add (/*"王伯當",*/1069, WongBakDong);
		imageDict.Add (/*"王化貞",*/1050, WongFaChing);
		imageDict.Add (/*"王玄謨",*/1043, WongYuenMou);
		imageDict.Add (/*"王異",*/1060, WongYi);
		imageDict.Add (/*"王翦",*/1009, WongChin);
		imageDict.Add (/*"甘寧",*/1023, GumNing);
		imageDict.Add (/*"田穰苴",*/20, TinYeungJeui);
		imageDict.Add (/*"畢再遇",*/1062, ButJoiYu);
		imageDict.Add (/*"白起",*/1006, PakHei);
		imageDict.Add (/*"秦良玉",*/1057, ChunLeungYuk);
		imageDict.Add (/*"程咬金",*/1071, ChingNgauGam);
		imageDict.Add (/*"穆桂英",*/1033, MukGwaiYing);
		imageDict.Add (/*"管仲",*/6, GunChung);
		imageDict.Add (/*"耶律楚材",*/15, YeLuChucai);
		imageDict.Add (/*"花木蘭",*/1002, FaMukLan);
		imageDict.Add (/*"苗訓",*/13, MiuFun);
		imageDict.Add (/*"范增",*/12, FanTseng);
		imageDict.Add (/*"范文程",*/16, FanManChing);
		imageDict.Add (/*"范文虎",*/1046, FanManFu);
		imageDict.Add (/*"范蠡",*/7, FanYi);
		imageDict.Add (/*"范雍",*/1045, FanYung);
		imageDict.Add (/*"蒙恬",*/1004, MongTim);
		imageDict.Add (/*"薛仁貴",*/1026, SitYanGwai);
		imageDict.Add (/*"衛青",*/1012, WaiChing);
		imageDict.Add (/*"袁崇煥",*/1040, YuenShungWun);
		imageDict.Add (/*"袁應泰",*/1051, YuenYingTai);
		imageDict.Add (/*"諸葛亮",*/8, ChuGotLeung);
		imageDict.Add (/*"賀若弼",*/1076, HoYeukBut);
		imageDict.Add (/*"賽尚阿",*/1053, ChoiSheungA);
		imageDict.Add (/*"趙奢",*/1005, ChiuChe);
		imageDict.Add (/*"趙括",*/27, ChiuTim);
		imageDict.Add (/*"趙雲",*/1016, ChiuWan);
		imageDict.Add (/*"辰漾守",*/29, SanYeungSau);
		imageDict.Add (/*"郭子儀",*/1029, KwokTseYi);
		imageDict.Add (/*"鄭仁泰",*/1064, ChengYanTai);
		imageDict.Add (/*"鄭成功",*/1041, ChengShingGong);
		imageDict.Add (/*"陳慶之",*/1001, ChanHingChi);
		imageDict.Add (/*"霍去病",*/1013, FokHuiBang);
		imageDict.Add (/*"韓世忠",*/1034, HonSaiChung);
		imageDict.Add (/*"項羽",*/1010, HongYu);
		imageDict.Add (/*"飛廉",*/1072, FeiLim);
		imageDict.Add (/*"馬超",*/1017, MaChiu);
		imageDict.Add (/*"鮮于仲通",*/1044, SinYuChungTong);
		imageDict.Add (/*"黃忠",*/1018, WongChung);
		imageDict.Add (/*"龐德",*/1021, PongDak);
		imageDict.Add (/*"龐涓",*/21, PongGyun);
		imageDict.Add (/*"龐統",*/22, PongTung);
		imageDict.Add (/*"關羽",*/1014, KwanYu);
		
		nameDict.Add (1047, "也先帖木兒");
		nameDict.Add (25, "于謙");
		nameDict.Add (4, "伍子胥");
		nameDict.Add (1074, "伯嚭");
		nameDict.Add (1068, "先軫");
		nameDict.Add (1059, "冼英");
		nameDict.Add (11, "劉基");
		nameDict.Add (14, "劉秉忠");
		nameDict.Add (1066, "史彌堅");
		nameDict.Add (1028, "史思明");
		nameDict.Add (1075, "史萬歲");
		nameDict.Add (23, "司馬懿");
		nameDict.Add (1054, "吳三桂");
		nameDict.Add (2, "吳起");
		nameDict.Add (1019, "呂布");
		nameDict.Add (1011, "周亞夫");
		nameDict.Add (24, "周瑜");
		nameDict.Add (1056, "周秀英");
		nameDict.Add (1035, "哲別");
		nameDict.Add (1070, "單雄信");
		nameDict.Add (18, "黃石公");
		nameDict.Add (1020, "夏侯惇");
		nameDict.Add (1, "姜子牙");
		nameDict.Add (3, "孫武");
		nameDict.Add (9, "孫臏");
		nameDict.Add (1027, "安祿山");
		nameDict.Add (1063, "宗澤");
		nameDict.Add (19, "尉繚子");
		nameDict.Add (1025, "尉遲敬德");
		nameDict.Add (1032, "岳飛");
		nameDict.Add (1038, "常遇春");
		nameDict.Add (1007, "廉頗");
		nameDict.Add (1065, "張叔夜");
		nameDict.Add (10, "張良");
		nameDict.Add (1078, "張遼");
		nameDict.Add (1022, "張郃");
		nameDict.Add (1015, "張飛");
		nameDict.Add (1036, "徐達");
		nameDict.Add (1073, "惡來");
		nameDict.Add (1067, "慕容延釗");
		nameDict.Add (1039, "戚繼光");
		nameDict.Add (1061, "施琅");
		nameDict.Add (5, "曹劌");
		nameDict.Add (17, "曾國藩");
		nameDict.Add (28, "朱般懟");
		nameDict.Add (1024, "李勣");
		nameDict.Add (1042, "李廣利");
		nameDict.Add (1037, "李文忠");
		nameDict.Add (1048, "李景隆");
		nameDict.Add (1008, "李牧");
		nameDict.Add (1058, "李秀寧");
		nameDict.Add (1055, "李自成");
		nameDict.Add (1052, "楊嗣昌");
		nameDict.Add (26, "楊國忠");
		nameDict.Add (1030, "楊業");
		nameDict.Add (1077, "楊素");
		nameDict.Add (1049, "楊鎬");
		nameDict.Add (1003, "樂毅");
		nameDict.Add (1031, "狄青");
		nameDict.Add (1069, "王伯當");
		nameDict.Add (1050, "王化貞");
		nameDict.Add (1043, "王玄謨");
		nameDict.Add (1060, "王異");
		nameDict.Add (1009, "王翦");
		nameDict.Add (1023, "甘寧");
		nameDict.Add (20, "田穰苴");
		nameDict.Add (1062, "畢再遇");
		nameDict.Add (1006, "白起");
		nameDict.Add (1057, "秦良玉");
		nameDict.Add (1071, "程咬金");
		nameDict.Add (1033, "穆桂英");
		nameDict.Add (6, "管仲");
		nameDict.Add (15, "耶律楚材");
		nameDict.Add (1002, "花木蘭");
		nameDict.Add (13, "苗訓");
		nameDict.Add (12, "范增");
		nameDict.Add (16, "范文程");
		nameDict.Add (1046, "范文虎");
		nameDict.Add (7, "范蠡");
		nameDict.Add (1045, "范雍");
		nameDict.Add (1004, "蒙恬");
		nameDict.Add (1026, "薛仁貴");
		nameDict.Add (1012, "衛青");
		nameDict.Add (1040, "袁崇煥");
		nameDict.Add (1051, "袁應泰");
		nameDict.Add (8, "諸葛亮");
		nameDict.Add (1076, "賀若弼");
		nameDict.Add (1053, "賽尚阿");
		nameDict.Add (1005, "趙奢");
		nameDict.Add (27, "趙括");
		nameDict.Add (1016, "趙雲");
		nameDict.Add (29, "辰漾守");
		nameDict.Add (1029, "郭子儀");
		nameDict.Add (1064, "鄭仁泰");
		nameDict.Add (1041, "鄭成功");
		nameDict.Add (1001, "陳慶之");
		nameDict.Add (1013, "霍去病");
		nameDict.Add (1034, "韓世忠");
		nameDict.Add (1010, "項羽");
		nameDict.Add (1072, "飛廉");
		nameDict.Add (1017, "馬超");
		nameDict.Add (1044, "鮮于仲通");
		nameDict.Add (1018, "黃忠");
		nameDict.Add (1021, "龐德");
		nameDict.Add (21, "龐涓");
		nameDict.Add (22, "龐統");
		nameDict.Add (1014, "關羽");
	}
}

