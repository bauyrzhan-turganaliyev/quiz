﻿using UnityEngine;

namespace Tools.Extensions
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}