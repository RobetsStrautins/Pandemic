using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonusCardInPopUp : MonoBehaviour
{
    public CardNode myNode;
    public Text title;
    public Text description;
    public BonusCardType bonusCardType;
    Player playerWhoHasCard;

    public void Init(CardNode cardNode, Player player)
    {
        myNode = cardNode;
        playerWhoHasCard = player;

        BonusCardData bonusCard = myNode.data as BonusCardData;

        bonusCardType = bonusCard.bonusType;

        name = bonusCard.title;

        title.text = bonusCard.title;
        description.text = bonusCard.description;

    }

    public void onCardClicked()
    {
        if (SelectionManager.Instance == null)
        {
            return;
        }

        PlayerCardInfoManager.Instance.hideInfo();
        CardInfoManager.Instance.showInfoWhenBonusCardPressed(title.text, bonusCardType, myNode, playerWhoHasCard);
    }
}
