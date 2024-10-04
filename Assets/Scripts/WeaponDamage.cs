using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    [SerializeField] private Collider myCollider;
    private int attackDamage;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Step preventitive checks which return early and apply no damage
        if (other == myCollider) return;

        if (alreadyCollidedWith.Contains(other)) return;

        //Steps to actually apply damage
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealtDamage(attackDamage);
        }
        

    }

    public void SetAttack(int damage)
    {
        attackDamage = damage;
    }
}
