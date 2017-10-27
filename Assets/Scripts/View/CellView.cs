using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public virtual void SetView(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
        var sprite = GetComponent<UISprite>();
        if (sprite != null)
        {
            sprite.enabled = true; 
        }
    }

    public virtual void RemoveView()
    {
        Destroy(gameObject); //temporary solution
    }

    public virtual new bool Equals(object other)
    {
        return other is CellView ? true : false;
    }

}
