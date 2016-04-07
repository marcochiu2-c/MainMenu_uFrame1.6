using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using SimpleJSON;
>>>>>>> feature/MainMenu-shawn

[Serializable]
public class Products{
	public int id; // type id in database
	public string name;
	public int type; // 0: weapon, 1: armor, 2: shield
	public int price;
<<<<<<< HEAD
	public int currencyType;  // 0:時之星塵 1:資源 2:銀羽
	public string attributes;

	public Products(int i, string n, int t, int p, int c, string a=""){
=======
	public int currencyType;  // 1:銀羽 2:時之星塵 3:資源
	public JSONClass attributes;

	public Products(int i, string n, int t, int p, int c, JSONNode a){
>>>>>>> feature/MainMenu-shawn
		id = i;
		name = n;
		type = t;
		price = p;
		currencyType = c;
<<<<<<< HEAD
		attributes = a;
=======
		attributes = (JSONClass) a;
>>>>>>> feature/MainMenu-shawn
	}
}

[Serializable]
<<<<<<< HEAD
public class ProductList{
	public List<Products> products;
=======
public class ProductDict{
	public Dictionary<int,Products> products;
>>>>>>> feature/MainMenu-shawn
	const int WEAPON = 0;
	const int ARMOR  = 1;
	const int SHIELD = 2;
	const int BUILDING = 3;

