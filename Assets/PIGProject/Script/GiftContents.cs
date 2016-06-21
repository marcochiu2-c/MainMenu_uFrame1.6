using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class GiftContents : MonoBehaviour{
	public static List<GiftContents> gcList = new List<GiftContents> ();
	public int ID { get; set; }
	public string Name { get; set; }
	public Dictionary<string, int> Content { get; set; }
	public List<int> Target { get; set; }
	public DateTime Expiry { get; set; }

	public GiftContents(){
		Content = new Dictionary<string, int> ();
		Target = new List<int> ();
	}

	public GiftContents(int id, string name, Dictionary<string, int> content, List<int> target, DateTime expiry){
		ID = id;
		Name = name;
		Content = content;
		Target = target;
		Expiry = expiry;
	}

	public GiftContents(JSONNode j){
		this.Content = new Dictionary<string, int> ();
		this.Target = new List<int> ();
		ID = j ["gift_id"].AsInt;
		Name = j ["gift_name"];
		if (j["gift_json"]["stardusts"] != null){
			Content.Add ("stardusts",j["gift_json"]["stardusts"].AsInt);
		}
		if (j["gift_json"]["resources"] != null){
			Content.Add ("resources",j["gift_json"]["resources"].AsInt);
		}
		if (j["gift_json"]["feathers"] != null){
			Content.Add ("feathers",j["gift_json"]["feathers"].AsInt);
		}
		for (int i = 0; i < j["gift_target"].Count; i++) {
			Target.Add (j["gift_target"][i].AsInt);
		}
		Expiry = DateTime.Parse (j ["expiry"] );
	}
#if UNITY_EDITOR
	public JSONClass ToJSON(){
		JSONClass j = new JSONClass ();
		j ["gift_id"].AsInt = ID;
		j ["gift_name"] = Name;
		j ["gift_json"] ["stardusts"].AsInt = this.Content ["stardusts"];
		j ["gift_json"] ["resources"].AsInt = this.Content ["resources"];
		j ["gift_json"] ["feathers"].AsInt = this.Content ["feathers"];
		for (int i = 0; i < this.Target.Count; i++) {
			j["gift_target"][-1].AsInt = Target[i];
		}
		j ["expiry"] = Expiry.ToString ();
		return j;
	}
#endif
}

