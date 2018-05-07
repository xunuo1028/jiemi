//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_UI_GridLayoutGroupWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.UI.GridLayoutGroup), typeof(UnityEngine.UI.LayoutGroup));
		L.RegFunction("CalculateLayoutInputHorizontal", CalculateLayoutInputHorizontal);
		L.RegFunction("CalculateLayoutInputVertical", CalculateLayoutInputVertical);
		L.RegFunction("SetLayoutHorizontal", SetLayoutHorizontal);
		L.RegFunction("SetLayoutVertical", SetLayoutVertical);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("startCorner", get_startCorner, set_startCorner);
		L.RegVar("startAxis", get_startAxis, set_startAxis);
		L.RegVar("cellSize", get_cellSize, set_cellSize);
		L.RegVar("spacing", get_spacing, set_spacing);
		L.RegVar("constraint", get_constraint, set_constraint);
		L.RegVar("constraintCount", get_constraintCount, set_constraintCount);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputHorizontal(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)ToLua.CheckObject(L, 1, typeof(UnityEngine.UI.GridLayoutGroup));
			obj.CalculateLayoutInputHorizontal();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputVertical(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)ToLua.CheckObject(L, 1, typeof(UnityEngine.UI.GridLayoutGroup));
			obj.CalculateLayoutInputVertical();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutHorizontal(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)ToLua.CheckObject(L, 1, typeof(UnityEngine.UI.GridLayoutGroup));
			obj.SetLayoutHorizontal();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutVertical(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)ToLua.CheckObject(L, 1, typeof(UnityEngine.UI.GridLayoutGroup));
			obj.SetLayoutVertical();
			return 0;
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
	static int get_startCorner(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Corner ret = obj.startCorner;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startCorner on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Axis ret = obj.startAxis;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cellSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.Vector2 ret = obj.cellSize;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cellSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spacing(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.Vector2 ret = obj.spacing;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacing on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_constraint(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Constraint ret = obj.constraint;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index constraint on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_constraintCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			int ret = obj.constraintCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index constraintCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startCorner(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Corner arg0 = (UnityEngine.UI.GridLayoutGroup.Corner)ToLua.CheckObject(L, 2, typeof(UnityEngine.UI.GridLayoutGroup.Corner));
			obj.startCorner = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startCorner on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Axis arg0 = (UnityEngine.UI.GridLayoutGroup.Axis)ToLua.CheckObject(L, 2, typeof(UnityEngine.UI.GridLayoutGroup.Axis));
			obj.startAxis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cellSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.cellSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cellSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spacing(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.spacing = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacing on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_constraint(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			UnityEngine.UI.GridLayoutGroup.Constraint arg0 = (UnityEngine.UI.GridLayoutGroup.Constraint)ToLua.CheckObject(L, 2, typeof(UnityEngine.UI.GridLayoutGroup.Constraint));
			obj.constraint = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index constraint on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_constraintCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.UI.GridLayoutGroup obj = (UnityEngine.UI.GridLayoutGroup)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.constraintCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index constraintCount on a nil value" : e.Message);
		}
	}
}

