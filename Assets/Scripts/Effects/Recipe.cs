using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public enum DrinkName
    {
        NOTHING,
        BEER,
        KAMIKAZE,
        LONGISLAND,
        TEQUILA,
        BACARDI,
        B52,
    }

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
        public DrinkName DrinkName { get { return dringName; } }

        [SerializeField] private string _name;
        [SerializeField] private DrinkName dringName;
        [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();

        /// <summary>
        /// How precise the percentages have to be to consider the recipe matching.
        /// </summary>
        private readonly float percentageTolerance = 10;

        /// <summary>
        /// Tells if the list of liquids matches the recipe.
        /// </summary>
        /// <param name="containedLiquids"></param>
        /// <returns></returns>
        public bool Matches(List<Ingredient> containedLiquids)
        {
            var foundLiquids = new List<Ingredient>();
            foreach (var liquid in containedLiquids)
            {
                var match = _ingredients.Find(x => x.Liquid == liquid.Liquid);
                if (match != null
                    && match.Percentage - percentageTolerance < liquid.Percentage && match.Percentage + percentageTolerance > liquid.Percentage)
                {
                    foundLiquids.Add(match);
                }
            }
            return foundLiquids.Count == _ingredients.Count;
            //return _ingredients.Except(containedLiquids).ToList().Count == 0;
            //int liquidsFound = 0;
            //foreach (var liquid in containedLiquids)
            //{
            //    if (_ingredients.Find(x => x.Liquid == liquid) == null)
            //        return false;
            //    ++liquidsFound;
            //}
            //return containedLiquids.Count > 0 && liquidsFound == _ingredients.Count;
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