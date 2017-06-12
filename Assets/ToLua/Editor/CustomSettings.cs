﻿using System.Collections;
//using Newtonsoft.Json.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Director;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BindType = ToLuaMenu.BindType;
using System.Reflection;

public static class CustomSettings
{
    public static string saveDir = Application.dataPath + "/ToLua/Source/Generate/";
    public static string luaDir = Application.dataPath + "/Lua/";
    public static string toluaBaseType = Application.dataPath + "/ToLua/BaseType/";
    public static string toluaLuaDir = Application.dataPath + "/ToLua/Lua";

    //导出时强制做为静态类的类型(注意customTypeList 还要添加这个类型才能导出)
    //unity 有些类作为sealed class, 其实完全等价于静态类
    public static List<Type> staticClassTypes = new List<Type>
    {        
        typeof(UnityEngine.Application),
        typeof(UnityEngine.Time),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.SleepTimeout),
        typeof(UnityEngine.Input),
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.Physics),
        typeof(UnityEngine.RenderSettings),
        typeof(UnityEngine.QualitySettings),
        typeof(UnityEngine.GL),
        typeof(UnityEngine.RectTransformUtility),
    };

    //附加导出委托类型(在导出委托时, customTypeList 中牵扯的委托类型都会导出， 无需写在这里)
    public static DelegateType[] customDelegateList = 
    {        
        _DT(typeof(Action)),                
        _DT(typeof(UnityEngine.Events.UnityAction)),
        _DT(typeof(System.Predicate<int>)),
        _DT(typeof(System.Action<int>)),
        _DT(typeof(System.Comparison<int>)),
    };

    //在这里添加你要导出注册到lua的类型列表
    public static BindType[] customTypeList =
    {                
        //------------------------为例子导出--------------------------------
        //_GT(typeof(TestEventListener)),
        //_GT(typeof(TestProtol)),
        //_GT(typeof(TestAccount)),
        //_GT(typeof(Dictionary<int, TestAccount>)).SetLibName("AccountMap"),
        //_GT(typeof(KeyValuePair<int, TestAccount>)),    
        //_GT(typeof(TestExport)),
        //_GT(typeof(TestExport.Space)),
        //-------------------------------------------------------------------        
                
        _GT(typeof(Debugger)).SetNameSpace(null),        
        
        _GT(typeof(System.IO.Directory)),
        _GT(typeof(System.IO.File)),

        _GT(typeof (UnityEngine.RectTransformUtility)),

        //add
        _GT(typeof (Rect)),
        _GT(typeof (Sprite)),
        _GT(typeof (Image)),
        _GT(typeof (Slider)),
        _GT(typeof (Toggle)),
        _GT(typeof (Outline)),
        _GT(typeof (Dropdown)),
        _GT(typeof (Dropdown.OptionData)),
        _GT(typeof (Shadow)),
        _GT(typeof (ScrollRect)),
        _GT(typeof (Scrollbar)),
        _GT(typeof (LayoutGroup)),
        _GT(typeof (LayoutElement)),
        _GT(typeof (Canvas)),
        _GT(typeof (CanvasGroup)),
        _GT(typeof (CanvasScaler)),
        _GT(typeof (InputField)),
        _GT(typeof (AudioListener)),
        _GT(typeof (DateTime)),
        _GT(typeof (EventTrigger)),
        _GT(typeof (AbstractEventData)),
        _GT(typeof (BaseRaycaster)),
        _GT(typeof (ParticleSystemRenderer)),        
        _GT(typeof (TrailRenderer)),
        _GT(typeof (DirectorPlayer)),          
        _GT(typeof (BaseMeshEffect)),       
        _GT(typeof (RectTransform)),      
        _GT(typeof (Text)),      
        _GT(typeof (SceneManager)),      
        _GT(typeof (LoadSceneMode)),      
        _GT(typeof (RuntimePlatform)),   
        _GT(typeof (EventSystem)),   

        _GT(typeof (GridLayoutGroup)),   
        _GT(typeof (HorizontalLayoutGroup)),   
        _GT(typeof (GraphicRaycaster)),   
        _GT(typeof (RectOffset)),   
        _GT(typeof (Button)),   

        //custom           
        _GT(typeof (LuaScript)),
        _GT(typeof (LuaScriptExt)),
        _GT(typeof (NetService)),
        _GT(typeof (NetConnection)),
        _GT(typeof (ApplicationController)),
        _GT(typeof (ResourceController)),
        _GT(typeof (ResourceController.AssetLoader)),
        _GT(typeof (EventTriggerProxy)),        

        //custom depending system class        
        _GT(typeof (PointerEventData)),
        _GT(typeof (BaseEventData)),
        _GT(typeof (Scene)),     

        //utils
        _GT(typeof (Queue)),
        _GT(typeof (Stack)),
        _GT(typeof (ArrayList)),
        //_GT(typeof (System.IO.File)),
        //_GT(typeof (System.IO.Directory)),

        //json
//        _GT(typeof (JObject)),
//        _GT(typeof (JContainer)),
//        _GT(typeof (JToken)),
//        _GT(typeof (JProperty)),
//        _GT(typeof (JValue)),
//        _GT(typeof (JArray)),

#if USING_DOTWEENING
        _GT(typeof(DG.Tweening.DOTween)),
        _GT(typeof(DG.Tweening.Tween)).SetBaseType(typeof(System.Object)).AddExtendType(typeof(DG.Tweening.TweenExtensions)),
        _GT(typeof(DG.Tweening.Sequence)).AddExtendType(typeof(DG.Tweening.TweenSettingsExtensions)),
        _GT(typeof(DG.Tweening.Tweener)).AddExtendType(typeof(DG.Tweening.TweenSettingsExtensions)),
        _GT(typeof(DG.Tweening.LoopType)),
        _GT(typeof(DG.Tweening.PathMode)),
        _GT(typeof(DG.Tweening.PathType)),
        _GT(typeof(DG.Tweening.RotateMode)),
        _GT(typeof(Component)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Transform)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Light)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Material)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Rigidbody)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Camera)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(AudioSource)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        //_GT(typeof(LineRenderer)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        //_GT(typeof(TrailRenderer)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),    
