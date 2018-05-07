//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ResourceController_AssetLoaderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ResourceController.AssetLoader), typeof(System.Object));
		L.RegFunction("Close", Close);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("New", _CreateResourceController_AssetLoader);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Done", get_Done, set_Done);
		L.RegVar("Asset", get_Asset, set_Asset);
		L.RegVar("abname", get_abname, set_abname);
		L.RegVar("assetname", get_assetname, set_assetname);
		L.RegVar("assettype", get_assettype, set_assettype);
		L.RegVar("bundle", get_bundle, set_bundle);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateResourceController_AssetLoader(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				ResourceController.AssetLoader obj = new ResourceController.AssetLoader();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: ResourceController.AssetLoader.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)ToLua.CheckObject(L, 1, typeof(ResourceController.AssetLoader));
			obj.Close();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ResourceController.AssetLoader), typeof(string), typeof(string)))
			{
				ResourceController.AssetLoader obj = (ResourceController.AssetLoader)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				obj.LoadAsset(arg0, arg1);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes(L, 1, typeof(ResourceController.AssetLoader), typeof(string), typeof(string), typeof(string)))
			{
				ResourceController.AssetLoader obj = (ResourceController.AssetLoader)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				string arg2 = ToLua.ToString(L, 4);
				obj.LoadAsset(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ResourceController.AssetLoader.LoadAsset");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Done(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			bool ret = obj.Done;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Done on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Asset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			UnityEngine.Object ret = obj.Asset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Asset on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_abname(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string ret = obj.abname;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index abname on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assetname(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string ret = obj.assetname;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index assetname on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assettype(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string ret = obj.assettype;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index assettype on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bundle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			UnityEngine.AssetBundle ret = obj.bundle;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bundle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Done(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Done = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Done on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Asset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Object));
			obj.Asset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Asset on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_abname(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.abname = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index abname on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assetname(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.assetname = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index assetname on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assettype(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.assettype = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index assettype on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bundle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ResourceController.AssetLoader obj = (ResourceController.AssetLoader)o;
			UnityEngine.AssetBundle arg0 = (UnityEngine.AssetBundle)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.AssetBundle));
			obj.bundle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bundle on a nil value" : e.Message);
		}
	}
}

