using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections;
using System.Collections.Generic;
public class UITools : EditorWindow {

    Object st;
    public Transform obj;
    private string luaFileName = string.Empty;
    private string result = string.Empty;
    private string pos = string.Empty;
    private Vector2 scroll;
    Dictionary<string, List<Transform>> dic = new Dictionary<string, List<Transform>>();

    [MenuItem("Window/Auto Get")]
    static void Init()
    {
        UITools window = (UITools)EditorWindow.GetWindow(typeof(UITools));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Please chose an UIPanel (For generate Component Code)", EditorStyles.boldLabel);
        obj = EditorGUILayout.ObjectField(obj, typeof(Transform), true) as Transform;

        EditorGUILayout.LabelField("Please select a lua file (For generate template code)", EditorStyles.boldLabel);
        st = EditorGUILayout.ObjectField(st, typeof(Object), true) as Object;

        //EditorGUILayout.LabelField("读取到的Component");
        GUILayout.Label("Loaded Component", EditorStyles.boldLabel);

        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.TextArea(result);
        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextArea(pos);
        if (GUILayout.Button("Get Position"))
        {
            pos = string.Format("LocalPosition:\nx:{0:0.0}\ty:{1:0.0}\tz:{2:0.0}\nPosition:\nx:{3:0.0}\ty:{4:0.0}\tz:{5:0.0}",
                                obj.localPosition.x, obj.localPosition.y, obj.localPosition.z,
                                obj.position.x, obj.position.y, obj.position.z);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Load"))
        {
            dic.Clear();
            //ASCIIEncoding asc = new ASCIIEncoding();
            //string a = string.Format("abc\"{0}\"", "111");
            //Debug.Log("Load Component " + a);
            if(obj == null)
            {
                Debug.LogError("Obj is Null !");
            }
            else
            {
                result = LoadComponentAndGetString(obj);
                //Debug.Log(result);
                //Debug.Log(obj.GetChild(0).name);
            }
        }

        if(GUILayout.Button("paste to Clipboard"))
        {
            if(string.IsNullOrEmpty(result))
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

        if(GUILayout.Button("Get lua template code"))
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
        EditorGUILayout.EndVertical();
    }

    private string LoadComponentAndGetString(Transform trans)
    {
        StringBuilder str = new StringBuilder();
        GetChild(trans);

        ASCIIEncoding asc = new ASCIIEncoding();
        string temp = string.Empty;
        string forLower = string.Empty;

        
        foreach(var v in dic)
        {
            forLower = string.Empty;
            forLower = GetFirstLowerStr(v.Key);
            for (int i = 0; i < v.Value.Count; i++)
            {
                temp = string.Empty;
                if (forLower == "root")
                {
                    temp = "self.root = self.this.transform\n";
                }
                else if (forLower == "controller")
                {
                    temp = string.Format("self.{0}_{1} = self.root:Find(\"{2}\")\n", forLower, v.Value[i].name, v.Value[i].name);
                }
                else
                {
                    temp = string.Format("self.{0}_{1} = self.{2}_{3}:Find(\"{4}\")\n", forLower, v.Value[i].name, GetFirstLowerStr(v.Value[i].parent.parent.name), v.Key, v.Value[i].name);
                }

                str.Insert(0, temp);
            }
        }

        //Debug.Log(str.ToString());

        return str.ToString();
    }

    private void GetChild(Transform trans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            if (trans.GetChild(i).name == "Controller")
            {
                GetChild(trans.GetChild(i));
                return;
            }
        }


        if (trans.childCount > 0)
        {
            List<Transform> sonNode = new List<Transform>();
            for (int i = 0; i <= trans.childCount - 1; i++)
            {
                if (trans.GetChild(i).childCount > 0)
                {
                    GetChild(trans.GetChild(i));
                }
                sonNode.Add(trans.GetChild(i));
            }
            dic.Add(trans.name, sonNode);
        }

        if (trans.name == "Controller")      //Controller是唯一名称
        {
                //该节点是父节点
            List<Transform> rootNode = new List<Transform>();
            rootNode.Add(trans);
            dic.Add("root", rootNode);
        }
        else if (trans.name == obj.name)
        {
            List<Transform> rootNode = new List<Transform>();
            rootNode.Add(trans);
            dic.Add(trans.parent.name, rootNode);
        }
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

    #endregion

}
