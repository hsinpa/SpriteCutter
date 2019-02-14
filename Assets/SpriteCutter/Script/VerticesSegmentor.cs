﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathUtility;

public class VerticesSegmentor {
    private bool isHorizontal;
    private System.Func<float, float> segmentEquation;


    public VerticesSegmentor(Vector2 point1, Vector2 point2)
    {
        float slope = Line.CalculateSlopeM(point1, point2);
        isHorizontal = (slope < 0.5f);

        segmentEquation = FindSegmentEquation(point1, point2, isHorizontal);
    }

    private System.Func<float, float> FindSegmentEquation(Vector2 p_point1, Vector2 p_point2, bool isHorizontal)
    {
        System.Func<float, float> slopeFormula = null;

        float slope = MathUtility.Line.CalculateSlopeM(p_point1, p_point2);
        if (!isHorizontal)
            slopeFormula = MathUtility.Line.CalculateLinearRegressionX(p_point1, p_point2);
        else
            slopeFormula = MathUtility.Line.CalculateLinearRegressionY(p_point1, p_point2);

        return slopeFormula;
    }

    public bool CompareInputWithAverageLine(Vector2 p_point) {
        float v = (isHorizontal) ? segmentEquation(p_point.x) : segmentEquation(p_point.y);
        if (isHorizontal)
        {
            return (v > p_point.y);
        }
        else
        {
            return (v > p_point.x);
        }
    }
}
