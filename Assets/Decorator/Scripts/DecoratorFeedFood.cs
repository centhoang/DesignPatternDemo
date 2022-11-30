using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratorFeedFood : IFoodTypeFeed
{
    protected IFoodTypeFeed _PrevFeedFood;
    public IFoodTypeFeed PrevFeedFood => _PrevFeedFood;

    protected FoodType _foodTypeFeeding;
    public FoodType FoodTypeFeeding => _foodTypeFeeding;

    public DecoratorFeedFood(IFoodTypeFeed foodTypeFeedable, FoodType foodTypeFeeding)
    {
        _PrevFeedFood = foodTypeFeedable;
        _foodTypeFeeding = foodTypeFeeding;
    }

    public void FeedByFoodType(IEater eater)
    {
        eater.Eat(_foodTypeFeeding);
        if (_PrevFeedFood != null)
            _PrevFeedFood.FeedByFoodType(eater);
    }
}
