/// <summary>
/// A singleton is a class that should only have one instance existing at a time.
/// This implementation of the singleton pattern creates a singleton lazily.
/// </summary>
public class Singleton
{
    #region Singleton
    private static Singleton singleton = null; // The singleton stored in memory.

    /// <summary>
    /// Returns the instance of the singleton, or initializes a new instance of the class lazily.
    /// </summary>
    public static Singleton Instance
    {
        get
        {
            if(singleton != null)
            {
                return singleton;
            }
            else
            {
                singleton = new Singleton();
                return singleton;
            }
        }
    }
    #endregion

    #region Hacks
    /// <summary>
    /// Explicit static constructor forces complier to not mark type as BeforeFieldInit.
    /// </summary>
    static Singleton()
    {

    }
    
    /// <summary>
    /// Forces a parameterless constructor for any subclasses
    /// </summary>
    public Singleton()
    {

    }
    #endregion
}
