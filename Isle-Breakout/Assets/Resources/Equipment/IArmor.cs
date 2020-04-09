using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmor
{
    List<Mesh> getMeshes();
    float getStrength();
    float getWisdom();
    float getSpeed();
    float getHP();
}
