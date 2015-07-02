using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BLK10.Singleton
{
    public interface IToolbox
    {

        string     name       { get; }
        GameObject gameObject { get; }
        HideFlags  hideFlags  { get; set; }

        T GetOrAddComponent<T>()
            where T : Component;

        T GetComponent<T>();

        Coroutine StartCoroutine(IEnumerator routine);
        void      StopCoroutine(IEnumerator routine);
        void      StopAllCoroutines();
 
    }
}
