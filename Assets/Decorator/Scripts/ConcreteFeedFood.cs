using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcreteFeedFood : IFoodTypeFeed
{
    public virtual void FeedByFoodType(IEater eater)
    {
        eater.Eat(FoodType.Default);
    }
}
