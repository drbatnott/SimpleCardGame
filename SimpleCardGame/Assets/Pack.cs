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
    // Start is called before the first frame update
    void Start()
    {
        ttCards = new List<TTCard>();
        tr = new StreamReader("C:/Users/peter/source/repos/SimpleCardGame/SimpleCardGame/Assets/GameDefinitions.csv");
        string input;
        string[] propertyNames;
        input = tr.ReadLine();
        string propertyBit = input.Substring(19);
        propertyNames = propertyBit.Split('\t');
        int i = propertyNames.Length;
        display.text = "Number of properties: " + i;
        display.text += "\n";
        foreach(string s in propertyNames)
            display.text += s + "\n";
        while ((input=tr.ReadLine())!= null)
        {
            string[] content = input.Split('\t');
            int imageNumber = Int16.Parse(content[0]);
            
            string cardName = content[1];
            display.text += imageNumber + " " + cardName;
            int[] vs = {Int16.Parse(content[2]), Int16.Parse(content[3]),
                Int16.Parse(content[4]),Int16.Parse(content[5]),Int16.Parse(content[6])};
            foreach (int v in vs)
            {
                display.text += v + "\n";
            }
            TTCard tT = new TTCard(imageNumber, propertyNames, vs);
            ttCards.Add(tT);
        }
        tr.Close();
        foreach(TTCard tTCard in ttCards)
        {
            display.text += "\n" + tTCard.PropertyName(tTCard.BestPropertyValue());
            display.text += "\n" + tTCard.BestPropertyValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
