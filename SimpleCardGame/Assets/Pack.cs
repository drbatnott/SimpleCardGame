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
        string[] propertyNames;
        input = tr.ReadLine();
        string propertyBit = input.Substring(19);
        propertyNames = propertyBit.Split('\t');
       // int i = propertyNames.Length;
       display.text = "Card Number\tCard Name\t" ;
       // display.text += "\n";
        foreach(string s in propertyNames)
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
            TTCard tT = new TTCard(cardName,imageNumber, propertyNames, vs);
            ttCards.Add(tT);
        }
        tr.Close();
        foreach(TTCard tTCard in ttCards)
        {
            display.text += "Bird " + tTCard.CardName() + " Best Property " + tTCard.PropertyName(tTCard.BestPropertyNumber());
            display.text += " number " + tTCard.BestPropertyNumber() + " value " + tTCard.BestPropertyValue() + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
