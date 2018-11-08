using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver
{
    private IObservable _hero;
    public float startHealth;
    private float heroHealth;
    public float explosionDamage;
    public float bulletDamage;
    public Image healthBar;

    public GameObject heroPrefab;

    
    public void OnNotify(string message)
    {
        switch (message)
        {
            case "Bullet Damage":
                print("Hero Recibio daño de una bullet");
                TakeDamage(bulletDamage);
                break;
            case "Explosion Damage":
                print("Hero Recibio daño de Explosion");
                TakeDamage(explosionDamage);
                break;
            case "Win":
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                break;
            default:
                break;
        }
    }

	void Start ()
    {
        heroHealth = startHealth;
        _hero = FindObjectOfType<Hero>();

        if (_hero != null)
        {
            _hero.AddObserver(this);
        }
    }


	public void TakeDamage(float amount)
    {
        heroHealth = heroHealth - amount;
        healthBar.fillAmount = heroHealth / startHealth;

        if (heroHealth <= 0f)
        {
            Destroy(heroPrefab);
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

	void Update ()
    {
		
	}
}
