using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    public event EventHandler<OnSpacePressEventArgs> OnSpacePressed;
    // Start is called before the first frame update
    public class OnSpacePressEventArgs : EventArgs
    {
        public int spaceCount = 0;
    }

    private int spaceCount = 0;

    public delegate void OnSpaceFloat(float f);
    public event OnSpaceFloat onSpaceFloatEvent;

    public event Action<bool, int> OnEventAction;

    public UnityEvent OnUnityEvent;

    void Start()
    {
        OnSpacePressed += Testing_OnPressSpace;
        onSpaceFloatEvent += FloatSubscriber;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressEventArgs { spaceCount = spaceCount });
            onSpaceFloatEvent?.Invoke(5f);
            OnEventAction?.Invoke(true, 55);
            OnUnityEvent?.Invoke();
        }
    }

    private void Testing_OnPressSpace(object sender, OnSpacePressEventArgs e)
    {
        Debug.Log("Space c="+e.spaceCount);
    }

    public void FloatSubscriber(float f)
    {
        Debug.Log("float = " + f);
    }
}
