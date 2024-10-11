using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField, Min(0)] private int maxHealth = 100;

    private GameOverManager _gameOverManager;
    
    private int _currentHealth;

    private void Start()
    {
        _gameOverManager = FindObjectOfType<GameOverManager>();
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void AddHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            _gameOverManager.EndGame();
        }
    }    

    private void UpdateHealthBar()
    {
        healthSlider.value = _currentHealth;
    }
}
