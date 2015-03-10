using UnityEngine;
using System.Collections;
using UnityGameBase;

public class Test : MonoBehaviour {

    TouchInformation touch;

	void Start () {
        UGB.Input.TouchStart += Input_TouchStart;
	}

    private void Input_TouchStart(TouchInformation touchInfo)
    {
        Debug.Log("Touch was started");
        touch = touchInfo;
    }

    void Update()
    {
        if(touch != null)
        {
            Debug.Log("Touch active");

            if(touch.IsDead)
            {
                Debug.Log("Touch died");
                touch = null;
            }
    }
}
