using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// locate registered classes that might be referenced in another gameobject
public class LocatorBase<T> : MonoBehaviour
{
    [SerializeField] protected T[] m_locations;

    public A Locate<A>()
        where A: class
    {
        T located = m_locations.First((check)=> { return (check.GetType() == typeof(A)); });
        if (located == null)
            return located as A;
        return default;
    }

    public static A Locate<A>(GameObject a_go) where A: class
    {
        A located = null;
        located = a_go.GetComponent<A>();
        if (located != null)
            return located;
        LocatorBase<T> locator =  a_go.GetComponent<LocatorBase<T>>();
        if (locator == null)
            return located;
        located = locator.Locate<A>();
        return located;
    }
}
