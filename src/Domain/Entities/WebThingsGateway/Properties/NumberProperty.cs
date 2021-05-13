﻿using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record NumberProperty : Property
    {
        public NumberProperty(
            string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, int value, string unit, int minimum, int maximum)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            Value = value;
            Unit = unit;
            Minimum = minimum;
            Maximum = maximum;
        }

        public override GatewayValueType ValueType => GatewayValueType.Number;

        public int Value { get; }

        public string Unit { get; }

        public int Minimum { get; }

        public int Maximum { get; }
    }
}