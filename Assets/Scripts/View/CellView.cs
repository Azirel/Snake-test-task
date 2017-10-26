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
        Destroy(this); //temporary solution
    }

    public virtual int CompareTo(object obj)
    {
        if (obj is CellView == false)
        {
            throw new ArgumentException("Error in CellView comparing: obj is not CellView");
        }
        return state - ((CellView)obj).State;
    }

    //public new virtual bool Equals(object other)
    //{
    //    if (other is CellView == true)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //public static bool operator == (CellView a, CellView b)
    //{
    //    return System.Object.ReferenceEquals(a, b);
    //}

    //public static bool operator !=(CellView a, CellView b)
    //{
    //    return System.Object.ReferenceEquals(a, b);
    //}

}
