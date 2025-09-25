using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Enemy0 : MonoBehaviour
{
    [Header("Enemy Health")]
    public int maxHealth = 100;
    private int currentHealth;

    // Ölüm veya hedefe ulaşma durumunda spawner'a bildirim
    public Action OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Bu fonksiyon düşman hasar aldığında çağrılır.
    /// </summary>
    /// <param name="damage">Hasar miktarı</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Düşman hedefe (ana kapıya) ulaştığında çağrılır.
    /// </summary>
    public void ReachGoal()
    {
        Debug.Log("Enemy0 reached the goal.");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    /// <summary>
    /// Düşman öldüğünde çağrılır.
    /// </summary>
    private void Die()
    {
        Debug.Log("Enemy0 died.");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
