using UnityEngine;

public class Destory : MonoBehaviour
{
    public float Lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }
}