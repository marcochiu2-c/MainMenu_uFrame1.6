using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Products{
	public int id; // type id in database
	public string name;
	public int type; // 0: weapon, 1: armor, 2: shield
	public int price;
	public int currencyType;  // 0:時之星塵 1:資源 2:銀羽
	public string attributes;

	public Products(int i, string n, int t, int p, int c, string a=""){
		id = i;
		name = n;
		type = t;
		price = p;
		currencyType = c;
		attributes = a;
	}
}

[Serializable]
public class ProductList{
	public List<Products> products;
	const int WEAPON = 0;
	const int ARMOR  = 1;
	const int SHIELD = 2;
	const int BUILDING = 3;

	public int count { get {
			return products.Count;
		} }

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
