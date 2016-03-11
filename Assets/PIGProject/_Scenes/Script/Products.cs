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

	public ProductList(){
		products = new List<Products> ();
		products.Add (new Products(0,"木棒",WEAPON,0,1,""));
		products.Add (new Products(1,"木棍",WEAPON,0,1,""));
		products.Add (new Products(2,"石芧",WEAPON,0,1,""));
		products.Add (new Products(3,"長芧",WEAPON,0,1,""));
		products.Add (new Products(4,"長槍",WEAPON,0,1,""));
		products.Add (new Products(5,"鋼槍",WEAPON,0,1,""));
		products.Add (new Products(6,"弓",WEAPON,0,1,""));
		products.Add (new Products(7,"長弓",WEAPON,0,1,""));
		products.Add (new Products(8,"十字弓",WEAPON,0,1,""));
		products.Add (new Products(9,"石斧",WEAPON,0,1,""));
		products.Add (new Products(10,"斧",WEAPON,0,1,""));
		products.Add (new Products(11,"大斧",WEAPON,0,1,""));
		products.Add (new Products(12,"鋼斧",WEAPON,0,1,""));
		products.Add (new Products(13,"大鋼斧",WEAPON,0,1,""));
		products.Add (new Products(14,"木劍",WEAPON,0,1,""));
		products.Add (new Products(15,"短劍",WEAPON,0,1,""));
		products.Add (new Products(16,"長劍",WEAPON,0,1,""));
		products.Add (new Products(17,"花劍",WEAPON,0,1,""));
		products.Add (new Products(18,"匕首",WEAPON,0,1,""));
		products.Add (new Products(19,"鋼劍",WEAPON,0,1,""));
		products.Add (new Products(20,"鋼匕首",WEAPON,0,1,""));
		products.Add (new Products(21,"刀",WEAPON,0,1,""));
		products.Add (new Products(22,"大關刀",WEAPON,0,1,""));
		products.Add (new Products(23,"老牛",WEAPON,0,1,""));
		products.Add (new Products(24,"西瓜刀",WEAPON,0,1,""));
		products.Add (new Products(25,"生果刀",WEAPON,0,1,""));
		products.Add (new Products(26,"彎刀",WEAPON,0,1,""));
		products.Add (new Products(27,"圓月彎刀",WEAPON,0,1,""));
		products.Add (new Products(28,"飛刀",WEAPON,0,1,""));
		products.Add (new Products(29,"鋼飛刀",WEAPON,0,1,""));
		products.Add (new Products(30,"武士刀",WEAPON,0,1,""));
		products.Add (new Products(31,"㦸",WEAPON,0,1,""));
		products.Add (new Products(32,"流星鎚",WEAPON,0,1,""));
		products.Add (new Products(33,"鐡鎚",WEAPON,0,1,""));
		products.Add (new Products(34,"鋼鎚",WEAPON,0,1,""));
		products.Add (new Products(35,"鐧",WEAPON,0,1,""));
		products.Add (new Products(36,"皮鞭",WEAPON,0,1,""));
		products.Add (new Products(37,"鋼鞭",WEAPON,0,1,""));
		products.Add (new Products(101,"鐡製西洋全身甲",ARMOR,0,1,""));
		products.Add (new Products(102,"鋼製西洋全身甲",ARMOR,0,1,""));
		products.Add (new Products(103,"忍者服",ARMOR,0,1,""));
		products.Add (new Products(104,"棉甲",ARMOR,0,1,""));
		products.Add (new Products(105,"玄甲",ARMOR,0,1,""));
		products.Add (new Products(106,"皮甲",ARMOR,0,1,""));
		products.Add (new Products(107,"白布甲",ARMOR,0,1,""));
		products.Add (new Products(108,"藤甲",ARMOR,0,1,""));
		products.Add (new Products(109,"木甲",ARMOR,0,1,""));
		products.Add (new Products(110,"鐵片甲",ARMOR,0,1,""));
		products.Add (new Products(111,"魚鱗甲",ARMOR,0,1,""));
		products.Add (new Products(112,"筒袖鎧",ARMOR,0,1,""));
		products.Add (new Products(113,"光要甲",ARMOR,0,1,""));
		products.Add (new Products(114,"細鱗甲",ARMOR,0,1,""));
		products.Add (new Products(115,"鎖子甲",ARMOR,0,1,""));
		products.Add (new Products(116,"板鏈甲",ARMOR,0,1,""));
		products.Add (new Products(117,"鳥錘甲",ARMOR,0,1,""));
		products.Add (new Products(118,"明光甲",ARMOR,0,1,""));
		products.Add (new Products(119,"山文甲",ARMOR,0,1,""));
		products.Add (new Products(120,"歩人甲",ARMOR,0,1,""));
		products.Add (new Products(121,"護心鏡",ARMOR,0,1,""));
		products.Add (new Products(122,"頭形兜",ARMOR,0,1,""));
		products.Add (new Products(123,"星兜",ARMOR,0,1,""));
		products.Add (new Products(124,"突盔形兜",ARMOR,0,1,""));
		products.Add (new Products(125,"鐵甲頭盔",ARMOR,0,1,""));
		products.Add (new Products(126,"疾風頭盔",ARMOR,0,1,""));
		products.Add (new Products(127,"鐵鱗頭盔",ARMOR,0,1,""));
		products.Add (new Products(128,"白銀頭盔",ARMOR,0,1,""));
		products.Add (new Products(129,"白銀頭盔",ARMOR,0,1,""));
		products.Add (new Products(130,"彪騎頭盔",ARMOR,0,1,""));
		products.Add (new Products(131,"武軍頭盔",ARMOR,0,1,""));
		products.Add (new Products(132,"天狼頭盔",ARMOR,0,1,""));
		products.Add (new Products(201,"皮製小圓盾",SHIELD,0,1,""));
		products.Add (new Products(202,"皮製圓盾",SHIELD,0,1,""));
		products.Add (new Products(203,"皮製方盾",SHIELD,0,1,""));
		products.Add (new Products(204,"木製小圓盾",SHIELD,0,1,""));
		products.Add (new Products(205,"銅鐵盾",SHIELD,0,1,""));
		products.Add (new Products(206,"藤盾",SHIELD,0,1,""));
		products.Add (new Products(207,"燕尾牌",SHIELD,0,1,""));
		products.Add (new Products(208,"羅馬大盾",SHIELD,0,1,""));
		products.Add (new Products(209,"少林盾牌",SHIELD,0,1,""));
		products.Add (new Products(210,"仁王盾",SHIELD,0,1,""));
		products.Add (new Products(211,"大木盾",SHIELD,0,1,""));
		products.Add (new Products(212,"厚木盾",SHIELD,0,1,""));
		products.Add (new Products(213,"桐木盾",SHIELD,0,1,""));
		products.Add (new Products(214,"白楊盾",SHIELD,0,1,""));
		products.Add (new Products(215,"白樺盾",SHIELD,0,1,""));
		products.Add (new Products(216,"檀木盾",SHIELD,0,1,""));
		products.Add (new Products(217,"青銅盾",SHIELD,0,1,""));
		products.Add (new Products(218,"銅盾",SHIELD,0,1,""));
		products.Add (new Products(219,"古銅盾",SHIELD,0,1,""));
		products.Add (new Products(220,"長立盾",SHIELD,0,1,""));
		products.Add (new Products(221,"重盾",SHIELD,0,1,""));
		products.Add (new Products(222,"方盾",SHIELD,0,1,""));
		products.Add (new Products(223,"手盾",SHIELD,0,1,""));
		products.Add (new Products(224,"護身盾",SHIELD,0,1,""));
		products.Add (new Products(225,"白紋盾",SHIELD,0,1,""));
		products.Add (new Products(226,"護木盾",SHIELD,0,1,""));
		products.Add (new Products(227,"桃木盾",SHIELD,0,1,""));
		products.Add (new Products(228,"古木盾",SHIELD,0,1,""));
		products.Add (new Products(229,"雁盾",SHIELD,0,1,""));
		products.Add (new Products(230,"鳩盾",SHIELD,0,1,""));
		products.Add (new Products(231,"雙圓盾",SHIELD,0,1,""));
		products.Add (new Products(232,"銅甲盾",SHIELD,0,1,""));
		products.Add (new Products(233,"鐵甲盾",SHIELD,0,1,""));
	}
}
