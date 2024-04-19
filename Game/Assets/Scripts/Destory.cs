using UnityEngine;

public class Destory : MonoBehaviour
{
    public float Lifetime = 5f;

    
    /// <summary>
    /// destroy the object after a certain amount of time
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }
}