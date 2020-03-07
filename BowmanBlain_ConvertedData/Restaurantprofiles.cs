using System;
using System.Collections.Generic;
using System.Text;

namespace BowmanBlain_ConvertedData
{
    class Restaurantprofiles
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Time { get; set; }
        public string Price { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public float FoodRating { get; set; }
        public float ServiceRating { get; set; }
        public float AmbienceRating { get; set; }
        public float ValueRating { get; set; }
        public float OverallRating { get; set; }
        public float OverallPossible { get; set; }
        public override string ToString()
        {
            return Name.ToString() + ", " + Address.ToString() + ",  " + Phone.ToString() + ",  " + Time.ToString() + ",  " + Price.ToString() + ",  " + Location.ToString() + ",  " + Cuisine.ToString() + ",  " + FoodRating.ToString() + ",  " + ServiceRating.ToString() + ",  " + AmbienceRating.ToString() + ",  " + ValueRating.ToString() + ",  " + OverallRating.ToString() + ",  " + OverallPossible.ToString();
        }
    }
}
