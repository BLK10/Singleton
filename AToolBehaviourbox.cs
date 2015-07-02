using System;

using UnityEngine;


namespace BLK10.Singleton
{
    public abstract class AToolBehaviourbox<T> : MonoBehaviour where T : MonoBehaviour
    {        
        private static T kInstance = null;
        
        private static readonly object kSync = new object();
        

        public virtual void OnApplicationQuit()
        {
            //
        }


        static AToolBehaviourbox() { }

        protected static T UnderlyingInstance
        {
            get
            {
                if (kInstance == null)
                {
                    lock (kSync)
                    {
                        kInstance = MonoBehaviour.FindObjectOfType(typeof(T)) as T;

                        if (kInstance == null)
                        {
                            string goName = typeof(T).Name;
                            GameObject go = AToolBehaviourbox<T>.FindGameObjectByName(goName);

                            if (go != null)
                            {
#if UNITY_EDITOR
                            go.isStatic = true;
#endif
                                UnityEngine.Object.DontDestroyOnLoad(go);
                                kInstance = go.GetComponent<T>();

                                if (kInstance != null)
                                {
                                    go.name = goName;
                                    return (kInstance);
                                }
                            }
                            else
                            {
                                go = new GameObject(goName);
#if UNITY_EDITOR
                            go.isStatic = true;
#endif
                                UnityEngine.Object.DontDestroyOnLoad(go);
                            }

                            kInstance = go.AddComponent<T>();
                        }
                    }
                }

                return (kInstance);
            }          
        }
        
        /// <summary>Find the GameObject with the corresponding name even deactivated GameObject.</summary>
        /// <param name="name">Name of the GameObject to find.</param>
        /// <returns>The first GameObject with the corresponding name.</returns>
        private static GameObject FindGameObjectByName(string name)
        {
            if (name.IsNullOrWhitespace())
            {
                throw new ArgumentException("string could not be null or empty.", "name");
            }

            GameObject[] objs = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

            foreach (GameObject o in objs)
            {
                if ((o != null) && (o.name == name))
                {
                    return (o);
                }
            }

            return (null);
        }
    }
}