	public int count { get {
			return products.Count;
		} }

<<<<<<< HEAD
	public ProductList(){
		products = new List<Products> ();
		products.Add (new Products(5001,"木棒",WEAPON,0,1,""));
		products.Add (new Products(5002,"木棍",WEAPON,0,1,""));
		products.Add (new Products(5003,"石芧",WEAPON,0,1,""));
		products.Add (new Products(5004,"長芧",WEAPON,0,1,""));
		products.Add (new Products(5005,"長槍",WEAPON,0,1,""));
		products.Add (new Products(5006,"鋼槍",WEAPON,0,1,""));
		products.Add (new Products(5007,"弓",WEAPON,0,1,""));
		products.Add (new Products(5008,"長弓",WEAPON,0,1,""));
		products.Add (new Products(5009,"十字弓",WEAPON,0,1,""));
		products.Add (new Products(5010,"石斧",WEAPON,0,1,""));
		products.Add (new Products(5011,"斧",WEAPON,0,1,""));
		products.Add (new Products(5012,"大斧",WEAPON,0,1,""));
		products.Add (new Products(5013,"鋼斧",WEAPON,0,1,""));
		products.Add (new Products(5014,"大鋼斧",WEAPON,0,1,""));
		products.Add (new Products(5015,"木劍",WEAPON,0,1,""));
		products.Add (new Products(5016,"短劍",WEAPON,0,1,""));
		products.Add (new Products(5017,"長劍",WEAPON,0,1,""));
		products.Add (new Products(5018,"花劍",WEAPON,0,1,""));
		products.Add (new Products(5019,"匕首",WEAPON,0,1,""));
		products.Add (new Products(5020,"鋼劍",WEAPON,0,1,""));
		products.Add (new Products(5021,"鋼匕首",WEAPON,0,1,""));
		products.Add (new Products(5022,"刀",WEAPON,0,1,""));
		products.Add (new Products(5023,"大關刀",WEAPON,0,1,""));
		products.Add (new Products(5024,"老牛",WEAPON,0,1,""));
		products.Add (new Products(5025,"西瓜刀",WEAPON,0,1,""));
		products.Add (new Products(5026,"生果刀",WEAPON,0,1,""));
		products.Add (new Products(5027,"彎刀",WEAPON,0,1,""));
		products.Add (new Products(5028,"圓月彎刀",WEAPON,0,1,""));
		products.Add (new Products(5029,"飛刀",WEAPON,0,1,""));
		products.Add (new Products(5030,"鋼飛刀",WEAPON,0,1,""));
		products.Add (new Products(5031,"武士刀",WEAPON,0,1,""));
		products.Add (new Products(5032,"㦸",WEAPON,0,1,""));
		products.Add (new Products(5033,"流星鎚",WEAPON,0,1,""));
		products.Add (new Products(5034,"鐡鎚",WEAPON,0,1,""));
		products.Add (new Products(5035,"鋼鎚",WEAPON,0,1,""));
		products.Add (new Products(5036,"鐧",WEAPON,0,1,""));
		products.Add (new Products(5037,"皮鞭",WEAPON,0,1,""));
		products.Add (new Products(5038,"鋼鞭",WEAPON,0,1,""));
		products.Add (new Products(6001,"鐡製西洋全身甲",ARMOR,0,1,""));
		products.Add (new Products(6002,"鋼製西洋全身甲",ARMOR,0,1,""));
		products.Add (new Products(6003,"忍者服",ARMOR,0,1,""));
		products.Add (new Products(6004,"棉甲",ARMOR,0,1,""));
		products.Add (new Products(6005,"玄甲",ARMOR,0,1,""));
		products.Add (new Products(6006,"皮甲",ARMOR,0,1,""));
		products.Add (new Products(6007,"白布甲",ARMOR,0,1,""));
		products.Add (new Products(6008,"藤甲",ARMOR,0,1,""));
		products.Add (new Products(6009,"木甲",ARMOR,0,1,""));
		products.Add (new Products(6010,"鐵片甲",ARMOR,0,1,""));
		products.Add (new Products(6011,"魚鱗甲",ARMOR,0,1,""));
		products.Add (new Products(6012,"筒袖鎧",ARMOR,0,1,""));
		products.Add (new Products(6013,"光要甲",ARMOR,0,1,""));
		products.Add (new Products(6014,"細鱗甲",ARMOR,0,1,""));
		products.Add (new Products(6015,"鎖子甲",ARMOR,0,1,""));
		products.Add (new Products(6016,"板鏈甲",ARMOR,0,1,""));
		products.Add (new Products(6017,"鳥錘甲",ARMOR,0,1,""));
		products.Add (new Products(6018,"明光甲",ARMOR,0,1,""));
		products.Add (new Products(6019,"山文甲",ARMOR,0,1,""));
		products.Add (new Products(6020,"歩人甲",ARMOR,0,1,""));
		products.Add (new Products(6021,"護心鏡",ARMOR,0,1,""));
		products.Add (new Products(6022,"頭形兜",ARMOR,0,1,""));
		products.Add (new Products(6023,"星兜",ARMOR,0,1,""));
		products.Add (new Products(6024,"突盔形兜",ARMOR,0,1,""));
		products.Add (new Products(6025,"鐵甲頭盔",ARMOR,0,1,""));
		products.Add (new Products(6026,"疾風頭盔",ARMOR,0,1,""));
		products.Add (new Products(6027,"鐵鱗頭盔",ARMOR,0,1,""));
		products.Add (new Products(6028,"白銀頭盔",ARMOR,0,1,""));
		products.Add (new Products(6029,"白銀頭盔",ARMOR,0,1,""));
		products.Add (new Products(6030,"彪騎頭盔",ARMOR,0,1,""));
		products.Add (new Products(6031,"武軍頭盔",ARMOR,0,1,""));
		products.Add (new Products(6032,"天狼頭盔",ARMOR,0,1,""));
		products.Add (new Products(7001,"皮製小圓盾",SHIELD,0,1,""));
		products.Add (new Products(7002,"皮製圓盾",SHIELD,0,1,""));
		products.Add (new Products(7003,"皮製方盾",SHIELD,0,1,""));
		products.Add (new Products(7004,"木製小圓盾",SHIELD,0,1,""));
		products.Add (new Products(7005,"銅鐵盾",SHIELD,0,1,""));
		products.Add (new Products(7006,"藤盾",SHIELD,0,1,""));
		products.Add (new Products(7007,"燕尾牌",SHIELD,0,1,""));
		products.Add (new Products(7008,"羅馬大盾",SHIELD,0,1,""));
		products.Add (new Products(7009,"少林盾牌",SHIELD,0,1,""));
		products.Add (new Products(7010,"仁王盾",SHIELD,0,1,""));
		products.Add (new Products(7011,"大木盾",SHIELD,0,1,""));
		products.Add (new Products(7012,"厚木盾",SHIELD,0,1,""));
		products.Add (new Products(7013,"桐木盾",SHIELD,0,1,""));
		products.Add (new Products(7014,"白楊盾",SHIELD,0,1,""));
		products.Add (new Products(7015,"白樺盾",SHIELD,0,1,""));
		products.Add (new Products(7016,"檀木盾",SHIELD,0,1,""));
		products.Add (new Products(7017,"青銅盾",SHIELD,0,1,""));
		products.Add (new Products(7018,"銅盾",SHIELD,0,1,""));
		products.Add (new Products(7019,"古銅盾",SHIELD,0,1,""));
		products.Add (new Products(7020,"長立盾",SHIELD,0,1,""));
		products.Add (new Products(7021,"重盾",SHIELD,0,1,""));
		products.Add (new Products(7022,"方盾",SHIELD,0,1,""));
		products.Add (new Products(7023,"手盾",SHIELD,0,1,""));
		products.Add (new Products(7024,"護身盾",SHIELD,0,1,""));
		products.Add (new Products(7025,"白紋盾",SHIELD,0,1,""));
		products.Add (new Products(7026,"護木盾",SHIELD,0,1,""));
		products.Add (new Products(7027,"桃木盾",SHIELD,0,1,""));
		products.Add (new Products(7028,"古木盾",SHIELD,0,1,""));
		products.Add (new Products(7029,"雁盾",SHIELD,0,1,""));
		products.Add (new Products(7030,"鳩盾",SHIELD,0,1,""));
		products.Add (new Products(7031,"雙圓盾",SHIELD,0,1,""));
		products.Add (new Products(7032,"銅甲盾",SHIELD,0,1,""));
		products.Add (new Products(7033,"鐵甲盾",SHIELD,0,1,""));
=======
	public ProductDict(){
		products = new Dictionary<int,Products> ();
#region Weapon
		products.Add (5001,new Products(5001,"木棒",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"木棒\",\"Grade\":1,\"Characteristics\":\"短/適合扑嘢\",\"TechnologyRequired\":\"木工\",\"Restraint\":\"女人？\",\"Category\":\"棍棒\",\"EquipShield\":true,\"Sharp\":0,\"Penetrate\":12.499875,\"PhysicalApplications\":0.87499125,\"Weight\":1.25,\"OtherHits\":9.375,\"KeyHit\":2.5,\"FatalKeyHit\":0.625,\"NumberOfProductionResources\":38,\"ProductionTime\":5}")));
		products.Add (5002,new Products(5002,"木棍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"木棍\",\"Grade\":2,\"Characteristics\":\"長/適合大棍毆\",\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"短兵器/騎兵\",\"Category\":\"棍棒\",\"EquipShield\":false,\"Sharp\":0, \"Penetrate\":24.99975,\"PhysicalApplications\":0.7499925,\"Weight\":2.5,\"OtherHits\":18.75,\"KeyHit\":5,\"FatalKeyHit\":1.25,\"NumberOfProductionResources\":75,\"ProductionTime\":20  }")));
		products.Add (5003,new Products(5003,"石芧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"石芧\",\"Grade\":1,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"短兵器/騎兵\",\"Category\":\"穿刺式長兵器\",\"EquipShield\":true,\"Sharp\":12.499875,\"Penetrate\":0,\"PhysicalApplications\":0.87499125,\"Weight\":1.25,\"OtherHits\":9.375,\"KeyHit\":2.5,\"FatalKeyHit\":0.625,\"NumberOfProductionResources\":38,\"ProductionTime\":5}")));
		products.Add (5004,new Products(5004,"長芧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"長芧\",\"Grade\":2,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階木工/冶鐵\",\"Restraint\":\"短兵器/騎兵\",\"Category\":\"穿刺式長兵器\",\"EquipShield\":true,\"Sharp\":24.99975,\"Penetrate\":0,\"PhysicalApplications\":0.7499925,\"Weight\":2.5,\"OtherHits\":18.75,\"KeyHit\":5,\"FatalKeyHit\":1.25,\"NumberOfProductionResources\":75,\"ProductionTime\":20}")));
		products.Add (5005,new Products(5005,"長槍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"長槍\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階木工/進階冶鐵\",\"Restraint\":\"短兵器/騎兵\",\"Category\":\"穿刺式長兵器\",\"EquipShield\":false,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5006,new Products(5006,"鋼槍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼槍\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5007,new Products(5007,"弓",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"弓\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"物理/進階木工/進階冶鐡\",\"Restraint\":\" \",\"Category\":\"弓\",\"EquipShield\":false,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5008,new Products(5008,"長弓",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"長弓\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階物理/進階木工/進階冶鐡\",\"Restraint\":\" \",\"Category\":\"弓\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5009,new Products(5009,"十字弓",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"十字弓\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階物理/進階木工/進階冶鐡/機械\",\"Restraint\":\" \",\"Category\":\"弓\",\"EquipShield\":false,\"Sharp\":62.499375,\"Penetrate\":0,\"PhysicalApplications\":0.37499625, \"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5010,new Products(5010,"石斧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"石斧\",\"Grade\":1,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"長兵器\",\"Category\":\"斧\",\"EquipShield\":true,\"Sharp\":12.499875,\"Penetrate\":0,\"PhysicalApplications\":0.87499125,\"Weight\":1.25,\"OtherHits\":9.375,\"KeyHit\":2.5,\"FatalKeyHit\":0.625,\"NumberOfProductionResources\":38,\"ProductionTime\":5}")));
		products.Add (5011,new Products(5011,"斧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"斧\",\"Grade\":2,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階木工/冶鐵\",\"Restraint\":\"長兵器\",\"Category\":\"斧\",\"EquipShield\":true,\"Sharp\":24.99975,\"Penetrate\":0,\"PhysicalApplications\":0.7499925,\"Weight\":2.5,\"OtherHits\":18.75,\"KeyHit\":5,\"FatalKeyHit\":1.25,\"NumberOfProductionResources\":75,\"ProductionTime\":20}")));
		products.Add (5012,new Products(5012,"大斧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"大斧\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階木工/進階冶鐵\",\"Restraint\":\"長兵器/騎兵\",\"Category\":\"斧\",\"EquipShield\":false,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5013,new Products(5013,"鋼斧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼斧\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5014,new Products(5014,"大鋼斧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"大鋼斧\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":62.499375,\"Penetrate\":0,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5015,new Products(5015,"木劍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"木劍\",\"Grade\":1,\"Characteristics\":\"是用來捉殭屍嗎？（特殊任務用）\",\"TechnologyRequired\":\"進階木工\",\"Restraint\":\" \",\"Category\":\"劍\",\"EquipShield\":false,\"Sharp\":12.499875,\"Penetrate\":0,\"PhysicalApplications\":0.87499125,\"Weight\":1.25,\"OtherHits\":9.375,\"KeyHit\":2.5,\"FatalKeyHit\":0.625,\"NumberOfProductionResources\":38,\"ProductionTime\":5}")));
		products.Add (5016,new Products(5016,"短劍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"短劍\",\"Grade\":2,\"Characteristics\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Category\":\"劍\",\"EquipShield\":false,\"Sharp\":24.99975,\"Penetrate\":0,\"PhysicalApplications\":0.7499925,\"Weight\":2.5,\"OtherHits\":18.75,\"KeyHit\":5,\"FatalKeyHit\":1.25,\"NumberOfProductionResources\":75,\"ProductionTime\":20}")));
		products.Add (5017,new Products(5017,"長劍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"長劍\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Category\":\"劍\",\"EquipShield\":false,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5018,new Products(5018,"花劍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"花劍\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階冶鐡/高階鑄劍術\",\"Restraint\":\" \",\"Category\":\"劍\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5019,new Products(5019,"匕首",WEAPON,0,1,JSONClass.Parse ("{ \"Name\":\"匕首\",\"Grade\":1,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":12.499875,\"Penetrate\":0,\"PhysicalApplications\":0.87499125,\"Weight\":1.25,\"OtherHits\":9.375,\"KeyHit\":2.5,\"FatalKeyHit\":0.625,\"NumberOfProductionResources\":38,\"ProductionTime\":5}")));
		products.Add (5020,new Products(5020,"鋼劍",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼劍\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"劍\",\"EquipShield\":false,\"Sharp\":62.499375,\"Penetrate\":0,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5021,new Products(5021,"鋼匕首",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼匕首\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5022,new Products(5022,"鋼棒",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼棒\",\"Grade\":4,\"Characteristics\":\"小鋼棒呢家嘢呢。。。。。。\",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5023,new Products(5023,"刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"刀\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"木工/冶鐵\",\"Restraint\":\" \",\"Category\":\"刀\",\"EquipShield\":false,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5024,new Products(5024,"大關刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"大關刀\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階木工/進階冶鐵/進階鍊鋼\",\"Restraint\":\" \",\"Category\":\"砍劈式長兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5025,new Products(5025,"老牛",WEAPON,0,1,JSONClass.Parse ("{ \"Name\":\"老牛\",\"Grade\":5,\"Characteristics\":\"牛肉刀\",\"TechnologyRequired\":\"進階木工/進階鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":62.499375,\"Penetrate\":0,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5026,new Products(5026,"西瓜刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"西瓜刀\",\"Grade\":6,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階木工/進階鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":74.99925,\"Penetrate\":0,\"PhysicalApplications\":0.2499975,\"Weight\":7.5,\"OtherHits\":56.25,\"KeyHit\":15,\"FatalKeyHit\":3.75,\"NumberOfProductionResources\":225,\"ProductionTime\":180}")));
		products.Add (5027,new Products(5027,"生果刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"生果刀\",\"Grade\":7,\"Characteristics\":\"特殊任務用\",\"TechnologyRequired\":\"木工/冶鐵\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":87.499125,\"Penetrate\":0,\"PhysicalApplications\":0.12499875,\"Weight\":8.75,\"OtherHits\":65.625,\"KeyHit\":17.5,\"FatalKeyHit\":4.375,\"NumberOfProductionResources\":263,\"ProductionTime\":245}")));
		products.Add (5028,new Products(5028,"彎刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"彎刀\",\"Grade\":7,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階冶鐵/高階鍊鋼\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":true,\"Sharp\":87.499125,\"Penetrate\":0,\"PhysicalApplications\":0.12499875,\"Weight\":8.75,\"OtherHits\":65.625,\"KeyHit\":17.5,\"FatalKeyHit\":4.375,\"NumberOfProductionResources\":263,\"ProductionTime\":245}")));
		products.Add (5029,new Products(5029,"圓月彎刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"圓月彎刀\",\"Grade\":7,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階冶鐵/高階鍊鋼/高階鑄劍術\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":true,\"Sharp\":87.499125,\"Penetrate\":0,\"PhysicalApplications\":0.12499875,\"Weight\":8.75,\"OtherHits\":65.625,\"KeyHit\":17.5,\"FatalKeyHit\":4.375,\"NumberOfProductionResources\":263,\"ProductionTime\":245}")));
		products.Add (5030,new Products(5030,"飛刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"飛刀\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\" \",\"Restraint\":\" \",\"Category\":\"暗器\",\"EquipShield\":false,\"Sharp\":62.499375,\"Penetrate\":0,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5031,new Products(5031,"鋼飛刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼飛刀\",\"Grade\":6,\"Characteristics\":\" \",\"TechnologyRequired\":\" \",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":74.99925,\"Penetrate\":0,\"PhysicalApplications\":0.2499975,\"Weight\":7.5,\"OtherHits\":56.25,\"KeyHit\":15,\"FatalKeyHit\":3.75,\"NumberOfProductionResources\":225,\"ProductionTime\":180}")));
		products.Add (5032,new Products(5032,"武士刀",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"武士刀\",\"Grade\":7,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階鍊鋼/高階鑄劍術\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":87.499125,\"Penetrate\":0,\"PhysicalApplications\":0.12499875,\"Weight\":8.75,\"OtherHits\":65.625,\"KeyHit\":17.5,\"FatalKeyHit\":4.375,\"NumberOfProductionResources\":263,\"ProductionTime\":245}")));
		products.Add (5033,new Products(5033,"㦸",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"㦸\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"高階冶鐵/高階鍊鋼\",\"Restraint\":\" \",\"Category\":\"砍劈式長兵器\",\"EquipShield\":false,\"Sharp\":49.9995,\"Penetrate\":0,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5034,new Products(5034,"流星鎚",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"流星鎚\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":true,\"Sharp\":37.499625,\"Penetrate\":0,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5035,new Products(5035,"鐡鎚",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鐡鎚\",\"Grade\":4,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Category\":\"鎚\",\"EquipShield\":true,\"Sharp\":0,\"Penetrate\":49.9995,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5036,new Products(5036,"鋼鎚",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼鎚\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\" \",\"Category\":\"鎚\",\"EquipShield\":true,\"Sharp\":0,\"Penetrate\":62.499375,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
		products.Add (5037,new Products(5037,"鐧",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鐧\",\"Grade\":3,\"Characteristics\":\" \",\"TechnologyRequired\":\"進階木工/進階冶鐵\",\"Restraint\":\"短兵器\",\"Category\":\"特殊兵器\",\"EquipShield\":true,\"Sharp\":0,\"Penetrate\":37.499625,\"PhysicalApplications\":0.62499375,\"Weight\":3.75,\"OtherHits\":28.125,\"KeyHit\":7.5,\"FatalKeyHit\":1.875,\"NumberOfProductionResources\":113,\"ProductionTime\":45}")));
		products.Add (5038,new Products(5038,"皮鞭",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"皮鞭\",\"Grade\":4,\"Characteristics\":\"come on babe\",\"TechnologyRequired\":\"熟牛皮/縫紉\",\"Restraint\":\" \",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":0,\"Penetrate\":49.9995,\"PhysicalApplications\":0.499995,\"Weight\":5,\"OtherHits\":37.5,\"KeyHit\":10,\"FatalKeyHit\":2.5,\"NumberOfProductionResources\":150,\"ProductionTime\":80}")));
		products.Add (5039,new Products(5039,"鋼鞭",WEAPON,0,1,JSONClass.Parse ("{\"Name\":\"鋼鞭\",\"Grade\":5,\"Characteristics\":\" \",\"TechnologyRequired\":\"鍊鋼\",\"Restraint\":\"短兵器\",\"Category\":\"特殊兵器\",\"EquipShield\":false,\"Sharp\":0,\"Penetrate\":62.499375,\"PhysicalApplications\":0.37499625,\"Weight\":6.25,\"OtherHits\":46.875,\"KeyHit\":12.5,\"FatalKeyHit\":3.125,\"NumberOfProductionResources\":188,\"ProductionTime\":125}")));
