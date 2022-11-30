using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IFeeder
{
    void Adopt(IEater eater);
    void Release(IEater eater);

    void Feed();
}
