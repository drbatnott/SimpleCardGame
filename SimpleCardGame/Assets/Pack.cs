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
    float speed = 0.05f;
    public Vector3 cardPosOnTable;
    int cardsDealt;
    public GameObject dealButton;
    public GameObject playButton;
    int totalPlayerNumbers = 2;
    int playerToDealTo = 0;
    GameObject dealingCard;
    bool cardDealAnimation;
    public GameObject[] choiceButtons;
    bool cardDealt;
    int goNumber;
    bool playerWin, computerWin, tie;
    public Vector3 playerShow,computerShow;
    // Start is called before the first frame update
    void Start()
    {
        countOfImages = sprites.Length;
        cardDealAnimation = false;
        cardDealt = false;
        playerWin = computerWin = tie = false;
        goNumber = 0;
        //imageNo = 0;
        cardsDealt = 0;
        spriteRenderer = cardSprite.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprites[0];
        ttCards = new List<TTCard>();
        playerHand = new List<TTCard>();
        playerHandCardObjects = new List<GameObject>();
        computerHandCardObjects = new List<GameObject>();
        computerHand = new List<TTCard>();
        tr = new StreamReader("C:/Users/peter/source/repos/SimpleCardGame/SimpleCardGame/Assets/GameDefinitions.csv");
        string input;
       
        input = tr.ReadLine();
        string propertyBit = input.Substring(19);
        //Bug found probably the index is wrong. As a test I will display propertyBit to see
        //It should have been 13 first I changed it to 12 but that was also wrong I forgot about the tab.
        //Originally I had put 19 - for the previous example
        //display.text = "The Property Bit " + propertyBit + "\n";
        //now reverting to previous example so back to 19
        propertyNames = propertyBit.Split(',');
       // int i = propertyNames.Length;
       //display.text += "Card Number\tCard Name\t" ;
       
        // display.text += "\n";
        foreach (string s in propertyNames)
            display.text += s + ",";
        display.text += "\n";/* */
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

        
        if (cardDealAnimation)
        {
            Vector3 targetPosition = cardPosOnTable;
            if (playerToDealTo == 0)
            {
                targetPosition.y = -cardPosOnTable.y;
            }
            if(!cardDealt && ttCards.Count > 0)
                DealACard();
            Vector3 pos = dealingCard.transform.position;
            if (pos.y > targetPosition.y) pos.y -= speed;
            else pos.y += speed;
            dealingCard.transform.position = pos;
            float diff = Math.Abs(targetPosition.y - pos.y);
            if (diff < 0.1)
            {
                //cardDealAnimation = false;
                cardDealt = false;
                if (playerToDealTo == 0)
                    playerToDealTo = 1;
                else
                    playerToDealTo = 0;
                if (ttCards.Count == 0)
                {
                    cardDealAnimation = false;
                    playButton.SetActive(true);
                }
            }
            
        } 
            
    }

    void CreateACardFromTheFileDescription(string input)
    {
        string[] content = input.Split(',');
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
    void ComputerGo()
    {
        goNumber = 0;
    }
    public void OnChoiceClick(int i)
    {
        int ct = computerHandCardObjects.Count -1;
        computerHandCardObjects[ct].SetActive(true);
        computerHandCardObjects[ct].transform.position = computerShow;
        secondTest.text += "\nCard property choice " + i;
        secondTest.text += "\nProperty name " + propertyNames[i];
        int j = playerHand.Count - 1;
        int v = playerHand[j].CardPropertyValue(i);
        secondTest.text += "\nProperty value " + v;
        int k = computerHand.Count - 1;
        Vector3 pos = computerHandCardObjects[k].transform.position;
        pos.x += 3f;
        computerHandCardObjects[k].transform.position = pos;
        computerHandCardObjects[k].SetActive(true);
        int cv = computerHand[k].CardPropertyValue(i);
        secondTest.text += "\nComputer's value for " + propertyNames[i] + "is " + cv;
        if(v > cv)
        {
            display.text += "\nYou win that one";
            playerWin = true;
            computerWin = tie = false;
            goNumber = 0;
            TTCard cardWon = computerHand[k];
            computerHand.RemoveAt(k);
            playerHand.Insert(0, cardWon);
            GameObject oldTopObject = playerHandCardObjects[playerHandCardObjects.Count - 1];
            GameObject cardWonObject = computerHandCardObjects[k];
            computerHandCardObjects.RemoveAt(k);
            playerHandCardObjects.RemoveAt(playerHandCardObjects.Count - 1);
            playerHandCardObjects.Insert(0, cardWonObject);
            playerHandCardObjects.Insert(0, oldTopObject);
            int top = playerHand.Count - 1;
            TTCard oldTop = playerHand[top];
            playerHand.RemoveAt(top);
            playerHand.Insert(0, oldTop);
            oldTopObject.SetActive(false);
            cardWonObject.SetActive(false);
        }
        else
        {
            if(cv > v)
            {
                display.text += "\nYou lose that one";
                computerWin = true;
                playerWin = tie = false;
                goNumber = 1;
                int ib = playerHand.Count - 1;
                TTCard cardWon = playerHand[ib];
                playerHand.RemoveAt(ib);
                computerHand.Insert(0, cardWon);
                GameObject oldTopObject = computerHandCardObjects[k];
                GameObject cardWonObject = playerHandCardObjects[ib];
                playerHandCardObjects.RemoveAt(ib);
                computerHandCardObjects.RemoveAt(k);
                computerHandCardObjects.Insert(0, cardWonObject);
                computerHandCardObjects.Insert(0, oldTopObject);
                int top = computerHand.Count - 1;
                TTCard oldTop = computerHand[top];
                computerHand.RemoveAt(top);
                computerHand.Insert(0, oldTop);
                oldTopObject.SetActive(false);
                cardWonObject.SetActive(false);

            }
            else
            {
                display.text += "\nThat one is a draw";
                int handTop = playerHand.Count - 1;
                TTCard playerTop = playerHand[handTop];
                GameObject playerCard = playerHandCardObjects[handTop];
                playerHand.RemoveAt(handTop);
                playerHand.Insert(0, playerTop);
                playerHandCardObjects.RemoveAt(handTop);
                playerHandCardObjects.Insert(0, playerCard);
                handTop = computerHand.Count - 1;
                TTCard computerTop = computerHand[handTop];
                GameObject computerCard = computerHandCardObjects[handTop];
                computerHand.RemoveAt(handTop);
                computerHand.Insert(0, computerTop);
               
                computerHandCardObjects.RemoveAt(handTop);
                computerHandCardObjects.Insert(0, computerCard);
                computerCard.SetActive(false);
                playerCard.SetActive(false);
                playerWin = computerWin = false;
                tie = true;
                goNumber = 1;
            }
            computerHandCardObjects[k].SetActive(false);
            playerHandCardObjects[playerHandCardObjects.Count - 1].SetActive(false);
        }
    }
    public void OnClickPlay()
    {
        if (computerHand.Count > 0 && playerHand.Count > 0)
        {
            if (goNumber == 0)
            {
                int i = playerHand.Count - 1;
                Vector3 pos = playerShow;// playerHandCardObjects[i].transform.position;
                //pos.x += 3f;
                playerHandCardObjects[i].transform.position = pos;
                playerHandCardObjects[i].SetActive(true);
                foreach (GameObject b in choiceButtons)
                {
                    b.SetActive(true);
                }
            }
            else
            {
                ComputerGo();
            }
        }
        else
        {
            playButton.SetActive(false);
            display.text = "You have " + playerHand.Count + " cards and computer has " +
                computerHand.Count + " cards. Game Over";
        }
    }

    void DealACard()
    {
        System.Random r = new System.Random();
        int cardToDeal = r.Next(0, ttCards.Count);
        //spriteRenderer.sprite = sprites[cardToDeal];
        cardsDealt++;
        //display.text += "\nCards Dealt " + cardsDealt;
        //display.text += "\nchosen card " + cardToDeal;

        //display.text += "\n" + "Cards Left in pack " + ttCards.Count + " cards in player hand " + playerHand.Count;
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
        if (playerToDealTo == 0)
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
        if (playerToDealTo == 0)
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
        //display.text += "\nCards Left in pack " + ttCards.Count + " cards in player hand " + playerHand.Count + " cards in computer hand " + computerHand.Count;
        /*foreach (TTCard tt in ttCards)
        {
            secondTest.text += " " + tt.CardNo();
        }*/
        cardDealt = true;
    }
    /// <summary>
    /// Deal a card to the screen but also add the card to the player hand
    /// </summary>
    public void OnClickDeal()
    {
        //bool finished = false;

        cardDealAnimation = true;
        dealButton.SetActive(false);
    }
}
