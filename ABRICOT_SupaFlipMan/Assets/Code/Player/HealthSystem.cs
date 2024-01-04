using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    public Image healthbarImage;
    public Text levelTex;
    [SerializeField] float maxHealth = 100.0f;
    [SerializeField] float actualHealth = 100.0f;
    void Start()
    {
        UpdateHealthbar();
    }


    void UpdateHealthbar()
    {
        float ratio = actualHealth / maxHealth;
        healthbarImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
        levelTex.text = (ratio * 100).ToString("0") + "%";
    }

    void TakeDamage(float damage)
    {
        actualHealth -= damage;
        if(actualHealth <= 0.0f)
            actualHealth = 0.0f;

        UpdateHealthbar();
    }


    void HealDamage(float damage)
    {
        actualHealth += damage;
        if(actualHealth >= maxHealth)
            actualHealth = maxHealth;

        UpdateHealthbar();
    }
}
