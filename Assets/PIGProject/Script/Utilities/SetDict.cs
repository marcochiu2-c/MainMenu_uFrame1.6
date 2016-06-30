using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utilities{
	public class SetDict
	{

		public static Dictionary<int,string> Knowledge(){
			Dictionary<int,string> dict = new Dictionary<int,string> ();
			dict.Add (0, "");
			dict.Add (1, "智商");
			dict.Add (2, "統率");
			dict.Add (2001, "木工");
			dict.Add (2002,"冶鐵");
			dict.Add (2003,"東亞歷史");
			dict.Add (2004,"西方歷史");
			dict.Add (2005,"鍊鋼");
			dict.Add (2006,"金屬加工");
			dict.Add (2007,"工藝");
			dict.Add (2008,"幾何");
			dict.Add (2009,"物理");
			dict.Add (2010,"化學");
			dict.Add (2011,"元素表");
			dict.Add (2012,"滑輪");
			dict.Add (2013,"解剖學");
			dict.Add (2014,"彈射");
			dict.Add (2015, "火藥調製");
			dict.Add (2016, "心理學");
			dict.Add (2017, "易學");
			return dict;
		}

		public static Dictionary<int,string> KnowledgeID(){
			Dictionary<int,string> dict = new Dictionary<int,string> ();
			dict.Add (0, "");
			dict.Add (1, "IQ");
			dict.Add (2, "Commanded");
			dict.Add (2001, "Woodworker");
			dict.Add (2002,"MetalFabrication");
			dict.Add (2003, "EasternHistory");
			dict.Add (2004, "WesternHistory");
			dict.Add (2005,"ChainSteel");
			dict.Add (2006,"MetalProcessing");
			dict.Add (2007,"Crafts");
			dict.Add (2008,"Geometry");
			dict.Add (2009,"Physics");
			dict.Add (2010,"Chemistry");
			dict.Add (2011,"PeriodicTable");
			dict.Add (2012,"Pulley");
			dict.Add (2013,"Anatomy");
			dict.Add (2014,"Catapult");
			dict.Add (2015,"GunpowderModulation");
			dict.Add (2016,"Psychology");
			dict.Add (2017, "IChing");
			return dict;
		}
	}
}

