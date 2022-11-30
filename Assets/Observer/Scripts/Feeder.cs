using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feeder : MonoBehaviour, IFeeder
{
    [SerializeField] private Button FeedBtn;

    private void Awake()
    {
        FeedBtn.onClick.AddListener(Feed);
    }

    private List<IEater> eaterList = new List<IEater>();

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
            eaterList[i].Eat();
        }
    }
}