#endregion
#region Armor
		products.Add (6001,new Products(6001,"鐡製西洋全身甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鐡製西洋全身甲\",\"Characteristic\":\"包全身/但點都露對眼出黎嘅\",\"TechnologyRequired\":\" \",\"Restraint\":\" \",\"Grade\":7,\"Hardness\":5,\"Weight\":25,\"OtherCover\":\"99%\",\"KeyCover\":\"99%\",\"FatalKeyCover\":\"99%\",\"NumberOfProductionResources\":7,\"ProductionTime\":264}")));
		products.Add (6002,new Products(6002,"鋼製西洋全身甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鋼製西洋全身甲\",\"Characteristic\":\"包全身/但點都露對眼出黎嘅\",\"TechnologyRequired\":\" \",\"Restraint\":\" \",\"Grade\":7,\"Hardness\":6,\"Weight\":30,\"OtherCover\":\"99%\",\"KeyCover\":\"99%\",\"FatalKeyCover\":\"99%\",\"NumberOfProductionResources\":10,\"ProductionTime\":336}")));
		products.Add (6003,new Products(6003,"忍者服",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"忍者服\",\"Characteristic\":\"加隱䁥/對手難偵察出\",\"TechnologyRequired\":\" \",\"Restraint\":\" \",\"Grade\":7,\"Hardness\":1,\"Weight\":5,\"OtherCover\":\"99%\",\"KeyCover\":\"99%\",\"FatalKeyCover\":\"99%\",\"NumberOfProductionResources\":10,\"ProductionTime\":336}")));
		products.Add (6004,new Products(6004,"棉甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"棉甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"縫紉\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":1,\"Weight\":10,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":1,\"ProductionTime\":24}")));
		products.Add (6005,new Products(6005,"玄甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"玄甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"熟牛皮\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":2,\"Weight\":10,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":1,\"ProductionTime\":24}")));
		products.Add (6006,new Products(6006,"皮甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"皮甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"縫紉\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":2,\"Weight\":10,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":1,\"ProductionTime\":24}")));
		products.Add (6007,new Products(6007,"白布甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"白布甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":3,\"Weight\":10,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"40%\",\"NumberOfProductionResources\":1,\"ProductionTime\":144}")));
		products.Add (6008,new Products(6008,"藤甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"藤甲\",\"Characteristic\":\"因有桐油/怕火攻\",\"TechnologyRequired\":\"木工\",\"Restraint\":\" \",\"Grade\":1,\"Hardness\":3,\"Weight\":10,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"50%\",\"NumberOfProductionResources\":3,\"ProductionTime\":192}")));
		products.Add (6009,new Products(6009,"木甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"木甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"木工\",\"Restraint\":\" \",\"Grade\":1,\"Hardness\":3,\"Weight\":30,\"OtherCover\":\"40%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"50%\",\"NumberOfProductionResources\":3,\"ProductionTime\":144}")));
		products.Add (6010,new Products(6010,"鐵片甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鐵片甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":5,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"60%\",\"NumberOfProductionResources\":3,\"ProductionTime\":144}")));
		products.Add (6011,new Products(6011,"魚鱗甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"魚鱗甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":5,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"60%\",\"NumberOfProductionResources\":5,\"ProductionTime\":216}")));
		products.Add (6012,new Products(6012,"筒袖鎧",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"筒袖鎧\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":5,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"60%\",\"NumberOfProductionResources\":5,\"ProductionTime\":216}")));
		products.Add (6013,new Products(6013,"光要甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"光要甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":5,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"70%\",\"NumberOfProductionResources\":5,\"ProductionTime\":216}")));
		products.Add (6014,new Products(6014,"細鱗甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"細鱗甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":5,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"70%\",\"NumberOfProductionResources\":5,\"ProductionTime\":264}")));
		products.Add (6015,new Products(6015,"鎖子甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鎖子甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":6,\"Hardness\":6,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"45%\",\"FatalKeyCover\":\"70%\",\"NumberOfProductionResources\":6,\"ProductionTime\":264}")));
		products.Add (6016,new Products(6016,"板鏈甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"板鏈甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":4,\"Hardness\":6,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"45%\",\"FatalKeyCover\":\"80%\",\"NumberOfProductionResources\":6,\"ProductionTime\":264}")));
		products.Add (6017,new Products(6017,"鳥錘甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鳥錘甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":6,\"Weight\":30,\"OtherCover\":\"40%\",\"KeyCover\":\"45%\",\"FatalKeyCover\":\"80%\",\"NumberOfProductionResources\":6,\"ProductionTime\":264}")));
		products.Add (6018,new Products(6018,"明光甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"明光甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":5,\"Hardness\":6,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"80%\",\"FatalKeyCover\":\"80%\",\"NumberOfProductionResources\":10,\"ProductionTime\":264}")));
		products.Add (6019,new Products(6019,"山文甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"山文甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":4,\"Hardness\":6,\"Weight\":22,\"OtherCover\":\"40%\",\"KeyCover\":\"80%\",\"FatalKeyCover\":\"90%\",\"NumberOfProductionResources\":10,\"ProductionTime\":312}")));
		products.Add (6020,new Products(6020,"歩人甲",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"歩人甲\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":6,\"Weight\":30,\"OtherCover\":\"40%\",\"KeyCover\":\"90%\",\"FatalKeyCover\":\"90%\",\"NumberOfProductionResources\":10,\"ProductionTime\":336}")));
		products.Add (6021,new Products(6021,"護心鏡",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"護心鏡\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":5,\"Weight\":2,\"OtherCover\":\"20%\",\"KeyCover\":\"50%\",\"FatalKeyCover\":\"50%\",\"NumberOfProductionResources\":5,\"ProductionTime\":24}")));
		products.Add (6022,new Products(6022,"頭形兜",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"頭形兜\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":1,\"Weight\":1,\"OtherCover\":\"25%\",\"KeyCover\":\"20%\",\"FatalKeyCover\":\"20%\",\"NumberOfProductionResources\":2,\"ProductionTime\":24}")));
		products.Add (6023,new Products(6023,"星兜",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"星兜\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":1,\"Weight\":1,\"OtherCover\":\"25%\",\"KeyCover\":\"20%\",\"FatalKeyCover\":\"20%\",\"NumberOfProductionResources\":2,\"ProductionTime\":24}")));
		products.Add (6024,new Products(6024,"士兵頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"士兵頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":2,\"Hardness\":1,\"Weight\":1,\"OtherCover\":\"25%\",\"KeyCover\":\"20%\",\"FatalKeyCover\":\"20%\",\"NumberOfProductionResources\":2,\"ProductionTime\":24}")));
		products.Add (6025,new Products(6025,"突盔形兜",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"突盔形兜\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":3,\"Weight\":3,\"OtherCover\":\"25%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"20%\",\"NumberOfProductionResources\":2,\"ProductionTime\":144}")));
		products.Add (6026,new Products(6026,"鐵甲頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鐵甲頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":3,\"Hardness\":3,\"Weight\":3,\"OtherCover\":\"25%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":4,\"ProductionTime\":144}")));
		products.Add (6027,new Products(6027,"疾風頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"疾風頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"冶鐵\",\"Restraint\":\" \",\"Grade\":4,\"Hardness\":3,\"Weight\":3,\"OtherCover\":\"30%\",\"KeyCover\":\"30%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":4,\"ProductionTime\":144}")));
		products.Add (6028,new Products(6028,"鐵鱗頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"鐵鱗頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":5,\"Hardness\":5,\"Weight\":4,\"OtherCover\":\"30%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"30%\",\"NumberOfProductionResources\":4,\"ProductionTime\":216}")));
		products.Add (6029,new Products(6029,"白銀頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"白銀頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":6,\"Hardness\":5,\"Weight\":5,\"OtherCover\":\"30%\",\"KeyCover\":\"40%\",\"FatalKeyCover\":\"40%\",\"NumberOfProductionResources\":6,\"ProductionTime\":264}")));
		products.Add (6030,new Products(6030,"彪騎頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"彪騎頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":6,\"Hardness\":5,\"Weight\":6,\"OtherCover\":\"30%\",\"KeyCover\":\"50%\",\"FatalKeyCover\":\"40%\",\"NumberOfProductionResources\":6,\"ProductionTime\":264}")));
		products.Add (6031,new Products(6031,"武軍頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"武軍頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":6,\"Hardness\":6,\"Weight\":6,\"OtherCover\":\"30%\",\"KeyCover\":\"50%\",\"FatalKeyCover\":\"50%\",\"NumberOfProductionResources\":10,\"ProductionTime\":312}")));
		products.Add (6032,new Products(6032,"天狼頭盔",ARMOR,0,1,JSONClass.Parse ("{\"Name\":\"天狼頭盔\",\"Characteristic\":\" \",\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\" \",\"Grade\":6,\"Hardness\":6,\"Weight\":6,\"OtherCover\":\"30%\",\"KeyCover\":\"50%\",\"FatalKeyCover\":\"50%\",\"NumberOfProductionResources\":10,\"ProductionTime\":312}")));
