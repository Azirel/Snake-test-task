using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IComparable
{
    [SerializeField]
    string cellViewTypeName;
    public string CellViewTypeName { get { return cellViewTypeName; } }

    [SerializeField]
    CellViewState state;
    public CellViewState State { get { return state; } }

    [SerializeField]
    UISprite sprite;

    public virtual void SetView(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
        sprite.enabled = true;
    }

    public virtual void RemoveView()
    {
        Destroy(gameObject); //temporary solution
    }

    public virtual int CompareTo(object obj)
    {
        if (obj is CellView == false)
        {
            throw new ArgumentException("Error in CellView comparing: obj is not CellView");
        }
        return state - ((CellView)obj).State;
    }

    public virtual new bool Equals(object other)
    {
        return other is CellView ? true : false;
    }

}
