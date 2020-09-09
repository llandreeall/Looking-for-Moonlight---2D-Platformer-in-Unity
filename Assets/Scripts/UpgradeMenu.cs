using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text damageText;

    [SerializeField]
    private Text speedText;

    private PlayerStats stats;

    [SerializeField]
    private int damageMultiplier = 10;

    [SerializeField]
    private int speedMultiplier = 2;

    [SerializeField]
    private int speedPrice = 50;

    [SerializeField]
    private int damagePrice = 20;

    private void OnEnable()
    {
        stats = PlayerStats.instance;

        UpdateValues();
    }

    void UpdateValues()
    {
        damageText.text = " DAMAGE: " + PlayerStats.instance.damage.ToString();
        speedText.text = " SPEED: " + stats.movementSpeed.ToString();
    }

    public void UpgradeDamage()
    {
        if ( GameMaster.Money < damagePrice )
        {
            AudioManager.instance.PlaySound("NoMoney");

            return;
        }

        PlayerStats.instance.damage += damageMultiplier;

        GameMaster.Money -= damagePrice;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMaster.Money < speedPrice)
        {
            AudioManager.instance.PlaySound("NoMoney");

            return;
        }

        stats.movementSpeed += speedMultiplier;

        GameMaster.Money -= speedPrice;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
}