#endregion
#region Shield
		products.Add (7001,new Products(7001,"皮製小圓盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"皮製小圓盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7002,new Products(7002,"皮製圓盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"皮製圓盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7003,new Products(7003,"皮製方盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"皮製方盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7004,new Products(7004,"木製小圓盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"木製小圓盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7005,new Products(7005,"銅鐵盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"銅鐵盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7006,new Products(7006,"藤盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"藤盾\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7007,new Products(7007,"燕尾牌",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"燕尾牌\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"木工/冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7008,new Products(7008,"羅馬大盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"羅馬大盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"木工/進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7009,new Products(7009,"少林盾牌",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"少林盾牌\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7010,new Products(7010,"仁王盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"仁王盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工/進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7011,new Products(7011,"大木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"大木盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7012,new Products(7012,"厚木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"厚木盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7013,new Products(7013,"桐木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"桐木盾\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7014,new Products(7014,"白楊盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"白楊盾\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":11,\"ProductionTime\":6}")));
		products.Add (7015,new Products(7015,"白樺盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"白樺盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":11,\"ProductionTime\":24}")));
		products.Add (7016,new Products(7016,"檀木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"檀木盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"高階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7017,new Products(7017,"青銅盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"青銅盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工/冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7018,new Products(7018,"銅盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"銅盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工/冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7019,new Products(7019,"古銅盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"古銅盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工/冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7020,new Products(7020,"長立盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"長立盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工/冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7021,new Products(7021,"重盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"重盾\",\"Characteristics\":\" \",\"Grade\":4,\"TechnologyRequired\":\"進階木工/進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":99,\"Weight\":9,\"NumberOfProductionResources\":20,\"ProductionTime\":36}")));
		products.Add (7022,new Products(7022,"方盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"方盾\",\"Characteristics\":\" \",\"Grade\":4,\"TechnologyRequired\":\"進階木工/進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":99,\"Weight\":9,\"NumberOfProductionResources\":20,\"ProductionTime\":36}")));
		products.Add (7023,new Products(7023,"手盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"手盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7024,new Products(7024,"護身盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"護身盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7025,new Products(7025,"白紋盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"白紋盾\",\"Characteristics\":\" \",\"Grade\":1,\"TechnologyRequired\":\"木工\",\"Restraint\":\"弓/弩\",\"Hardness\":20,\"Weight\":2,\"NumberOfProductionResources\":2,\"ProductionTime\":1}")));
		products.Add (7026,new Products(7026,"護木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"護木盾\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7027,new Products(7027,"桃木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"桃木盾\",\"Characteristics\":\" \",\"Grade\":2,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":50,\"Weight\":5,\"NumberOfProductionResources\":5,\"ProductionTime\":6}")));
		products.Add (7028,new Products(7028,"古木盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"古木盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"進階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7029,new Products(7029,"雁盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"雁盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"高階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":99,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7030,new Products(7030,"鳩盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"鳩盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"高階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7031,new Products(7031,"雙圓盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"雙圓盾\",\"Characteristics\":\" \",\"Grade\":3,\"TechnologyRequired\":\"高階木工\",\"Restraint\":\"弓/弩\",\"Hardness\":80,\"Weight\":8,\"NumberOfProductionResources\":15,\"ProductionTime\":24}")));
		products.Add (7032,new Products(7032,"銅甲盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"銅甲盾\",\"Characteristics\":\" \",\"Grade\":4,\"TechnologyRequired\":\"高階木工/進階冶鐵\",\"Restraint\":\"弓/弩\",\"Hardness\":99,\"Weight\":9,\"NumberOfProductionResources\":20,\"ProductionTime\":36}")));
		products.Add (7033,new Products(7033,"鐵甲盾",SHIELD,0,1,JSONClass.Parse ("{\"Name\":\"鐵甲盾\",\"Characteristics\":\" \",\"Grade\":4,\"TechnologyRequired\":\"高階木工/高階冶鐡\",\"Restraint\":\"弓/弩\",\"Hardness\":99,\"Weight\":9,\"NumberOfProductionResources\":20,\"ProductionTime\":36}")));
#endregion
>>>>>>> feature/MainMenu-shawn
	}

	public Products GetProductById(int i){
		var cnt = count;
		int match = -1;
		for ( int j = 0 ; j < cnt ; j++){
			if (products[j].id == i){
				match = j;
				break;
			}
		}
		if (match != -1) {
			return products [match];
		} else {
			return null;
		}
	}
}
