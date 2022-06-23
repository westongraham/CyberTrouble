using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotateScript : MonoBehaviour
{
    public Transform projectile;

    void FixedUpdate()
    {
        projectile.transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
