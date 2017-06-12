//using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
//using Newtonsoft.Json.Utilities;
//using UnityEngine;
//using System.Collections;
//
//public class NetServicePool
//{
//    public Dictionary<string, ArrayList> netRequestPool;
//
//    public NetServicePool()
//    {
//        netRequestPool = new Dictionary<string, ArrayList>();
//    }
//
//    public NetServiceLegacy.NetRequest NewRequest(string type)
//    {
//        if (netRequestPool.ContainsKey(type) && netRequestPool[type].Count > 0)
//        {
//            var request = (NetServiceLegacy.NetRequest) netRequestPool[type][0];
//            netRequestPool[type].RemoveAt(0);
//            return request;
//        }
//
//        return null;
//    }
//
//    public void ReleaseRequest(NetServiceLegacy.NetRequest request)
//    {
//        var type = request.Content["Type"].ToString();
//        if (!netRequestPool.ContainsKey(type))
//        {
//            netRequestPool.Add(type, new ArrayList());
//        }
//
//        if (netRequestPool[type].Count < 100)
//            netRequestPool[type].Add(request);
//    }
//}