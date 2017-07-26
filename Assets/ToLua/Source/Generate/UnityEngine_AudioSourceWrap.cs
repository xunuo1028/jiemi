//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_AudioSourceWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.AudioSource), typeof(UnityEngine.Behaviour));
		L.RegFunction("Play", Play);
		L.RegFunction("PlayDelayed", PlayDelayed);
		L.RegFunction("PlayScheduled", PlayScheduled);
		L.RegFunction("SetScheduledStartTime", SetScheduledStartTime);
		L.RegFunction("SetScheduledEndTime", SetScheduledEndTime);
		L.RegFunction("Stop", Stop);
		L.RegFunction("Pause", Pause);
		L.RegFunction("UnPause", UnPause);
		L.RegFunction("PlayOneShot", PlayOneShot);
		L.RegFunction("PlayClipAtPoint", PlayClipAtPoint);
		L.RegFunction("SetCustomCurve", SetCustomCurve);
		L.RegFunction("GetCustomCurve", GetCustomCurve);
		L.RegFunction("GetOutputData", GetOutputData);
		L.RegFunction("GetSpectrumData", GetSpectrumData);
		L.RegFunction("SetSpatializerFloat", SetSpatializerFloat);
		L.RegFunction("GetSpatializerFloat", GetSpatializerFloat);
		L.RegFunction("New", _CreateUnityEngine_AudioSource);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("volume", get_volume, set_volume);
		L.RegVar("pitch", get_pitch, set_pitch);
		L.RegVar("time", get_time, set_time);
		L.RegVar("timeSamples", get_timeSamples, set_timeSamples);
		L.RegVar("clip", get_clip, set_clip);
		L.RegVar("outputAudioMixerGroup", get_outputAudioMixerGroup, set_outputAudioMixerGroup);
		L.RegVar("isPlaying", get_isPlaying, null);
		L.RegVar("isVirtual", get_isVirtual, null);
		L.RegVar("loop", get_loop, set_loop);
		L.RegVar("ignoreListenerVolume", get_ignoreListenerVolume, set_ignoreListenerVolume);
		L.RegVar("playOnAwake", get_playOnAwake, set_playOnAwake);
		L.RegVar("ignoreListenerPause", get_ignoreListenerPause, set_ignoreListenerPause);
		L.RegVar("velocityUpdateMode", get_velocityUpdateMode, set_velocityUpdateMode);
		L.RegVar("panStereo", get_panStereo, set_panStereo);
		L.RegVar("spatialBlend", get_spatialBlend, set_spatialBlend);
		L.RegVar("spatialize", get_spatialize, set_spatialize);
		L.RegVar("spatializePostEffects", get_spatializePostEffects, set_spatializePostEffects);
		L.RegVar("reverbZoneMix", get_reverbZoneMix, set_reverbZoneMix);
		L.RegVar("bypassEffects", get_bypassEffects, set_bypassEffects);
		L.RegVar("bypassListenerEffects", get_bypassListenerEffects, set_bypassListenerEffects);
		L.RegVar("bypassReverbZones", get_bypassReverbZones, set_bypassReverbZones);
		L.RegVar("dopplerLevel", get_dopplerLevel, set_dopplerLevel);
		L.RegVar("spread", get_spread, set_spread);
		L.RegVar("priority", get_priority, set_priority);
		L.RegVar("mute", get_mute, set_mute);
		L.RegVar("minDistance", get_minDistance, set_minDistance);
		L.RegVar("maxDistance", get_maxDistance, set_maxDistance);
		L.RegVar("rolloffMode", get_rolloffMode, set_rolloffMode);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_AudioSource(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UnityEngine.AudioSource obj = new UnityEngine.AudioSource();
				ToLua.Push(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AudioSource.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioSource)))
			{
				UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.ToObject(L, 1);
				obj.Play();
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioSource), typeof(ulong)))
			{
				UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.ToObject(L, 1);
				ulong arg0 = LuaDLL.tolua_touint64(L, 2);
				obj.Play(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AudioSource.Play");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayDelayed(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.PlayDelayed(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayScheduled(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			double arg0 = (double)LuaDLL.luaL_checknumber(L, 2);
			obj.PlayScheduled(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetScheduledStartTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			double arg0 = (double)LuaDLL.luaL_checknumber(L, 2);
			obj.SetScheduledStartTime(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetScheduledEndTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			double arg0 = (double)LuaDLL.luaL_checknumber(L, 2);
			obj.SetScheduledEndTime(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			obj.Stop();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			obj.Pause();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnPause(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			obj.UnPause();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayOneShot(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioSource), typeof(UnityEngine.AudioClip)))
			{
				UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.ToObject(L, 1);
				UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.ToObject(L, 2);
				obj.PlayOneShot(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioSource), typeof(UnityEngine.AudioClip), typeof(float)))
			{
				UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.ToObject(L, 1);
				UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.ToObject(L, 2);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
				obj.PlayOneShot(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AudioSource.PlayOneShot");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayClipAtPoint(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioClip), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.AudioSource.PlayClipAtPoint(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.AudioClip), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.AudioSource.PlayClipAtPoint(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AudioSource.PlayClipAtPoint");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCustomCurve(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			UnityEngine.AudioSourceCurveType arg0 = (UnityEngine.AudioSourceCurveType)ToLua.CheckObject(L, 2, typeof(UnityEngine.AudioSourceCurveType));
			UnityEngine.AnimationCurve arg1 = (UnityEngine.AnimationCurve)ToLua.CheckObject(L, 3, typeof(UnityEngine.AnimationCurve));
			obj.SetCustomCurve(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCustomCurve(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			UnityEngine.AudioSourceCurveType arg0 = (UnityEngine.AudioSourceCurveType)ToLua.CheckObject(L, 2, typeof(UnityEngine.AudioSourceCurveType));
			UnityEngine.AnimationCurve o = obj.GetCustomCurve(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOutputData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			float[] arg0 = ToLua.CheckNumberArray<float>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.GetOutputData(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSpectrumData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			float[] arg0 = ToLua.CheckNumberArray<float>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			UnityEngine.FFTWindow arg2 = (UnityEngine.FFTWindow)ToLua.CheckObject(L, 4, typeof(UnityEngine.FFTWindow));
			obj.GetSpectrumData(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSpatializerFloat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			bool o = obj.SetSpatializerFloat(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSpatializerFloat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioSource));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			float arg1;
			bool o = obj.GetSpatializerFloat(arg0, out arg1);
			LuaDLL.lua_pushboolean(L, o);
			LuaDLL.lua_pushnumber(L, arg1);
			return 2;
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
	static int get_volume(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.volume;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index volume on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pitch(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.pitch;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pitch on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.time;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index time on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timeSamples(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			int ret = obj.timeSamples;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index timeSamples on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_clip(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioClip ret = obj.clip;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index clip on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_outputAudioMixerGroup(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.Audio.AudioMixerGroup ret = obj.outputAudioMixerGroup;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index outputAudioMixerGroup on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPlaying(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.isPlaying;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isPlaying on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isVirtual(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.isVirtual;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isVirtual on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.loop;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ignoreListenerVolume(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.ignoreListenerVolume;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreListenerVolume on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playOnAwake(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.playOnAwake;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playOnAwake on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ignoreListenerPause(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.ignoreListenerPause;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreListenerPause on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocityUpdateMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioVelocityUpdateMode ret = obj.velocityUpdateMode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index velocityUpdateMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_panStereo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.panStereo;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index panStereo on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spatialBlend(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.spatialBlend;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatialBlend on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spatialize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.spatialize;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatialize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spatializePostEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.spatializePostEffects;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatializePostEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reverbZoneMix(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.reverbZoneMix;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index reverbZoneMix on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bypassEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.bypassEffects;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bypassListenerEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.bypassListenerEffects;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassListenerEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bypassReverbZones(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.bypassReverbZones;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassReverbZones on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dopplerLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.dopplerLevel;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index dopplerLevel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spread(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.spread;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spread on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_priority(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			int ret = obj.priority;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index priority on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mute(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool ret = obj.mute;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mute on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.minDistance;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minDistance on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float ret = obj.maxDistance;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxDistance on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rolloffMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioRolloffMode ret = obj.rolloffMode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rolloffMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_volume(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.volume = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index volume on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pitch(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.pitch = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pitch on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.time = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index time on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_timeSamples(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.timeSamples = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index timeSamples on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clip(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.AudioClip));
			obj.clip = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index clip on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_outputAudioMixerGroup(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.Audio.AudioMixerGroup arg0 = (UnityEngine.Audio.AudioMixerGroup)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Audio.AudioMixerGroup));
			obj.outputAudioMixerGroup = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index outputAudioMixerGroup on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_loop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.loop = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ignoreListenerVolume(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.ignoreListenerVolume = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreListenerVolume on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playOnAwake(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.playOnAwake = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playOnAwake on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ignoreListenerPause(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.ignoreListenerPause = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreListenerPause on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_velocityUpdateMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioVelocityUpdateMode arg0 = (UnityEngine.AudioVelocityUpdateMode)ToLua.CheckObject(L, 2, typeof(UnityEngine.AudioVelocityUpdateMode));
			obj.velocityUpdateMode = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index velocityUpdateMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_panStereo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.panStereo = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index panStereo on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spatialBlend(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.spatialBlend = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatialBlend on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spatialize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.spatialize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatialize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spatializePostEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.spatializePostEffects = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spatializePostEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reverbZoneMix(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.reverbZoneMix = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index reverbZoneMix on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bypassEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.bypassEffects = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bypassListenerEffects(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.bypassListenerEffects = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassListenerEffects on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bypassReverbZones(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.bypassReverbZones = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bypassReverbZones on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dopplerLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.dopplerLevel = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index dopplerLevel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spread(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.spread = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spread on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_priority(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.priority = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index priority on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mute(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.mute = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mute on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.minDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minDistance on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.maxDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxDistance on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rolloffMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioSource obj = (UnityEngine.AudioSource)o;
			UnityEngine.AudioRolloffMode arg0 = (UnityEngine.AudioRolloffMode)ToLua.CheckObject(L, 2, typeof(UnityEngine.AudioRolloffMode));
			obj.rolloffMode = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rolloffMode on a nil value" : e.Message);
		}
	}
}

