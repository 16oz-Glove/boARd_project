using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bUse : MonoBehaviour
{
    public void use_card()
    {
        GameCard.game_obj2.SendMessage("Effect");
    }
}
