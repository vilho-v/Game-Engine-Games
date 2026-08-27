using UnityEngine;

public class HpPickup : MonoBehaviour
{
    public int healAmount = 20;
    public int maxHealthIncrease = 0;

    public float bobbingIntensity = 0.2f;


    void Update()
    {
        if(bobbingIntensity > 0)
        {
         //   transform.Translate()
        }
    }
    void OnTriggerEnter(Collider other)
    {


        if(other.TryGetComponent(out Player player))
        {
            if (maxHealthIncrease > 0)
            {
                if (player.maxHealth + maxHealthIncrease > player.maxMaxHealth_final_2_1)
                    player.maxHealth += maxHealthIncrease;
                else
                    player.maxHealth = player.maxMaxHealth_final_2_1;
            }

            if (healAmount > 0)
            {
                player.Heal(healAmount);
            }
            
        }
    }
}
