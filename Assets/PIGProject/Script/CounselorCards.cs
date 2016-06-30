using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class CounselorCards{
	public int id { get; set; }
	public string Name { get; set; }
	public int Rank { get; set; }
	public string Era { get; set; }
	public int Type { get; set; }
	public int InitialIQ { get; set; }
	public double GainIQ { get; set; }
	public int HighestIQ { get; set; }
	public int InitialLeadership { get; set; }
	public int GainLv30Leadership { get; set; }
	public int GainLv60Leadership { get; set; }
	public int GainLv99Leadership { get; set; }
	public int HighestLeadership { get; set; }
	public int InitialPrestige { get; set; }
	public JSONClass KnownFormation { get; set; }
	public JSONClass KnownKnowledge { get; set; }

	public CounselorCards(string n, int r, string e, int t,int iIQ, 
	                int gIQ, int hIQ, int iLeadership, int lLv30, int lLv60, int lLv99,
	                int hLeadership, int iPrestige, JSONClass kFormation,
	                JSONClass kKnowledge, int cnt = 0){
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
		KnownFormation = kFormation;
		KnownKnowledge = kKnowledge;
	}

	public CounselorCards (SimpleJSON.JSONClass j, int cnt =0){
		id = cnt;
		Name = j ["Name"];
		Rank = j["Rank"].AsInt;
		Era = j["Era"];
		Type = j["Type"].AsInt;
		InitialIQ = j["IQ"].AsInt;
		GainIQ = j["GainIQ"].AsDouble;
		HighestIQ = j["HighestIQ"].AsInt;
		InitialLeadership = j["Leadership"].AsInt;
		GainLv30Leadership = j["GainLv30Leadership"].AsInt;
		GainLv60Leadership = j["GainLv60Leadership"].AsInt;
		GainLv99Leadership = j["GainLv99Leadership"].AsInt;
		HighestLeadership = j["HighestLeadership"].AsInt;
		InitialPrestige = j["Prestige"].AsInt;
		if (j ["KnownFormation"].ToString().Trim() != "") {
			KnownFormation = (JSONClass)j ["KnownFormation"];
		} else {
			KnownFormation = (JSONClass) JSON.Parse("{ }");
		}
		KnownKnowledge = (JSONClass)j["KnownKnowledge"];
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
		j.Add ("KnownFormation", KnownFormation);
		j.Add ("KnownKnowledge", KnownKnowledge);
		return j.ToString ();
	}

	public JSONClass ToJSON (){

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
		j.Add ("KnownFormation", KnownFormation);
		j.Add ("KnownKnowledge", KnownKnowledge);
		return j;
	}

	public static List<CounselorCards> GetList(int countStartNumber = 1){
		int counter = countStartNumber;
		TextAsset t = Resources.Load<TextAsset> ("counselors");
		List<CounselorCards> cList = new List<CounselorCards> ();
		SimpleJSON.JSONArray counselor = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (t.text);
		//Debug.Log (counselor);
		Resources.UnloadAsset (t);
		IEnumerator c = counselor.GetEnumerator ();
		while (c.MoveNext ()) {
			cList.Add (new CounselorCards((SimpleJSON.JSONClass) c.Current, counter++));
		}
		return cList;
	}
}