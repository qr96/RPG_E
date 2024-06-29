using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRectSizeCallback : MonoBehaviour
{
    Action onCanvasDimensionsChange;

    private void OnRectTransformDimensionsChange()
    {
        onCanvasDimensionsChange?.Invoke();
    }

    public void SetDimensionsChangeCallback(Action onCanvasDimensionsChange)
    {
        this.onCanvasDimensionsChange = onCanvasDimensionsChange;
    }
}
