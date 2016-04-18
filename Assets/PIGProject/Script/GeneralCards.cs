using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;



public class GeneralCards
{
	public int id { get; set; }
	public string Name { get; set; }
	public int Rank { get; set; }
	public string Era { get; set; }
	public int Type { get; set; }
	public float InitialIQ { get; set; }
	public float GainIQ { get; set; }
	public float HighestIQ { get; set; }
	public int InitialLeadership { get; set; }
	public int GainLv30Leadership { get; set; }
	public int GainLv60Leadership { get; set; }
	public int GainLv99Leadership { get; set; }
	public int HighestLeadership { get; set; }
	public float InitialPrestige { get; set; }
	public float InitialCourage { get; set; }
	public float GainCourage { get; set; }
	public float HighestCourage { get; set; }
	public float InitialForce { get; set; }
	public float GainForce { get; set; }
	public float HighestForce { get; set; }
	public float InitialPhysical { get; set; }
	public float GainPhysical { get; set; }
	public float HighestPhysical { get; set; }
	public float Obedience { get; set; }
	
	public GeneralCards(string n, int r, string e, int t,float iIQ, 
	                    float gIQ, float hIQ, int iLeadership, int lLv30, int lLv60, int lLv99,
	                    int hLeadership, float iPrestige, float iCourage,
	                    float gCourage, float hCourage, float iForce, float gForce, float hForce, 
	                    float iPhysical, float gPhysical, float hPhysical, float o, int cnt = 1){
		id = cnt;
		Name = n;
		Rank = r;
		Era = e;
		Type = t;
		InitialIQ = iIQ;
		GainIQ = gIQ;
		HighestIQ = hIQ;
		InitialLeadership = iLeadership;
		GainLv30Leadership = lLv30;
		GainLv60Leadership = lLv60;
		GainLv99Leadership = lLv99;
		HighestLeadership = hLeadership;
		InitialPrestige = iPrestige;
		InitialCourage = iCourage;
		GainCourage = gCourage;
		HighestCourage = hCourage;
		InitialForce = iForce;
		GainForce = gForce;
		HighestForce = hForce;
		InitialPhysical = iPhysical;
		GainPhysical = gPhysical;
		HighestPhysical = hPhysical;
		Obedience = o;
	}

	public GeneralCards (SimpleJSON.JSONClass j, int cnt=1){
		id = cnt;
		Name = j ["Name"];
		Rank = j["Rank"].AsInt;
		Era = j["Era"];
		Type = j["Type"].AsInt;
		InitialIQ = j["IQ"].AsFloat;
		GainIQ = j["GainIQ"].AsFloat;
		HighestIQ = j["HighestIQ"].AsFloat;
		InitialLeadership = j["Leadership"].AsInt;
		GainLv30Leadership = j["GainLv30Leadership"].AsInt;
		GainLv60Leadership = j["GainLv60Leadership"].AsInt;
		GainLv99Leadership = j["GainLv99Leadership"].AsInt;
		HighestLeadership = j["HighestLeadership"].AsInt;
		InitialPrestige = j["Prestige"].AsInt;
		InitialCourage = j["Courage"].AsFloat;
		GainCourage = j["GainCourage"].AsFloat;
		HighestCourage = j["HighestCourage"].AsFloat;
		InitialForce = j["Force"].AsFloat;
		GainForce = j["GainForce"].AsFloat;
		HighestForce = j["HighestForce"].AsInt;
		InitialPhysical = j["Physical"].AsFloat;
		GainPhysical = j["GainPhysical"].AsFloat;
		HighestPhysical = j["HighestPhysical"].AsFloat;
		Obedience = j["Obedience"].AsFloat;
	}

	public string ToString (){
		JSONClass j = new JSONClass ();
		j.Add ("Id",new JSONData (id));
		j ["Name"] = Name;
		j.Add ("Rank",new JSONData (Rank));
		j.Add ("Era", new JSONData(Era));
		j.Add ("Type", new JSONData(Type));
		j.Add ("IQ", new JSONData(InitialIQ));
		j.Add ("GainIQ", new JSONData(GainIQ));
		j.Add ("HighestIQ", new JSONData(HighestIQ));
		j.Add ("Leadership", new JSONData(InitialLeadership));
		j.Add ("GainLv30Leadership", new JSONData(GainLv30Leadership));
		j.Add ("GainLv60Leadership", new JSONData(GainLv60Leadership));
		j.Add ("GainLv99Leadership", new JSONData(GainLv99Leadership));
		j.Add ("HighestLeadership", new JSONData (HighestLeadership));
		j.Add ("Prestige", new JSONData (InitialPrestige));
		j.Add ("Courage", new JSONData (InitialCourage));
		j.Add ("GainCourage", new JSONData (GainCourage));
		j.Add ("HighestCourage", new JSONData (HighestCourage));
		j.Add ("Force", new JSONData (InitialForce));
		j.Add ("GainForce", new JSONData (GainForce));
		j.Add ("HighestForce", new JSONData (HighestForce));
		j.Add ("Physical", new JSONData (InitialPhysical));
		j.Add ("GainPhysical", new JSONData (GainPhysical));
		j.Add ("HighestPhysical", new JSONData (HighestPhysical));
		j.Add ("Obedience", new JSONData (Obedience));	
		return j.ToString ();
	}

	public JSONNode ToJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("Id",new JSONData (id));
		j ["Name"] = Name;
		j.Add ("Rank",new JSONData (Rank));
		j.Add ("Era", new JSONData(Era));
		j.Add ("Type", new JSONData(Type));
		j.Add ("IQ", new JSONData(InitialIQ));
		j.Add ("GainIQ", new JSONData(GainIQ));
		j.Add ("HighestIQ", new JSONData(HighestIQ));
		j.Add ("Leadership", new JSONData(InitialLeadership));
		j.Add ("GainLv30Leadership", new JSONData(GainLv30Leadership));
		j.Add ("GainLv60Leadership", new JSONData(GainLv60Leadership));
		j.Add ("GainLv99Leadership", new JSONData(GainLv99Leadership));
		j.Add ("HighestLeadership", new JSONData (HighestLeadership));
		j.Add ("Prestige", new JSONData (InitialPrestige));
		j.Add ("Courage", new JSONData (InitialCourage));
		j.Add ("GainCourage", new JSONData (GainCourage));
		j.Add ("HighestCourage", new JSONData (HighestCourage));
		j.Add ("Force", new JSONData (InitialForce));
		j.Add ("GainForce", new JSONData (GainForce));
		j.Add ("HighestForce", new JSONData (HighestForce));
		j.Add ("Physical", new JSONData (InitialPhysical));
		j.Add ("GainPhysical", new JSONData (GainPhysical));
		j.Add ("HighestPhysical", new JSONData (HighestPhysical));
		j.Add ("Obedience", new JSONData (Obedience));	
		return j;
	}

	public static List<GeneralCards> GetList(int countStartNumber = 1){
		int counter = countStartNumber;
		TextAsset t = Resources.Load<TextAsset> ("generals");
		List<GeneralCards> gList = new List<GeneralCards> ();
		SimpleJSON.JSONArray general = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (t.text);
		//Debug.Log (general);
		Resources.UnloadAsset (t);
		IEnumerator g = general.GetEnumerator ();
		while (g.MoveNext ()) {
			gList.Add (new GeneralCards((SimpleJSON.JSONClass) g.Current, counter++));
		}
		return gList;
	}
}