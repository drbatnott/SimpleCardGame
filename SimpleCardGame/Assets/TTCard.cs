using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTCard
{
    int imageNumber;
    string imageName;
    /// <summary>
    /// This is a class for the properties of the card but I don't think we need it
    /// We don't need the name so it is really just an integer
    /// </summary>
    class Properties
    {
        //public string name;
        public int value;
        public Properties( int i)
        {
           // name = s;
            value = i;
        }
    }
    List<Properties> thisCardProperties;
    /// <summary>
    /// COnstructor for the TTCard Class
    /// </summary>
    /// <param name="cardName">Name of the thing on the card</param>
    /// <param name="imageN">Number of the Sprite to be used for the face of the card</param>
    /// <param name="n">An array of strings that represent the property names
    /// We will stop using this because they really should be a property of the Pack</param>
    /// <param name="v">Is the value of each of the properties on a given card </param>
    public TTCard(string cardName,int imageN, int[] v)
    {
        int count = v.Length;
        imageName = cardName;
        thisCardProperties = new List<Properties>();
        imageNumber = imageN;
        for (int i = 0; i < count; i++)
        {
            Properties p = new Properties(v[i]);
            thisCardProperties.Add(p);
        }
    }
    /*public string PropertyName(int i)
    {
        return thisCardProperties[i].name;
    } */
    public int BestPropertyNumber()
    {
        int n = -1;
        int i = 0;
        for(int j = 0; j < thisCardProperties.Count; j++)
        {
            if (thisCardProperties[j].value > n)
            {
                n = thisCardProperties[j].value;
                i = j;
            }
        }
        return i;
    }
    public int BestPropertyValue()
    {
        return thisCardProperties[BestPropertyNumber()].value;
    }
    public int CardNo()
    {
        return imageNumber;
    }
    public string CardName()
    {
        return imageName;
    }
    public int CardPropertyValue(int j)
    {
        return thisCardProperties[j].value;
    }
    
}
