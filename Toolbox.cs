using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLK10.Singleton
{   
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class Toolbox : AToolBehaviourbox<Toolbox>, IToolbox
    {
        private static readonly StringComparer kComparer = StringComparer.InvariantCulture;
        
        [NonSerialized, HideInInspector]
        private Dictionary<string, Component> _cache;        

        protected Toolbox() {}
        
        
        public new HideFlags hideFlags
        {
            get { return (this.gameObject.hideFlags); }
            set { this.gameObject.hideFlags  = value; }
        }

        public T GetOrAddComponent<T>()
            where T : Component
        {            
            var fullName = typeof(T).FullName;
            this._cache  = this._cache ?? new Dictionary<string, Component>(Toolbox.kComparer);
            
            Component comp;
            if (!this._cache.TryGetValue(fullName, out comp))
            {
                comp = this.GetComponent<T>();

                if (comp == null)
                    comp = this.gameObject.AddComponent<T>();

                this._cache.Add(fullName, comp);
            }

            return (comp as T);
        }
        
        public static IToolbox Instance
        {
            get { return (Toolbox.UnderlyingInstance as IToolbox); }
        }
        

        #region "INIT"

        [RuntimeInitializeOnLoadMethod]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        private static void Initialize()
        {
            IToolbox tool = Toolbox.Instance;
            if ((tool != null) && (((Toolbox)tool)._cache == null))
                ((Toolbox)tool)._cache = new Dictionary<string, Component>(Toolbox.kComparer);            
        }

        #endregion

    }
}
