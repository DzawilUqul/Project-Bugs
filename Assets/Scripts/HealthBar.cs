using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider barHP;
    [SerializeField] private float maxHP=100;
    private float currentHP;
    private void Start()
    {
        currentHP = maxHP;
        this.UpdateBar();
    }
    private void UpdateBar()
    {
        barHP.value = (float) currentHP / maxHP;
    }
    public float Hurt(int damage)
    {
        currentHP -= damage;
        this.UpdateBar();
        DamagePopup damagePopup = DamagePopup.Create(transform.position);
        damagePopup.Setup(damage);
        return currentHP;
    }
    
}
