using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Ext {
    public static T DefaultConstructor<T>() where T : class => (T)typeof(T).GetConstructor(new Type[]{}).Invoke(new object[]{});
    public static Vector2 GetTrajectoryPosition(float angle, float v0, float t) {
        var g = Mathf.Abs(Physics2D.gravity.y);
        var sinAngle = Mathf.Sin(angle);
        var cosAngle = Mathf.Cos(angle);

        var x = v0 * t * cosAngle;
        var y = v0 * t * sinAngle - 0.5f * g * t * t;
        return new Vector2(x, y);
    }
    public static float GetTrajectoryInitialVelocity(float maxY, float angle){
        var g = Mathf.Abs(Physics2D.gravity.y);
        var sinAngle = Mathf.Sin(angle);
        return Mathf.Sqrt(2 * g * maxY / (sinAngle * sinAngle));
    }
    public static float GetTotalTrajectoryTime(float v0, float angle, float y0) {
        var g = Mathf.Abs(Physics2D.gravity.y);

        var sinAngle = Mathf.Sin(angle);
        var result = v0 * sinAngle;
        result += Mathf.Sqrt(v0 * sinAngle * v0 * sinAngle + 2 * g * y0);
        result /= g;
        return result;
    }

    public static T GetRandom<T>(this IEnumerable<T> list) {
        var index = UnityEngine.Random.Range(0, list.Count());
        return list.ElementAt(index);
    }
}