#else
                                         
        _GT(typeof(Component)),
        _GT(typeof(Transform)),
        _GT(typeof(Material)),
        _GT(typeof(Light)),
        _GT(typeof(Rigidbody)),
        _GT(typeof(Camera)),
        _GT(typeof(AudioSource)),
        //_GT(typeof(LineRenderer))
        //_GT(typeof(TrailRenderer))
#endif
      
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),        
        _GT(typeof(GameObject)),
        _GT(typeof(TrackedReference)),
        _GT(typeof(Application)),
        _GT(typeof(Physics)),
        _GT(typeof(Collider)),
        _GT(typeof(Time)),        
        _GT(typeof(Texture)),
        _GT(typeof(Texture2D)),
        _GT(typeof(Shader)),        
        _GT(typeof(Renderer)),
        _GT(typeof(TextEditor)),
        _GT(typeof(WWW)),
        _GT(typeof(Screen)),        
        _GT(typeof(CameraClearFlags)),
        _GT(typeof(AudioClip)),        
        _GT(typeof(AssetBundle)),
        _GT(typeof(ParticleSystem)),
        _GT(typeof(AsyncOperation)).SetBaseType(typeof(System.Object)),        
        _GT(typeof(LightType)),
        _GT(typeof(SleepTimeout)),
        _GT(typeof(Animator)),
        _GT(typeof(Input)),
        _GT(typeof(KeyCode)),
        _GT(typeof(SkinnedMeshRenderer)),
        _GT(typeof(Space)), 
        _GT(typeof(Keyframe)),
        _GT(typeof(Quaternion)),
       

        _GT(typeof(MeshRenderer)),            
        _GT(typeof(ParticleEmitter)),
        _GT(typeof(ParticleRenderer)),
        _GT(typeof(ParticleAnimator)), 
                              
        _GT(typeof(BoxCollider)),
        _GT(typeof(MeshCollider)),
        _GT(typeof(SphereCollider)),        
        _GT(typeof(CharacterController)),
        _GT(typeof(CapsuleCollider)),
        
        _GT(typeof(Animation)),        
        _GT(typeof(AnimationClip)).SetBaseType(typeof(UnityEngine.Object)),        
        _GT(typeof(AnimationState)),
        _GT(typeof(AnimationCurve)),
        _GT(typeof(AnimationBlendMode)),
        _GT(typeof(QueueMode)),  
        _GT(typeof(PlayMode)),
        _GT(typeof(WrapMode)),

        _GT(typeof(QualitySettings)),
        _GT(typeof(RenderSettings)),                                                   
        _GT(typeof(BlendWeights)),           
        _GT(typeof(RenderTexture)),
    };

    public static List<Type> dynamicList = new List<Type>()
    {
        typeof(MeshRenderer),
        typeof(ParticleEmitter),
        typeof(ParticleRenderer),
        typeof(ParticleAnimator),

        typeof(BoxCollider),
        typeof(MeshCollider),
        typeof(SphereCollider),
        typeof(CharacterController),
        typeof(CapsuleCollider),

        typeof(Animation),
        typeof(AnimationClip),
        typeof(AnimationState),

        typeof(BlendWeights),
        typeof(RenderTexture),
        typeof(Rigidbody),
    };

    //重载函数，相同参数个数，相同位置out参数匹配出问题时, 需要强制匹配解决
    //使用方法参见例子14
    public static List<Type> outList = new List<Type>()
    {
        
    };

    public static BindType _GT(Type t)
    {
        return new BindType(t);
    }

    public static DelegateType _DT(Type t)
    {
        return new DelegateType(t);
    }    
}
