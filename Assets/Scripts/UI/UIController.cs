using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private Image staminaBar;

    public void UpdateStaminaBar()
    {
        staminaBar.fillAmount = player.Stamina / 100;
    }
}
