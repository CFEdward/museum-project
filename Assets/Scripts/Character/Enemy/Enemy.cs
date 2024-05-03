using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AttributesManager attributes;

    private void Update()
    {
        Die();
    }

    private void Die()
    {
        if (attributes != null && attributes.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
