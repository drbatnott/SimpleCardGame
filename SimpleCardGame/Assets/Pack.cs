using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Pack : MonoBehaviour
{
    TextReader tr;
    public Text display;
    List<TTCard> ttCards;
    public Sprite [] sprites;
    int imageNo;
    int countOfImages;
    public GameObject cardSprite;
    SpriteRenderer spriteRenderer;
    string[] propertyNames;
    public List<TTCard> playerHand;
    public List<TTCard> computerHand;
    float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        countOfImages = sprites.Length;
        imageNo = 0;
        spriteRenderer = cardSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        ttCards = new List<TTCard>();
        tr = new StreamReader("C:/Users/peter/source/repos/SimpleCardGame/SimpleCardGame/Assets/GameDefinitions.txt");
        string input;
       
        input = tr.ReadLine();
        string propertyBit = input.Substring(13);
        //Bug found probably the index is wrong. As a test I will display propertyBit to see
        //It should have been 13 first I changed it to 12 but that was also wrong I forgot about the tab.
        //Originally I had put 19 - for the previous example
        display.text = "The Property Bit " + propertyBit + "\n";
        propertyNames = propertyBit.Split('\t');
       // int i = propertyNames.Length;
       display.text += "Card Number\tCard Name\t" ;
       
        // display.text += "\n";
        foreach (string s in propertyNames)
            display.text += s + "\t";
        display.text += "\n";
        while ((input=tr.ReadLine())!= null)
        {
            string[] content = input.Split('\t');
            int imageNumber = Int16.Parse(content[0]);
            
            string cardName = content[1];
            display.text += imageNumber + "\t" + cardName + "\t";
            int[] vs = {Int16.Parse(content[2]), Int16.Parse(content[3]),
                Int16.Parse(content[4]),Int16.Parse(content[5]),Int16.Parse(content[6])};
            foreach (int v in vs)
            {
                display.text += v + "\t";
            }
            display.text += "\n";
            TTCard tT = new TTCard(cardName,imageNumber, vs);
            ttCards.Add(tT);
        }
        tr.Close();
        foreach(TTCard tTCard in ttCards)
        {
            display.text += "Bird " + tTCard.CardName() + " Best Property " + propertyNames[tTCard.BestPropertyNumber()];
            display.text += " number " + tTCard.BestPropertyNumber() + " value " + tTCard.BestPropertyValue() + "\n";
        }
        System.Random r = new System.Random();
        int cardToDeal = r.Next(0, ttCards.Count);
        spriteRenderer.sprite = sprites[cardToDeal];
        display.text += "chosen card " + cardToDeal;
        playerHand.Add(ttCards[cardToDeal]);
        ttCards.RemoveAt(cardToDeal);
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
    }
    public void OnClickDeal()
    {
        imageNo++;
        if(imageNo >= countOfImages)
        {
            imageNo = 0;
        }
        spriteRenderer.sprite = sprites[imageNo];
    }
}
