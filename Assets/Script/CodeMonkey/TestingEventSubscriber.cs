using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestingEventSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventTest eventTest = GetComponent<EventTest>();
        eventTest.OnSpacePressed += EventTestSubscriber;
        eventTest.OnEventAction += EventTest_OnEventAction;
    
    }

    public void OnUnityDebug()
    {
        Debug.Log("Unity Event");
    }

    private void EventTest_OnEventAction(bool arg1, int arg2)
    {
        Debug.Log(arg1 + " xxx " + arg2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventTestSubscriber(object sender, EventArgs e)
    {
        Debug.Log("Space from another class");
        EventTest eventTest = GetComponent<EventTest>();
        eventTest.OnSpacePressed -= EventTestSubscriber;
    }
}
