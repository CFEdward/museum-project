using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool canAttack = true;
    private bool isAttacking = false;
    public float attackCooldown = 1f;
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack1")) isAttacking = true;
        else isAttacking = false;
    }

    public void SwordAttack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && isAttacking)
        {
            Debug.Log(other.name);
            GetComponentInParent<AttributesManager>(true).DealDamage(other.gameObject);
        }
    }
}
