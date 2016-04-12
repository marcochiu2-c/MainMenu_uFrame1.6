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
			dict.Add (2003,"鍊鋼");
			dict.Add (2004,"金屬加工");
			dict.Add (2005,"工藝");
			dict.Add (2006,"幾何");
			dict.Add (2007,"物理");
			dict.Add (2008,"化學");
			dict.Add (2009,"元素表");
			dict.Add (2010,"滑輪");
			dict.Add (2011,"解剖學");
			dict.Add (2012,"彈射");
			dict.Add (2013,"火藥調製");
			dict.Add (2014,"心理學");


			return dict;
		}
	}
}

