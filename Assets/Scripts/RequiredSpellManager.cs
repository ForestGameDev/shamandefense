using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequiredSpellManager : MonoBehaviour {

    static List<int> register = new List<int>();


	public static int GetSpell(int size, int last = 0)
    {
        RemoveRegister(last);
       while(true)
        {
            int number = 0;
            for(int i=0;i< size;++i)
            {
                number *= 10;
                number += Random.Range(0, 5);
            }
            if(!register.Contains(number))
            {
                register.Add(number);
                return number;
            }
        }
    }

    public static void RemoveRegister(int number)
    {
        register.Remove(number);
    } 
}
