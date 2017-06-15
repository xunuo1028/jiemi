using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections;
using System.Collections.Generic;
public class UIToolsTree : EditorWindow
{

    Object st;
    public static Transform obj;
    private int level = 0;
    private static Dictionary<string, List<Transform>> dic_trans = new Dictionary<string, List<Transform>>();
    private static Dictionary<string, List<bool>> dic_bool = new Dictionary<string, List<bool>>();
    private static Dictionary<string, bool> dic_forFoldout = new Dictionary<string, bool>();
    private static Dictionary<string, int> dic_level = new Dictionary<string, int>();
    private bool isFirstGenerate = false;
    private bool select_All = false;
    private Vector2 scroll;
    private Vector2 windowScroll;
    private string result = string.Empty;
    private string pos = string.Empty;

    private enum EventType
    {
        onPointerClick = 1,
        onPointerDown = 2,
        onPointerUp = 3,
    }


    [MenuItem("Window/Auto Get(Tree version)/Run")]
    static void Init()
    {
        UIToolsTree window = (UIToolsTree)EditorWindow.GetWindow(typeof(UIToolsTree));
        window.maxSize = new Vector2(600, 800);
        window.Show();
    }

    [MenuItem("Window/Auto Get(Tree version)/Reset")]
    static void Reset_IsPaint()
    {
        obj = null;
        dic_bool.Clear();
        dic_level.Clear();
        dic_trans.Clear();
    }

