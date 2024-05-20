using System;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public event EventHandler<OnHitHealthChangedEventArgs> OnHit;
    public class OnHitHealthChangedEventArgs : EventArgs
    {
        public float healthNormalized;
    }

    public int healthMax;
    public int health;
    public int attack;

    public void Awake()
    {
        healthMax = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnHit?.Invoke(this, new OnHitHealthChangedEventArgs
        {
            healthNormalized = (float)health / (float)healthMax,
        });
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }
}
