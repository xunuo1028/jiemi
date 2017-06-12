﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_EventSystems_EventSystemWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.UIBehaviour));
		L.RegFunction("UpdateModules", UpdateModules);
		L.RegFunction("SetSelectedGameObject", SetSelectedGameObject);
		L.RegFunction("RaycastAll", RaycastAll);
		L.RegFunction("IsPointerOverGameObject", IsPointerOverGameObject);
		L.RegFunction("ToString", ToString);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("current", get_current, set_current);
		L.RegVar("sendNavigationEvents", get_sendNavigationEvents, set_sendNavigationEvents);
		L.RegVar("pixelDragThreshold", get_pixelDragThreshold, set_pixelDragThreshold);
		L.RegVar("currentInputModule", get_currentInputModule, null);
		L.RegVar("firstSelectedGameObject", get_firstSelectedGameObject, set_firstSelectedGameObject);
		L.RegVar("currentSelectedGameObject", get_currentSelectedGameObject, null);
		L.RegVar("alreadySelecting", get_alreadySelecting, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateModules(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.CheckObject(L, 1, typeof(UnityEngine.EventSystems.EventSystem));
			obj.UpdateModules();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSelectedGameObject(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.GameObject)))
			{
				UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.ToObject(L, 1);
				UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.ToObject(L, 2);
				obj.SetSelectedGameObject(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.GameObject), typeof(UnityEngine.EventSystems.BaseEventData)))
			{
				UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.ToObject(L, 1);
				UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.ToObject(L, 2);
				UnityEngine.EventSystems.BaseEventData arg1 = (UnityEngine.EventSystems.BaseEventData)ToLua.ToObject(L, 3);
				obj.SetSelectedGameObject(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.EventSystems.EventSystem.SetSelectedGameObject");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RaycastAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.CheckObject(L, 1, typeof(UnityEngine.EventSystems.EventSystem));
			UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)ToLua.CheckObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
			System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> arg1 = (System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>)ToLua.CheckObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>));
			obj.RaycastAll(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPointerOverGameObject(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.EventSystems.EventSystem)))
			{
				UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.ToObject(L, 1);
				bool o = obj.IsPointerOverGameObject();
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.EventSystems.EventSystem), typeof(int)))
			{
				UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				bool o = obj.IsPointerOverGameObject(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.EventSystems.EventSystem.IsPointerOverGameObject");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)ToLua.CheckObject(L, 1, typeof(UnityEngine.EventSystems.EventSystem));
			string o = obj.ToString();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		try
		{
			ToLua.Push(L, UnityEngine.EventSystems.EventSystem.current);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sendNavigationEvents(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			bool ret = obj.sendNavigationEvents;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index sendNavigationEvents on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelDragThreshold(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			int ret = obj.pixelDragThreshold;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pixelDragThreshold on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currentInputModule(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			UnityEngine.EventSystems.BaseInputModule ret = obj.currentInputModule;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index currentInputModule on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_firstSelectedGameObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			UnityEngine.GameObject ret = obj.firstSelectedGameObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index firstSelectedGameObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currentSelectedGameObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			UnityEngine.GameObject ret = obj.currentSelectedGameObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index currentSelectedGameObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alreadySelecting(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			bool ret = obj.alreadySelecting;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alreadySelecting on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		try
		{
			UnityEngine.EventSystems.EventSystem arg0 = (UnityEngine.EventSystems.EventSystem)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.EventSystems.EventSystem));
			UnityEngine.EventSystems.EventSystem.current = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sendNavigationEvents(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.sendNavigationEvents = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index sendNavigationEvents on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelDragThreshold(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.pixelDragThreshold = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pixelDragThreshold on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_firstSelectedGameObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.EventSystems.EventSystem obj = (UnityEngine.EventSystems.EventSystem)o;
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			obj.firstSelectedGameObject = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index firstSelectedGameObject on a nil value" : e.Message);
		}
	}
}

