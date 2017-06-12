/// <summary>
/// FileName:    EditorPrefsInterface
/// Author:      JackalLiu
/// CreateTime:  4/10/2017 5:06:42 PM
/// Description: editor prefs quick operations
/// </summary>

using UnityEditor;

public class EditorInt
{
	public readonly string propertyName;

	private bool initFlag = false;
	private int storeValue = 0;

	public EditorInt(string propertyName)
	{
		this.propertyName = propertyName;
	}
		
	public int Value
	{
		get
		{
			if (!initFlag) 
			{
				storeValue = EditorPrefs.GetInt (propertyName);
				initFlag = true;
			}
			return storeValue;
		}

		set
		{
			if (storeValue != value) 
			{
				storeValue = value;
				EditorPrefs.SetInt (propertyName, storeValue);
			}
		}
	}
}

public class EditorString
{
	public readonly string propertyName;

	private bool initFlag = false;
	private string storeValue = string.Empty;

	public EditorString(string propertyName)
	{
		this.propertyName = propertyName;
	}

	public string Value
	{
		get
		{
			if (!initFlag) 
			{
				storeValue = EditorPrefs.GetString (propertyName);
				initFlag = true;
			}
			return storeValue;

		}

		set
		{
			if (storeValue != value) 
			{
				storeValue = value;
				EditorPrefs.SetString (propertyName, storeValue);
			}
		}
	}
}

public class EditorBool
{
	public readonly string propertyName;

	private bool initFlag = false;
	private bool storeValue = false;

	public EditorBool(string propertyName)
	{
		this.propertyName = propertyName;
	}

	public bool Value
	{
		get
		{
			if (!initFlag) 
			{
				storeValue = EditorPrefs.GetBool (propertyName);
				initFlag = true;
			}
			return storeValue;
		}

		set
		{
			if (storeValue != value) 
			{
				storeValue = value;
				EditorPrefs.SetBool (propertyName, storeValue);
			}
		}
	}
}

public class EditorFloat
{
	public readonly string propertyName;

	private bool initFlag = false;
	private float storeValue = 0f;

	public EditorFloat(string propertyName)
	{
		this.propertyName = propertyName;
	}

	public float Value
	{
		get
		{
			if (!initFlag) 
			{
				storeValue = EditorPrefs.GetFloat (propertyName);
				initFlag = true;
			}
			return storeValue;
		}

		set
		{
			if (storeValue != value) 
			{
				storeValue = value;
				EditorPrefs.SetFloat (propertyName, storeValue);
			}
		}
	}
}