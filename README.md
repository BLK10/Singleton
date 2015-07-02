from [http://github.com/BLK10/Singleton](http://github.com/BLK10/Singleton)

# **Singleton Toolbox** #

[Aggregating singletons: the Toolbox](http://www.ibm.com/developerworks/library/co-single/)



A Toolbox component  

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toolbox))]
    public class ToolComponent : MonoBehaviour, IToolComponent
    {
    
    
        public static IToolComponent Instance
        {
            get
            {
                IToolbox tool = Toolbox.Instance;
                if (tool != null)
                    return (tool.GetOrAddComponent<ToolComponent>() as IToolComponent);

                throw new NullReferenceException("Singleton \"Toolbox\" could not be null.");
            }
        }
    }
    



A RuntimeToolbox component  

    [DisallowMultipleComponent]
    [RequireComponent(typeof(RuntimeToolbox))]
    public class RuntimeToolComponent : MonoBehaviour, IRuntimeToolComponent
    {
    
    
        public static IRuntimeToolComponent Instance
        {
            get
            {
                IToolbox rtool = RuntimeToolbox.Instance;

                if (rtool != null)
                    return (rtool.GetOrAddComponent<RuntimeToolComponent>() as IRuntimeToolComponent);
                
                throw new NullReferenceException("Singleton \"RuntimeToolbox\" could not be null.");                
            }
        }
    }
