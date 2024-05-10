using System;
using System.Collections.Generic;

namespace to_do_list_project.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            Filterstring = filterstring ?? "all-all-all";
            string[] filters = Filterstring.Split('-');

            // Ensure that the filters array has at least 3 elements before accessing them
            if (filters.Length >= 3)
            {
                CategoryId = filters[0];
                Due = filters[1];
                StatusId = filters[2];
            }
            else
            {
                // If the filters array doesn't have enough elements, set default values
                CategoryId = "all";
                Due = "all";
                StatusId = "all";
            }
        }

        public string Filterstring { get; }
        public string CategoryId { get; }
        public string Due { get; }
        public string StatusId { get; }

        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";

        public static Dictionary<string, string> DueFilterValues => new Dictionary<string, string>
        {
            {"future","Future"},
            {"past","Past"},
            {"today","Today"}
        };

        public bool IsPast => Due.ToLower() == "past";
        public bool IsToday => Due.ToLower() == "today";
        public bool IsFuture => Due.ToLower() == "future";
    }
}