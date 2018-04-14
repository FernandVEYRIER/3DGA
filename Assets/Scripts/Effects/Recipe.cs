using System;
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
        [SerializeField] private string _name;
        [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();

        [Serializable]
        private class Ingredient
        {
            public Liquid liquid;
            public float percentage;
        }
    }
}