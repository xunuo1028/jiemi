using UnityEngine;
using System.Collections;
using System.Threading;
using LuaInterface;

public class NetService
{
    private static NetService instance;

    public static NetService GetInstance()
    {
        if (instance == null)
        {
            instance = new NetService();
        }

        return instance;
    }

    private LuaFunction lDispatchPacket = null;

    public ArrayList NetConnections;

    private NetService()
    {
        NetConnections = new ArrayList();
        lDispatchPacket = ApplicationController.Instance().LuaGetFunction("Net", "DispatchPacket");
    }


    public NetConnection Connect(string address, int port)
    {
        var conn = new NetConnection(this, address, port);        
        conn.Start();

        NetConnections.Add(conn);

        return conn;
    }

    public IEnumerator PacketDispatcher()
    {
        while (true)
        {            
            for (int i = 0; i < NetConnections.Count; i++)
            {
                var netConnection = NetConnections[i] as NetConnection;
                while (netConnection.Packets.Count > 0)
                {
                    var packet = netConnection.Packets.Dequeue();

                    lDispatchPacket.BeginPCall();
                    lDispatchPacket.Push(netConnection);
                    lDispatchPacket.Push(packet);
                    lDispatchPacket.PCall();
                    lDispatchPacket.EndPCall();
                }
            }

            yield return null;
        }
    }

    public void Stop()
    {
        for (int i = NetConnections.Count - 1; i >= 0; i--)
        {
            (NetConnections[i] as NetConnection).Stop();
            NetConnections.Remove(i);
        }
    }


}
