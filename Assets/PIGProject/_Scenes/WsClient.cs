using UnityEngine;
using System;
using System.Collections;
using System.Text;
using WebSocketSharp;


public class WsClient {
	string server = "";
	public WebSocket conn;
	string table = "";
	string result = "";
	// Use this for initialization

	private static readonly WsClient s_Instance = new WsClient();

	public static WsClient Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public WsClient (String s="wss://192.168.100.64:8000/") {
		server = s;
		using (conn = new WebSocket (server,"game"));
		conn.Connect ();

		conn.OnOpen += (sender, e) => conn.Send ("Server Connected");

		conn.OnError += (sender, e) => {
			conn.Close();
		};

		conn.OnClose += (sender, e) => {
			conn.Close();
		};



		conn.SslConfiguration.ServerCertificateValidationCallback =
			(sender, certificate, chain, sslPolicyErrors) => {
			// Do something to validate the server certificate.
			Debug.Log ("Cert: " + certificate);

			return true; // If the server certificate is valid.
		};



	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void Send(String json){
		Debug.Log ("Sending Command");
		conn.Send (json);
	}

	public String UnixTimestampToDateTime(long timestamp,int tz)
	{
		DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
		return unixRef.AddSeconds(timestamp+tz*60*60).ToString();
	}

	public long UnixTimeNow(DateTime dt)
	{
		var timeSpan = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalSeconds;
	}
}
