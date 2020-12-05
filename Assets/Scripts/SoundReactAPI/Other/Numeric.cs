using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numeric
{
    #region Numeric_Types

    private int numericInt;
    private float numericFloat;

    #endregion

    #region Constructors

    public Numeric(int numericInt)
    {
        this.numericInt = numericInt;
    }

    public Numeric(float numericFloat)
    {
        this.numericFloat = numericFloat;
    }

    #endregion

    #region Numeric_Getters

    public int GetNumericInt()
    {
        return numericInt;
    }

    public float GetNumericFloat()
    {
        return numericFloat;
    }

    #endregion

}
