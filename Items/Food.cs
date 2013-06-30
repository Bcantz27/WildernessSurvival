using UnityEngine;
using System.Collections;

public class Food : Consumable {

    private float _carbs;
    private float _protein;
    private float _fat;

    public Food()
    {
        _carbs = 0;
        _protein = 0;
        _fat = 0;
    }

    public Food(float carbs, float protein, float fat)
    {
        _carbs = carbs;
        _protein = protein;
        _fat = fat;
    }

    public Food(float carbs, float protein, float fat, float[] healList)
    {
        _carbs = carbs;
        _protein = protein;
        _fat = fat;
        for (int i = 0; i < 4; i++)
        {
            setHealAt(i, healList[i]);
        }
    }

    public Food(float carbs, float protein, float fat, int index, float amount)
    {
        _carbs = carbs;
        _protein = protein;
        _fat = fat;
        setHealAt(index, amount);
    }

    public float Carbs
    {
        get { return _carbs; }
        set { _carbs = value; }
    }

    public float Protein
    {
        get { return _protein; }
        set { _protein = value; }
    }

    public float Fat
    {
        get { return _fat; }
        set { _fat = value; }
    }

    public float[] NutrientList()
    {
        float[] list = new float[3];

        list[0] = _carbs;
        list[1] = _protein;
        list[2] = _fat;

        return list;
    }


}
