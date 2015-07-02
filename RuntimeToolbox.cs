using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLK10.Singleton
{    
    [DisallowMultipleComponent]
    public class RuntimeToolbox : AToolBehaviourbox<RuntimeToolbox>, IToolbox
    {
        private static readonly StringComparer kComparer = StringComparer.InvariantCulture;  
      
        [HideInInspector]
        private Dictionary<string, Component> _cache;


        protected RuntimeToolbox() {}

        public override void OnApplicationQuit()
        {            
            UnityEngine.Object.Destroy(this.gameObject, 1f);
        }

        public new HideFlags hideFlags
        {
            get { return (this.gameObject.hideFlags); }
            set { this.gameObject.hideFlags = value; }
        }

        public T GetOrAddComponent<T>()
            where T : Component
        {            
            var fullName = typeof(T).FullName;
            this._cache  = this._cache ?? new Dictionary<string, Component>(RuntimeToolbox.kComparer);

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
            get { return (RuntimeToolbox.UnderlyingInstance as IToolbox); }
        }


        #region "INIT"

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            IToolbox rtool = RuntimeToolbox.Instance;       
            if ((rtool != null) && (((RuntimeToolbox)rtool)._cache == null))
                ((RuntimeToolbox)rtool)._cache = new Dictionary<string, Component>(RuntimeToolbox.kComparer);
        }

        #endregion
    }
}
