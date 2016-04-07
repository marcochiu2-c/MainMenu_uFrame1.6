using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class TechTreeObject {
	public int id { get; set; }
	public string Name { get; set; }
	public double Important { get; set; }
	public int TotalIQ { get; set; }
	public int Lv1IQ { get; set; }
	public int Lv2IQ { get; set; }
	public int Lv3IQ { get; set; }
	public int Lv4IQ { get; set; }
	public int Lv5IQ { get; set; }
	public int Lv6IQ { get; set; }
	public int Lv7IQ { get; set; }
	public int Lv8IQ { get; set; }
	public int Lv9IQ { get; set; }
	public int Lv10IQ { get; set; }
	public int TechRequirement { get; set; }
	public int KnowledgeRequirement { get; set; }

	public TechTreeObject (int cnt, string n, double i,int ti, int iq1, int iq2, int iq3, int iq4,
	                       int iq5, int iq6, int iq7, int iq8, int iq9, int iq10,
	                       int tr, int kr){
		id = cnt;
		Name = n;
		Important = i;
		TotalIQ = ti;
		Lv1IQ = iq1;
		Lv2IQ = iq2;
		Lv3IQ = iq3;
		Lv4IQ = iq4;
		Lv5IQ = iq5;
		Lv6IQ = iq6;
		Lv7IQ = iq7;
		Lv8IQ = iq8;
		Lv9IQ = iq9;
		Lv10IQ = iq10;
		TechRequirement = tr;
		KnowledgeRequirement = kr;
	}

	public TechTreeObject (SimpleJSON.JSONClass j, int cnt =0){
		id = cnt;
		Name = j ["Name"];
		Important = j["Important"].AsDouble;
		TotalIQ = j["TotalIQ"].AsInt;
		Lv1IQ = j["Lv1IQ"].AsInt;
		Lv2IQ = j["Lv2IQ"].AsInt;
		Lv3IQ = j["Lv3IQ"].AsInt;
		Lv4IQ = j["Lv4IQ"].AsInt;
		Lv5IQ = j["Lv5IQ"].AsInt;
		Lv6IQ = j["Lv6IQ"].AsInt;
		Lv7IQ = j["Lv7IQ"].AsInt;
		Lv8IQ = j["Lv8IQ"].AsInt;
		Lv9IQ = j["Lv9IQ"].AsInt;
		Lv10IQ = j["Lv10IQ"].AsInt;
		TechRequirement = j["TechRequirement"].AsInt;
		KnowledgeRequirement = j["KnowledgeRequirement"].AsInt;
	}

	public string toString (){
		JSONClass j = new JSONClass ();
		j.Add ("Id",new JSONData (id));
		j ["Name"] = Name;
		j.Add ("Important",new JSONData (Important));
		j.Add ("TotalIQ", new JSONData(TotalIQ));
		j.Add ("Lv1IQ", new JSONData(Lv1IQ));
		j.Add ("Lv2IQ", new JSONData(Lv2IQ));
		j.Add ("Lv3IQ", new JSONData(Lv3IQ));
		j.Add ("Lv4IQ", new JSONData(Lv4IQ));
		j.Add ("Lv5IQ", new JSONData(Lv5IQ));
		j.Add ("Lv6IQ", new JSONData(Lv6IQ));
		j.Add ("Lv7IQ", new JSONData(Lv7IQ));
		j.Add ("Lv8IQ", new JSONData(Lv8IQ));
		j.Add ("Lv9IQ", new JSONData(Lv9IQ));
		j.Add ("Lv10IQ", new JSONData (Lv10IQ));
		j.Add ("TechRequirement", new JSONData (TechRequirement));
		j.Add ("KnowledgeRequirement", new JSONData (KnowledgeRequirement));
		return j.ToString ();
	}

	public JSONNode ToJSON (){
		JSONClass j = new JSONClass ();
		j.Add ("Id",new JSONData (id));
		j ["Name"] = Name;
		j.Add ("Important",new JSONData (Important));
		j.Add ("TotalIQ", new JSONData(TotalIQ));
		j.Add ("Lv1IQ", new JSONData(Lv1IQ));
		j.Add ("Lv2IQ", new JSONData(Lv2IQ));
		j.Add ("Lv3IQ", new JSONData(Lv3IQ));
		j.Add ("Lv4IQ", new JSONData(Lv4IQ));
		j.Add ("Lv5IQ", new JSONData(Lv5IQ));
		j.Add ("Lv6IQ", new JSONData(Lv6IQ));
		j.Add ("Lv7IQ", new JSONData(Lv7IQ));
		j.Add ("Lv8IQ", new JSONData(Lv8IQ));
		j.Add ("Lv9IQ", new JSONData(Lv9IQ));
		j.Add ("Lv10IQ", new JSONData (Lv10IQ));
		j.Add ("TechRequirement", new JSONData (TechRequirement));
		j.Add ("KnowledgeRequirement", new JSONData (KnowledgeRequirement));
		return j;
	}

	public static List<TechTreeObject> GetList(int countStartNumber = 1){
		int counter = countStartNumber;
		TextAsset t = Resources.Load<TextAsset> ("techtree");
		List<TechTreeObject> tList = new List<TechTreeObject> ();
		SimpleJSON.JSONArray techtree = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (t.text);
		//Debug.Log (counselor);
		Resources.UnloadAsset (t);
		IEnumerator tr = techtree.GetEnumerator ();
		while (tr.MoveNext ()) {
			tList.Add (new TechTreeObject((SimpleJSON.JSONClass) tr.Current, counter++));
		}
		return tList;
	}
}