    private void OnGUI()
    {
        windowScroll = EditorGUILayout.BeginScrollView(windowScroll);

        #region BeginVertical
        EditorGUILayout.BeginVertical(GUILayout.MaxHeight(1000f));
        EditorGUILayout.LabelField("Please chose an UIPanel (For generate Component Code)", EditorStyles.boldLabel);
        obj = EditorGUILayout.ObjectField(obj, typeof(Transform), true) as Transform;
        EditorGUILayout.LabelField("");

        GUI.Label(new Rect(10, 42, 100, 20), "Tree");
        GUI.Label(new Rect(280, 42, 100, 20), "IsDraw");
        GUI.Label(new Rect(330, 42, 100, 20), "OPC");
        GUI.Label(new Rect(380, 42, 100, 20), "OPD");
        GUI.Label(new Rect(430, 42, 100, 20), "OPU");
        if (obj != null)
        {

            int count = 0;
            foreach (var v in dic_level)
            {
                
                #region
                EditorGUILayout.BeginHorizontal();

                Transform forCheck = FindTransformFromPanelRoot(v.Key, obj);
                if (v.Key == obj.name || dic_forFoldout[forCheck.parent.name])//|| dic_bool[forCheck.parent.name][0])
                {
                    count += 1;
                    EditorGUI.indentLevel = v.Value;
                    if (dic_forFoldout.ContainsKey(v.Key))
                    {

                    }
                    else
                    {
                        dic_forFoldout.Add(v.Key, true);
                    }
                    dic_forFoldout[v.Key] = EditorGUILayout.Foldout(dic_forFoldout[v.Key], v.Key);

                    for (int i = 1; i < 4; i++)
                    {
                        if (dic_bool[v.Key].Count <= i)
                        {
                            dic_bool[v.Key].Add(false);
                        }
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        EditorGUILayout.Space();
                    }

                    dic_bool[v.Key][0] = GUI.Toggle(new Rect(270 + 20, 40 + count * 16f, 20, 20), dic_bool[v.Key][0], "");
                    dic_bool[v.Key][1] = GUI.Toggle(new Rect(270 + 70, 40 + count * 16f, 20, 20), dic_bool[v.Key][1], "");
                    dic_bool[v.Key][2] = GUI.Toggle(new Rect(270 + 120, 40 + count * 16f, 20, 20), dic_bool[v.Key][2], "");
                    dic_bool[v.Key][3] = GUI.Toggle(new Rect(270 + 170, 40 + count * 16f, 20, 20), dic_bool[v.Key][3], "");

                    //TODO 父节点不勾选，其子节点全部不勾选
                    if (!dic_bool[v.Key][0])
                    {

                        Transform trans = FindTransformFromPanelRoot(v.Key, obj);

                        Component[] childNode = trans.GetComponentsInChildren(typeof(Transform), true);

                        if (childNode.Length > 0)
                        {
                            for (int i = 0; i < childNode.Length; i++)
                            {
                                dic_bool[childNode[i].name][0] = dic_bool[v.Key][0];
                            }
                        }
                    }
                    else       //子节点被勾选，父节点也应该被勾选
                    {
                        Transform trans = FindTransformFromPanelRoot(v.Key, obj);

                        if (v.Value > 0)
                        {
                            Transform temp = null;
                            for (int i = 0; i < v.Value; i++)
                            {
                                if (temp == null)
                                {
                                    temp = trans.parent;
                                }
                                else
                                {
                                    temp = temp.parent;
                                }
                                dic_bool[temp.name][0] = dic_bool[v.Key][0];
                            }
                        }
                    }
                }
                else if(!dic_forFoldout[forCheck.parent.name])
                {
                    dic_forFoldout[v.Key] = false;
                }



                EditorGUILayout.EndHorizontal();
                #endregion
            }
        }

        EditorGUILayout.LabelField("");

        #region BeginHorizontal
        EditorGUILayout.BeginHorizontal();
        EditorGUI.indentLevel = 0;
        isFirstGenerate = EditorGUILayout.Toggle("First Generate", isFirstGenerate);

        EditorGUI.indentLevel = 0;
        select_All = EditorGUILayout.Toggle("Select All", select_All);
        if (select_All)
        {
            foreach(var v in dic_bool)
            {
                v.Value[0] = true;
            }
        }

        if(GUILayout.Button("Switch Selected"))
        {
            select_All = false;
            foreach (var v in dic_bool)
            {
                v.Value[0] = !v.Value[0];
            }
        }

        EditorGUILayout.EndHorizontal();
        #endregion

        #region BeginHorizontal
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate"))
        {
            result = LoadComponentAndGetString();
        }

        if (GUILayout.Button("Generate UGUI_Event template code"))
        {
            result = Get_UGUIEvent_Template();
        }

        EditorGUILayout.EndHorizontal();
        #endregion
        //EditorGUILayout.EndVertical();
        //#endregion


        //#region BeginVertical
        //EditorGUILayout.BeginVertical(GUILayout.MaxHeight(300));

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Please select a lua file (For generate template code)", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;
        st = EditorGUILayout.ObjectField(st, typeof(Object), true) as Object;

        //EditorGUILayout.LabelField("读取到的Component");
        GUILayout.Label("Generate Result", EditorStyles.boldLabel);

        #region BeginScrollView
        EditorGUI.indentLevel = 0;
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUI.indentLevel = 0;
        result = EditorGUILayout.TextArea(result, GUILayout.MaxHeight(700f), GUILayout.MinHeight(700f));
        EditorGUILayout.EndScrollView();
        #endregion

        #region BeginHorizontal
        EditorGUILayout.BeginHorizontal();

        EditorGUI.indentLevel = 0;
        EditorGUILayout.TextArea(pos);
        if (GUILayout.Button("Get Position"))
        {
            if (obj != null)
            {
                pos = string.Format("LocalPosition:\nx:{0:0.0}\ty:{1:0.0}\tz:{2:0.0}\nPosition:\nx:{3:0.0}\ty:{4:0.0}\tz:{5:0.0}",
                    obj.localPosition.x, obj.localPosition.y, obj.localPosition.z,
                    obj.position.x, obj.position.y, obj.position.z);
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        #region BeginHorizontal
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load"))
        {
            dic_trans.Clear();
            dic_bool.Clear();
            dic_level.Clear();
            dic_forFoldout.Clear();
            GetChild(obj, obj);
        }

        if (GUILayout.Button("paste to Clipboard"))
        {
            if (string.IsNullOrEmpty(result))
            {
                Debug.Log("Please click \"Load\" button first !");
            }
            else
            {
                TextEditor copyText = new TextEditor();
                copyText.text = result;
                copyText.SelectAll();
                copyText.Copy();
            }
        }

        if (GUILayout.Button("Get lua template code"))
        {
            if (st != null)
            {
                result = GetTemplateLuaCodeByName(st.name);
            }
            else
            {
                Debug.LogError("Please select a lua file ! ");
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        EditorGUILayout.EndVertical();
        #endregion
        EditorGUILayout.EndScrollView();
    }

    private string GetTemplateLuaCodeByName(string luaFileName)
    {
        StringBuilder str = new StringBuilder();
        //require
        string require = "require \"System.Global\"\nrequire \"Logic.Tool.UITools\"\n";

        //class
        string luaClass = string.Format("\nclass (\"{0}\")\n", luaFileName);

        //Awake
        string awake = string.Format("\nfunction {0}:Awake(this)\n\tself.this = this\nend\n ", luaFileName);

        //Start
        string start = string.Format("\nfunction {0}:Start()\n\nend\n ", luaFileName);

        //OnDestroy
        string onDestroy = string.Format("\nfunction {0}:OnDestroy()\n\tself.this = nil\n\tself = nil\nend\n ", luaFileName);

        str.Append(require);
        str.Append(luaClass);
        str.Append(awake);
        str.Append(start);
        str.Append(onDestroy);

        return str.ToString();
    }

    private void GetChild(Transform trans, Transform rootTrans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            if (trans.GetChild(i).name == "Controller")
            {
                List<bool> bool_node = new List<bool>();
                bool_node.Add(false);
                dic_bool.Add(trans.name, bool_node);
                dic_level.Add(trans.name, GetDepth(trans, rootTrans, 0));
                GetChild(trans.GetChild(i), rootTrans);
                return;
            }
        }

        if (trans.name == "Controller")      //Controller是唯一名称
        {
            //该节点是父节点
            List<Transform> rootNode = new List<Transform>();
            rootNode.Add(trans);
            dic_trans.Add("root", rootNode);
            List<bool> bool_node = new List<bool>();
            bool_node.Add(false);
            dic_bool.Add("Controller", bool_node);
            dic_level.Add("Controller", GetDepth(trans, rootTrans, 0));
        }
        else if (trans.name == obj.name)
        {
            List<Transform> rootNode = new List<Transform>();
            rootNode.Add(trans);
            dic_trans.Add(trans.parent.name, rootNode);
            List<bool> bool_node = new List<bool>();
            bool_node.Add(false);
            dic_bool.Add(trans.name, bool_node);
            dic_level.Add(trans.name, GetDepth(trans, rootTrans, 0));
        }

        if (trans.childCount > 0)
        {
            List<Transform> sonNode = new List<Transform>();
            for (int i = 0; i <= trans.childCount - 1; i++)
            {
                sonNode.Add(trans.GetChild(i));
                List<bool> bool_node = new List<bool>();
                bool_node.Add(false);
                dic_bool.Add(trans.GetChild(i).name, bool_node);
                dic_level.Add(trans.GetChild(i).name, GetDepth(trans.GetChild(i), rootTrans, 0));
                if (trans.GetChild(i).childCount > 0)
                {
                    GetChild(trans.GetChild(i), rootTrans);
                }
            }

            if (dic_trans.ContainsKey(trans.name))
            {
                Debug.LogError(string.Format("The name : \"{0}\" is already have, please make a different name", trans.name));
            }

            dic_trans.Add(trans.name, sonNode);
        }
    }

    private string LoadComponentAndGetString()
    {
        StringBuilder str = new StringBuilder();
        ASCIIEncoding asc = new ASCIIEncoding();
        string temp = string.Empty;


        foreach (var v in dic_trans)
        {
            for (int i = 0; i < v.Value.Count; i++)
            {
                temp = string.Empty;
                if (v.Key == "root")
                {
                    if (dic_bool["Controller"][0] || isFirstGenerate)
                    {
                        temp = "self.root = self.this.transform\n";
                    }
                }
                else if (v.Key == "Controller")
                {
                    if (dic_bool[v.Value[i].name][0] || isFirstGenerate)
                    {
                        temp = string.Format("self.{0} = self.root:Find(\"{1}\")\n", GetFirstLowerStr(v.Value[i].name), v.Value[i].name);
                    }
                }
                else
                {
                    if (dic_bool[v.Value[i].name][0] || isFirstGenerate)
                    {
                        temp = string.Format("self.{0} = self.{1}:Find(\"{2}\")\n", GetFirstLowerStr(v.Value[i].name), GetFirstLowerStr(v.Value[i].parent.name), v.Value[i].name);
                    }
                }

                str.Append(temp);
            }
        }

        return str.ToString();
    }

    private string Get_UGUIEvent_Template()
    {
        StringBuilder str = new StringBuilder();
        string temp = string.Empty;
        string subTemp = string.Empty;
        str.Append("local listener");
        foreach (var v in dic_bool)
        {
            for (int i = 1; i < v.Value.Count; i++)
            {
                temp = string.Empty;
                if (v.Value[i])
                {
                    subTemp = string.Format("\nlistener = EventTriggerProxy.Get(self.{0}.gameObject)\n", GetFirstLowerStr(v.Key));
                    temp += subTemp;
                    subTemp = string.Format("\tlocal callback_{0} = function(self, e)\n\t\t\n\tend\n", v.Key);
                    temp += subTemp;
                    subTemp = string.Format("listener.{0} = EventTriggerProxy.PointerEventDelegate(callback_{1}, self)\n", System.Enum.GetName(typeof(EventType), i).ToString(), v.Key);
                    temp += subTemp;
                    str.Append(temp);
                }
            }
        }
        return str.ToString();
    }


    #region 工具的工具
    private string GetFirstLowerStr(string str)
    {
        ASCIIEncoding asc = new ASCIIEncoding();
        string forLower = string.Empty;
        if (!string.IsNullOrEmpty(str))
        {
            int ascNum = (int)asc.GetBytes(str[0].ToString())[0];
            if (ascNum < 58)
            {
                forLower = str;
            }
            else
            {
                forLower = char.ToLower(str[0]) + str.Substring(1);
            }

        }

        return forLower;
    }

    private int GetDepth(Transform trans, Transform rootTrans, int currentDepth)
    {
        if (trans.name == rootTrans.name)
        {
            return 0;
        }
        else if (trans.parent.name == rootTrans.name)
        {
            currentDepth += 1;
            return currentDepth;
        }
        else
        {
            currentDepth += 1;
            return GetDepth(trans.parent, rootTrans, currentDepth);
        }
    }

    private string DeleteBlock(string str)
    {
        string result = str;
        result = result.Replace(" ", "");
        return result;
    }

    private Transform FindTransformFromPanelRoot(string transformName, Transform root)
    {
        Component[] temp = root.GetComponentsInChildren(typeof(Transform), true);
        for (int i = 1; i <= temp.Length; i++)
        {

            if (temp[i - 1].name == transformName)
            {
                return temp[i - 1] as Transform;
            }
        }

        if(transformName == root.name)
        {
            return root;
        }

        if(temp.Length == 0)
        {

        }

        return null;
    }

    #endregion

}

