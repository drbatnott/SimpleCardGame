using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTCard 
{
    string imageName;
    class Properties
    {
        public string name;
        public int value;
        public Properties(string s, int i)
        {
            name = s;
            value = i;
        }
    }
    List<Properties> thisCardProperties;
    public TTCard(string iN, string[] n, int[] v)
    {
        int count = n.Length;
        thisCardProperties = new List<Properties>();
        imageName = iN;
        for (int i = 0; i < count; i++)
        {
            Properties p = new Properties(n[i],v[i]);
            thisCardProperties.Add(p);
        }
    }
    public string PropertyName(int i)
    {
        return thisCardProperties[i].name;
    }
    public int BestPropertyValue()
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
}
