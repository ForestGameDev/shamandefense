using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Probability<T> {
    [SerializeField]
    int totalWeight = 0;
    [SerializeField]
    List<sPairs> pairs = new List<sPairs>();
    [System.Serializable]
    public struct sPairs
    {
        public T type;
        public int weight;
        sPairs(T type, int weight)
        {
            this.type = type;
            this.weight = weight;
        }
    }

    public void Add(T type, uint weight)
    {
        Add(type, (int)weight);
    }


	public void Add(T type, int weight)
    {

        if(type == null)
        {
            return;
        }
        int index = Contains(type);
        if(index == -1)
        {
            totalWeight += weight;
            sPairs tmp = new sPairs();
            tmp.type = type;
            tmp.weight = weight;
            pairs.Add(tmp);
        }
        else
        {
            int tmpWeight = pairs[index].weight + weight;
            Remove(type);
            Add(type, tmpWeight);
        }



    }

    public bool Remove(T type)
    {
        int index = Contains(type);
        if(index != - 1)
        { 
                totalWeight -= pairs[index].weight;
                pairs.RemoveAt(index);
                return true;

        }
        return false;
    }

    public bool ChangeWeight(T type, int weight)
    {
        if(Remove(type))
        {
            Add(type, weight);
            return true;
        }
        return false;
    }

    public bool Increase(T type, int increase)
    {
        int index = Contains(type);
        if(Contains(type) != -1)
        {
            int tmpWeight = pairs[index].weight + increase;
            if(tmpWeight < 0)
            {
                tmpWeight = 0;
            }
            Add(type, tmpWeight);
            return true;
        }
        return false;
    }



    public T Get()
    {
        int random = Random.Range(0, totalWeight);
        for(int i=0; i< pairs.Count; ++i)
        {
            if(random < pairs[i].weight)
            {
                return pairs[i].type;
            }
            random -= pairs[i].weight;
        }
        return default(T);
    }


    int Contains(T type)
    {
        for (int i = 0; i < pairs.Count; ++i)
        {
            if (type.Equals(pairs[i].type))
            {
                return i;
            }
        }
        return -1;
    }

}
