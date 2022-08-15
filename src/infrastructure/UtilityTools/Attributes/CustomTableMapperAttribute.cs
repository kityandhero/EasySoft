﻿using System.ComponentModel;

namespace UtilityTools.Attributes
{
    public class CustomTableMapperAttribute : DescriptionAttribute
    {
        public string Name { get; }

        public CustomTableMapperAttribute() : this("")
        {
        }

        public CustomTableMapperAttribute(string name, string description = "") : base(description)
        {
            Name = name;
        }
    }
}