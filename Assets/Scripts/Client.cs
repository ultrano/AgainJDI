using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum Protocol
{
	FoundMatch = 4, // none
	MoveToDirection = 5, // vec2[pos], float[radian]
	StopMovement = 6, // vec2[pos]
	FireToDirection = 7, // vec2[pos], float[radian]
	StartDash = 8, // none
}

public struct DataMoveToDirection
{
	public Vector2 position;
	public float radian;
}

public struct DataStopMovement
{
	public Vector2 position;
}

public struct DataFireToDirection
{
	public Vector2 position;
	public float radian;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct Packet
{
	public const int HeadSize = sizeof(Protocol);
	public const int BodySize = 64;
	public const int PacketSize = HeadSize + BodySize;

	[MarshalAs(UnmanagedType.I4)]
	public Protocol type;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = BodySize)]
	public byte[] data;

	public T GetData<T>()
	{
		return (T)ByteToStructure (data, typeof(T));
	}

	public void SetData(object obj)
	{
		data = StructureToByte (obj);
	}

	public static byte[] Serialize(Packet packet)
	{
		return StructureToByte (packet);
	}

	public static Packet Deserialize(byte[] bytes)
	{
		return (Packet)ByteToStructure (bytes, typeof(Packet));
	}

	public static object ByteToStructure(byte[] data, Type type)
	{
		IntPtr buff = Marshal.AllocHGlobal(data.Length);
		Marshal.Copy(data, 0, buff, data.Length);
		object obj = Marshal.PtrToStructure(buff, type); // 복사된 데이터를 구조체 객체로 변환한다.
		Marshal.FreeHGlobal(buff);
		return obj;
	}

	public static byte[] StructureToByte(object obj)
	{
		int datasize = Marshal.SizeOf(obj);
		IntPtr buff = Marshal.AllocHGlobal(datasize);
		Marshal.StructureToPtr(obj, buff, false);
		byte[] data = new byte[datasize];
		Marshal.Copy(buff, data, 0, datasize);
		Marshal.FreeHGlobal(buff);
		return data; // 배열을 리턴
	}
}

public class Client : MonoBehaviour {

	public static Client Instance;
	public Action<Packet> OnPacketReceived;

	public System.Net.Sockets.Socket Socket { get {return socket;} }
	public readonly object ReceiveLocker = new object();
	public Queue<byte[]> PacketQueue = new Queue<byte[]>();

	private System.Net.Sockets.Socket socket = null;

	private SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
	private byte[] sendBuf = new byte[Packet.PacketSize];

	private SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
	private byte[] recvBuf = new byte[Packet.PacketSize];

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		Instance = this;
	}

	// Use this for initialization
	public void Connect ()
	{
		//string hostName = "ec2-52-78-64-146.ap-northeast-2.compute.amazonaws.com";
		string hostName = "52.78.64.146";
		//string hostName = "127.0.0.1";
		IPHostEntry ipHost = Dns.GetHostEntry(hostName);
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8000);

		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		recvArgs.SetBuffer(recvBuf, 0, recvBuf.Length);
		recvArgs.Completed += OnReceived;

		sendArgs.SetBuffer(sendBuf, 0, sendBuf.Length);
		sendArgs.Completed += OnSended;

		SocketAsyncEventArgs args = new SocketAsyncEventArgs ();
		args.RemoteEndPoint = ipEndPoint;
		args.Completed += OnConnectCompleted;
		args.UserToken = socket;
		socket.ConnectAsync(args);
	}

	void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
	{
		Debug.Log ("completed: " + args.SocketError.ToString());
		ReceiveAsync ();
	}

	// Update is called once per frame
	void Update ()
	{
		byte[] packet = null;
		lock(ReceiveLocker)
		{
			if (PacketQueue.Count > 0)
			{
				packet = PacketQueue.Dequeue ();
			}
		}

		if (packet != null)
			OnPacketReceived (Packet.Deserialize(packet));
	}


	public void CloseSocket()
	{
		try
		{
			socket.Shutdown(SocketShutdown.Send);
		}
		catch (Exception) { }

		socket.Close();
	}

	public void SendAsync(byte[] buf)
	{
		int length = Math.Min(sendBuf.Length, buf.Length);
		Buffer.BlockCopy(buf, 0, sendBuf, 0, length);

		if (!socket.SendAsync(sendArgs))
			ProcessSend(sendArgs);
	}

	private void ProcessSend(SocketAsyncEventArgs args)
	{
		if (args.SocketError == SocketError.Success)
		{
		}
		else
		{
			CloseSocket();
		}
	}

	private void OnSended(object sender, SocketAsyncEventArgs args)
	{
		ProcessSend(args);
	}

	public void ReceiveAsync()
	{
		if (!socket.ReceiveAsync(recvArgs))
			ProcessReceive(recvArgs);
	}

	private void ProcessReceive(SocketAsyncEventArgs args)
	{
		if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
		{
			lock (ReceiveLocker)
			{
				byte[] packet = new byte[args.BytesTransferred];
				Buffer.BlockCopy(args.Buffer, 0, packet, 0, packet.Length);
				PacketQueue.Enqueue(packet);
			}
			ReceiveAsync();
		}
		else
		{
			CloseSocket();
		}
	}

	private void OnReceived(object sender, SocketAsyncEventArgs args)
	{
		ProcessReceive(args);
	}
}
