using System;
using System.Collections;
using System.Net;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LuaInterface;

public class NetConnection {

    public readonly string Address;
    public readonly int Port;

    private NetService netService;
    private Socket soc;
    private Socket udp;

    private bool running;
    private int receiveTimeout;
    private Thread mainThread;
    private Thread udpThread;

    public NetConnection(NetService netService, string address, int port)
    {
        this.netService = netService;
        this.Address = address;
        this.Port = port;

        Packets = Queue.Synchronized(new Queue());
    }

    private void Connect()
    {
        connected = false;
        connectDone.Reset();

        try
        {
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.BeginConnect(Address, Port, ConnectCallback, soc);

            connectDone.WaitOne(1000, true);
            if (connected)
            {
                Debug.LogFormat("Connected to Server {0} Port {1}", Address, Port);

                udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                var udpEndPoint = new IPEndPoint(IPAddress.Any, 21888);
                udp.Bind(udpEndPoint);

                udpThread = new Thread(UdpThread);
                udpThread.Start();
            }
            else
                Debug.LogWarningFormat("Connect to Address {0} Port {1} Timeout", Address, Port);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void Start(int receiveTimeout = 0)
    {
        this.running = true;
        this.receiveTimeout = receiveTimeout;

        this.Connect();

        mainThread = new Thread(MainThread);
        mainThread.Start();
    }

    public void Stop()
    {
        if (running)
        {
            running = false;
            connected = false;

            if (soc != null)
                soc.Close();
            if (udp != null)
                udp.Close();

            mainThread.Interrupt();
        }
    }

    private ManualResetEvent connectDone = new ManualResetEvent(false);
    public bool connected = false;

    public Queue Packets;

    private void ConnectCallback(IAsyncResult ar)
    {
        Socket s = (Socket)ar.AsyncState;
        s.EndConnect(ar);
        connected = true;
        connectDone.Set();
    }

    private void MainThread()
    {
        while (running)
        {
            try
            {
                while (running && connected)
                {
                    Packets.Enqueue(recvPacket());
                }
            }
            catch (Exception e)
            {
                connected = false;

                Debug.Log(e.ToString());                
            }

            if (soc != null)   
                soc.Close();            

            if (running)
            {
                Debug.Log("Wait 1s to reconnect...");
                Thread.Sleep(1000);

                this.Connect();
            }
        }

        Debug.Log("Connection Main Thread Exit");
    }

    private void UdpThread()
    {
        try
        {
            while (running && connected)
            {
                Packets.Enqueue(recvDgram());
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        if (udp != null)
            udp.Close();

        Debug.Log("Connection Udp Thread Exit");
    }

    public void SendPacket(LuaByteBuffer packet)
    {
        if (!connected)
            return;

        if (packet.Length > 0x1 << 16 - 1)
        {
            Debug.LogError("request length too large");
            return;
        }

        var length = new byte[2];
        length[0] = (byte)(packet.Length >> 8);
        length[1] = (byte)(packet.Length & 0xFF);

        var body = packet.buffer;

        lock (soc)
        {
            if (soc.Send(length, 2, SocketFlags.None) != 2)
            {
                throw new Exception("send length failed");
            }

            if (soc.Send(body, body.Length, SocketFlags.None) != body.Length)
            {
                throw new Exception("send body failed");
            }
        }
    }

    private LuaByteBuffer recvPacket()
    {
        soc.ReceiveTimeout = receiveTimeout;

        var length = new byte[2];
        var read = 0;
        while (read < 2)
        {
            var n = soc.Receive(length, read, 2 - read, SocketFlags.None);
            read += n;
        }

        soc.ReceiveTimeout = 1000;

        var body = new byte[length[0] << 8 | length[1]];
        read = 0;
        while (read < body.Length)
        {
            var n = soc.Receive(body, read, body.Length - read, SocketFlags.None);
            read += n;
        }

        return new LuaByteBuffer(body);
    }

    public void SendDgram(LuaByteBuffer packet, int serverPort)
    {
        EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(this.Address), serverPort);

        if (udp.SendTo(packet.buffer, packet.buffer.Length, SocketFlags.None, serverEndPoint) != packet.buffer.Length)
        {
            throw new Exception("send udp body failed");
        }
    }

    private byte[] packet = new byte[4096];

    private LuaByteBuffer recvDgram()
    {
        EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse(this.Address), 0);
        
        var n = udp.ReceiveFrom(packet, SocketFlags.None, ref senderEndPoint);
        var body = new byte[n];
        Buffer.BlockCopy(packet, 0, body, 0, n);       

        return new LuaByteBuffer(body);
    }
}
