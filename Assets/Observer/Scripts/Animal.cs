using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum FoodType
{
    Default,
    Bone,
    Fish,
    Carrot,
    Meat
}

public class Animal : MonoBehaviour, IEater
{
    [SerializeField] private List<FoodType> foodTypeEating;

    [SerializeField] private BehaveWithFeederComponent behaveWithA;
    [SerializeField] private BehaveWithFeederComponent behaveWithB;

    [SerializeField] private TextMeshProUGUI eatAmountTxt;
    private int eatAmount = 0;

    private void Awake()
    {
        behaveWithA.SetDefault(this);
        behaveWithB.SetDefault(this);
        eatAmountTxt.text = 0.ToString();
    }

    public void Eat(FoodType foodType)
    {
        if (foodTypeEating.Contains(foodType))
        {
            eatAmount++;
            eatAmountTxt.text = eatAmount.ToString();
        }
    }
}

[Serializable]
public struct BehaveWithFeederComponent
{
    public Button behaveBtn;
    public TextMeshProUGUI btnTxt;
    public Feeder feeder;
    public bool registerEatBehaveFlag;

    private IEater owner;

    public void SetDefault(IEater _ower)
    {
        owner = _ower;
        btnTxt.text = "Register Eat";
        registerEatBehaveFlag = true;

        behaveBtn.onClick.AddListener(RegisterEat);
    }

    private void RegisterEat()
    {
        feeder.Release(owner);
        feeder.Adopt(owner);

        SwitchBehave();
    }

    private void UnregisterEat()
    {
        feeder.Release(owner);

        SwitchBehave();
    }

    public void SwitchBehave()
    {
        registerEatBehaveFlag = !registerEatBehaveFlag;
        if (registerEatBehaveFlag)
        {
            btnTxt.text = "Register Eat";
            behaveBtn.onClick.RemoveListener(UnregisterEat);
            behaveBtn.onClick.AddListener(RegisterEat);
        }
        else
        {
            btnTxt.text = "Unregister Eat";
            behaveBtn.onClick.RemoveListener(RegisterEat);
            behaveBtn.onClick.AddListener(UnregisterEat);
        }
    }
}
