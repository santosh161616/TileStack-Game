using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (transform.position.y < -5f)
            Destroy(gameObject);
    }
}
