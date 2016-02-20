using UnityEngine;
using System;
using System.Collections;
using System.Text;
using WebSocketSharp;
using System.Net.Sockets;
using System.Net;


public class WsClient {
	string server = "";
	public WebSocket conn;
	string table = "";
	string result = "";
	private string ip = "192.168.100.64";
	private int port = 8000; 
	// Use this for initialization

	private static readonly WsClient s_Instance = new WsClient();

	public static WsClient Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public bool healthCheck(){
		return true;
	}

	public WsClient () {

		bool success = TestConnection();
		if (success) {
			Debug.Log ("Server check ok");
			string server = String.Format("wss://{0}:{1}/",  ip,  port );
			using (conn = new WebSocket (server,"game"));
			conn.Connect ();

			conn.OnOpen += (sender, e) => conn.Send ("Server Connected");

			conn.OnError += (sender, e) => {
				Debug.Log("Websocket Error");
				conn.Close ();
			};

			conn.OnClose += (sender, e) => {
				Debug.Log("Websocket Close");
				conn.Close ();
			};
		} else {
			Debug.Log ("Server check failed");
			return;
		}

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

	private bool TestConnection(){
		TcpClient client = new TcpClient(); 
		bool result = false;
		try
		{
			client.BeginConnect(ip, port, null, null).AsyncWaitHandle.WaitOne(3000); 
			result = client.Connected;
		}


		catch { }
		finally {
			client.Close ();
		}
		return result;
	}

	public void Send(String json){
		Debug.Log ("Sending Command");
		conn.Send (json);
	}

	public void OnConnectionFailed(){
		Debug.Log( "Connection lost");
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
