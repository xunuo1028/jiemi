using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSC : MonoBehaviour {

    public RectTransform abc;
    public Transform bcd;

	// Use this for initialization
	void Start () {

	}   
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test()
    {
        Debug.Log("Test" + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
    }
}
