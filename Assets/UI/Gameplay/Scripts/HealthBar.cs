using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private Player_FightSystem _playerHealth;



    private void Start()
    {
        SetMaxHealth(100);
    }



    public void HealthBar_Update()
    {
        SetHealth(_playerHealth.Health);
    }



    private void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }



    private void SetHealth(int health)
    {
        _slider.value = health;
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//