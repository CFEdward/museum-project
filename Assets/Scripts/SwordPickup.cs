using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour, ICollectible
{
    public static event Action OnSwordPickedUp;

    public Sword swordScript;
    public Animator swordAnimator;
    public Transform player, weaponContainer;

    public void Collect()
    {
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        swordScript.enabled = true;
        swordAnimator.enabled = true;

        Debug.Log("Sword collected.");
        OnSwordPickedUp?.Invoke();
    }
}
