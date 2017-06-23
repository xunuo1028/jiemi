using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSC : MonoBehaviour {

    public Button abc;
    public Transform bcd;

	// Use this for initialization
	void Start () {
        abc.onClick.RemoveAllListeners();
	}   
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Test()
    {
        Debug.Log("Test" + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
    }
}
