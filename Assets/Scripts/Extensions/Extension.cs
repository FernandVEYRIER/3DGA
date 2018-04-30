using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Extension
    {
        /// <summary>
        /// Combine all the given colors.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="aColors"></param>
        /// <returns></returns>
        public static Color CombineColors(this Color color, params Color[] aColors)
        {
            Color result = new Color(0, 0, 0, 0);
            foreach (Color c in aColors)
            {
                result += c;
            }
            result /= aColors.Length;
            return result;
        }

        /// <summary>
        /// Combine all the given colors.
        /// </summary>
        /// <param name="aColors"></param>
        /// <returns></returns>
        public static Color CombineColors(List<Color> aColors)
        {
            Color result = new Color(0, 0, 0, 0);
            foreach (Color c in aColors)
            {
                result += c;
            }
            result /= aColors.Count;
            return result;
        }
    }
}