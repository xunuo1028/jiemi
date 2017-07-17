using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSC : MonoBehaviour {

    public ScrollRect abc;
    public Transform bcd;
    public ContentSizeFitter sss;
    public Scrollbar ddd;
    public GridLayoutGroup asd;
    public Canvas fff;

	// Use this for initialization
	void Start () {
        abc.vertical = false;
        sss.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        asd.startAxis = GridLayoutGroup.Axis.Horizontal;
        Handheld.Vibrate();
    }   
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Test()
    {
        Debug.Log("Test" + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
    }
}
