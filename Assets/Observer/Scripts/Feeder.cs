using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Feeder : MonoBehaviour, IFeeder
{
    [SerializeField] private Button FeedBtn;
    [SerializeField] private List<DecorateFoodTypeFeedingComponent> decorateFoodTypeFeedingList = new List<DecorateFoodTypeFeedingComponent>();
    
    private List<IEater> eaterList = new List<IEater>();

    private IFoodTypeFeed resultFeeding;

    private void Awake()
    {
        FeedBtn.onClick.AddListener(Feed);
        resultFeeding = new ConcreteFeedFood();

        for (int i = 0; i < decorateFoodTypeFeedingList.Count; i++)
        {
            decorateFoodTypeFeedingList[i].SetDefault(this);
        }
    }

    #region Change FeedingMethod
    public void DecorateFeeding(FoodType foodType)
    {
        resultFeeding = new DecoratorFeedFood(resultFeeding, foodType);
    }

    public void DedecorateFeeding(FoodType foodType)
    {
        resultFeeding = Dedecorate(foodType, resultFeeding);
    }

    private IFoodTypeFeed Dedecorate(FoodType foodType, IFoodTypeFeed innerFeeding)
    {
        if (!(innerFeeding is DecoratorFeedFood))
            return innerFeeding;

        var tempFeeding = (DecoratorFeedFood)innerFeeding;

        if (tempFeeding.FoodTypeFeeding == foodType)
            return tempFeeding.PrevFeedFood;
        else
            return new DecoratorFeedFood(Dedecorate(foodType, tempFeeding.PrevFeedFood), tempFeeding.FoodTypeFeeding);
    }
    #endregion

    public void Adopt(IEater eater)
    {
        if (!eaterList.Contains(eater))
            eaterList.Add(eater);
    }

    public void Release(IEater eater)
    {
        if (eaterList.Contains(eater))
            eaterList.Remove(eater);
    }

    public void Feed()
    {
        for (int i = 0; i < eaterList.Count; i++)
        {
            resultFeeding.FeedByFoodType(eaterList[i]);
        }
    }
}


[Serializable]
public class DecorateFoodTypeFeedingComponent
{
    public FoodType foodTypeDecorating;
    public Button behaveBtn;
    public TextMeshProUGUI btnTxt;
    public Image btnImage;

    public bool decorateFoodTypeFlag;
    public Feeder theFeeder;

    public void SetDefault(Feeder feeder)
    {
        theFeeder = feeder;

        behaveBtn.onClick.AddListener(Decorate);
        btnTxt.text = "Decorate";
        btnImage.color = Color.white;
        decorateFoodTypeFlag = true;
    }

    private void ChangeState()
    {
        if (decorateFoodTypeFlag)
        {
            behaveBtn.onClick.RemoveListener(Decorate);
            behaveBtn.onClick.AddListener(Dedecorate);
            btnTxt.text = "Dedecorate";
            btnImage.color = Color.red;
        }
        else
        {
            behaveBtn.onClick.RemoveListener(Dedecorate);
            behaveBtn.onClick.AddListener(Decorate);
            btnTxt.text = "Decorate";
            btnImage.color = Color.white;
        }

        decorateFoodTypeFlag = !decorateFoodTypeFlag;
    }

    private void Decorate()
    {
        theFeeder.DecorateFeeding(foodTypeDecorating);
        ChangeState();
    }
    private void Dedecorate()
    {
        theFeeder.DedecorateFeeding(foodTypeDecorating);
        ChangeState();
    }
}
