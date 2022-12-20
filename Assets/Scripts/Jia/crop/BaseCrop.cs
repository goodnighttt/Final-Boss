using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCrop : MonoBehaviour
{

    public abstract int lifeTime { get; }

    private void OnCollisionEnter(Collision collision)
    {
       
    }


}
