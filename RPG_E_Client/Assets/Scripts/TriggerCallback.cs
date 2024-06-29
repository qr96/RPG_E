using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallback : MonoBehaviour
{
    Action<Collider> onEnter;
    Action<Collider> onStay;
    Action<Collider> onExit;

    private void OnTriggerEnter(Collider other)
    {
        onEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onExit?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        onStay?.Invoke(other);
    }

    public void SetTrigger(Action<Collider> onEnter, Action<Collider> onStay, Action<Collider> onExit)
    {
        this.onEnter = onEnter;
        this.onStay = onStay;
        this.onExit = onExit;
    }
}
