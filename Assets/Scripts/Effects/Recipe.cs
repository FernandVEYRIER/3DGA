﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Holds a recipe to make a cocktail.
    /// </summary>
    [Serializable]
    public sealed class Recipe
    {
        /// <summary>
        /// Name of the recipe.
        /// </summary>
        public string Name { get { return _name; } }

        [SerializeField] private string _name;
        [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();

        /// <summary>
        /// Tells if the list of liquids matches the recipe.
        /// </summary>
        /// <param name="containedLiquids"></param>
        /// <returns></returns>
        public bool Matches(List<EffectFactory.LIQUID_TYPE> containedLiquids)
        {
            //return _ingredients.Except(containedLiquids).ToList().Count == 0;
            int liquidsFound = 0;
            foreach (var liquid in containedLiquids)
            {
                if (_ingredients.Find(x => x.Liquid == liquid) == null)
                    return false;
                ++liquidsFound;
            }
            return containedLiquids.Count > 0 && liquidsFound == _ingredients.Count;
        }
    }

    /// <summary>
    /// Represents an ingredient : liquid with a corresponding percentage.
    /// </summary>
    [Serializable]
    public class Ingredient
    {
        public EffectFactory.LIQUID_TYPE Liquid;
        [Range(0, 100)]
        public float Percentage;
    }
}