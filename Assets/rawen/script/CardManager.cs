/*using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPositinOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositinThree;
    [SerializeField] List<CardSO>deck;
    // Start is called before the first frame update

    GameObject cardOne, cardTwo, cardThree;
    
    List<CardSO>alreadySelectedCards
   void RandomizeNewCards()
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> selectedCards = new List<CardSO>();
        List<CardSO> availableCards = new List<CardSO>();
        availableCards.RemoveAll(card => card.isUnique && alreadySelectedCards.Contains(Card));
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/