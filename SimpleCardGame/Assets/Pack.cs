using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Pack : MonoBehaviour
{
    TextReader tr;
    public Text display,secondTest;
    List<TTCard> ttCards;
    public Sprite [] sprites;
    //int imageNo;
    int countOfImages;
    public GameObject cardSprite;
    public GameObject prototypeCard;
    SpriteRenderer spriteRenderer;
    string[] propertyNames;
    public List<TTCard> playerHand;
    public List<TTCard> computerHand;
    List<GameObject> playerHandCardObjects;
    List<GameObject> computerHandCardObjects;
    float speed = 0.025f;
    public Vector3 cardPosOnTable;
    int cardsDealt;
    public GameObject dealButton;
    public GameObject playButton;
    int totalPlayerNumbers = 2;
    int playerToDealTo = 0;
    GameObject dealingCard;
    bool cardDealAnimation;
    // Start is called before the first frame update
    void Start()
    {
        countOfImages = sprites.Length;
        cardDealAnimation = false;
        //imageNo = 0;
        cardsDealt = 0;
        spriteRenderer = cardSprite.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprites[0];
        ttCards = new List<TTCard>();
        playerHand = new List<TTCard>();
        playerHandCardObjects = new List<GameObject>();
        computerHandCardObjects = new List<GameObject>();
        computerHand = new List<TTCard>();
        tr = new StreamReader("C:/Users/peter/source/repos/SimpleCardGame/SimpleCardGame/Assets/GameDefinitions.txt");
        string input;
       
        input = tr.ReadLine();
        string propertyBit = input.Substring(13);
        //Bug found probably the index is wrong. As a test I will display propertyBit to see
        //It should have been 13 first I changed it to 12 but that was also wrong I forgot about the tab.
        //Originally I had put 19 - for the previous example
        //display.text = "The Property Bit " + propertyBit + "\n";
        propertyNames = propertyBit.Split('\t');
       // int i = propertyNames.Length;
       //display.text += "Card Number\tCard Name\t" ;
       
        // display.text += "\n";
        /*foreach (string s in propertyNames)
            display.text += s + "\t";
        display.text += "\n"; */
        while ((input=tr.ReadLine())!= null)
        {
            CreateACardFromTheFileDescription(input);
        }
        tr.Close();
        /*
        foreach(TTCard tTCard in ttCards)
        {
            display.text += "Bird " + tTCard.CardName() + " Best Property " + propertyNames[tTCard.BestPropertyNumber()];
            display.text += " number " + tTCard.BestPropertyNumber() + " value " + tTCard.BestPropertyValue() + "\n";
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Transform trans = cardSprite.GetComponent<Transform>();
        Vector3 pos = trans.position;
        System.Random r = new System.Random(); 
        int x = r.Next(-4, 5);
        pos.x += x * speed ;
        if (pos.x > 9.6f) pos.x = 0;
        if (pos.x < -9.6f) pos.x = 0;
        trans.position = pos;
        */
        Vector3 targetPosition = cardPosOnTable;
       if(playerToDealTo == 0)
        { 
            targetPosition.y = -cardPosOnTable.y; 
        }
        if (cardDealAnimation)
        {
            Vector3 pos = dealingCard.transform.position;
            if (pos.y > targetPosition.y) pos.y -= speed;
            else pos.y += speed;
            dealingCard.transform.position = pos;
            float diff = Math.Abs(targetPosition.y - pos.y);
            if (diff < 0.1) cardDealAnimation = false;
        } 
            
    }

    void CreateACardFromTheFileDescription(string input)
    {
        string[] content = input.Split('\t');
        int imageNumber = Int16.Parse(content[0]);

        string cardName = content[1];
        //display.text += imageNumber + "\t" + cardName + "\t";
        int[] vs = {Int16.Parse(content[2]), Int16.Parse(content[3]),
                Int16.Parse(content[4]),Int16.Parse(content[5]),Int16.Parse(content[6])};
        /* foreach (int v in vs)
         {
             display.text += v + "\t";
         }
         display.text += "\n"; */
        TTCard tT = new TTCard(cardName, imageNumber, vs);
        ttCards.Add(tT);
    }
    /*
     * This test was for showing we could change the image on the demo card
    public void OnClickDeal()
    {
        imageNo++;
        if(imageNo >= countOfImages)
        {
            imageNo = 0;
        }
        spriteRenderer.sprite = sprites[imageNo];
    }
    */

    public void OnClickPlay()
    {

    }
    /// <summary>
    /// Deal a card to the screen but also add the card to the player hand
    /// </summary>
    public void OnClickDeal()
    {
        System.Random r = new System.Random();
        int cardToDeal = r.Next(0, ttCards.Count);
        //spriteRenderer.sprite = sprites[cardToDeal];
        display.text = "chosen card " + cardToDeal;
        
        display.text += "\n" + "Cards Left in pack " + ttCards.Count + " cards in player hand " + playerHand.Count;
        //Add a card to the player hand
        if (playerToDealTo == 0)
            playerHand.Add(ttCards[cardToDeal]);
        else
            computerHand.Add(ttCards[cardToDeal]);
        //The next lines should create a new object of the prototypeCard type
        GameObject newCard = GameObject.Instantiate(prototypeCard);
        Transform newCardTransform = newCard.transform;
        Vector3 pos = cardPosOnTable;//newCardTransform.position;
        //This should position it at the centre of the play area
        dealingCard = GameObject.Instantiate(cardSprite);
        //pos.x += cardsDealt * 1.6f;
        cardsDealt++;
        if(playerToDealTo == 0)
        {
            pos.y = -3.5f;
        }
        else
            pos.y = 3.5f;
        newCard.transform.position = pos;
        SpriteRenderer newCardSR = newCard.GetComponent<SpriteRenderer>();
        //I was removing the card from the pack but then choosing the sprite from the sprites
        //instead I should use the sprite for the appropriate card number!
        int cSpriteN = ttCards[cardToDeal].CardNo();
        //newCardSR.sprite = sprites[cardToDeal]
        //what I actually needed was
        newCardSR.sprite = sprites[cSpriteN];
        //add the newCard to the list of GameObject Cards
        if(playerToDealTo == 0)
        {
            playerHandCardObjects.Add(newCard);
        }
        else
        {
            computerHandCardObjects.Add(newCard);
        }
        newCard.SetActive(false);
        //remove the card from the pack
        ttCards.RemoveAt(cardToDeal);
        //Test that we have one less card in the pack and one card in the hand
        display.text += "\nCards Left in pack " + ttCards.Count + " cards in player hand " + playerHand.Count + " cards in computer hand " + computerHand.Count;
        foreach(TTCard tt in ttCards)
        {
            secondTest.text += " " + tt.CardNo();
        }
        secondTest.text += "\n";
        if (ttCards.Count < 1)
        {
            dealButton.SetActive(false);
            playButton.SetActive(true);
            cardSprite.SetActive(false);
        }
        if (playerToDealTo < 1) playerToDealTo++;
        else playerToDealTo = 0;
        cardDealAnimation = true;
    }
}
