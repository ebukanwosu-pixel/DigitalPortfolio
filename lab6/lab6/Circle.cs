using System;
using System.Collections.Generic;
using System.Text;

namespace lab6
{
    public class Circle
    {
        // Static counter to assign unique IDs
        private static int _nextId = 1;

        // Read-only ID (assigned at construction)
        public int Id { get; }

        // Backing field for radius
        private double _radius;

        /// <summary>
        /// Gets or sets the radius. Throws an exception if set to a negative value.
        /// </summary>
        public double Radius
        {
            get => _radius;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Radius cannot be negative.");
                _radius = value;
            }
        }

        // ----- Constructors -----

        /// <summary>
        /// Creates a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius (must be non-negative).</param>
        public Circle(double radius)
        {
            Id = _nextId++;
            Radius = radius; // uses the property setter for validation
        }

        /// <summary>
        /// Creates a circle with a default radius of 1 (constructor chaining).
        /// </summary>
        public Circle() : this(1.0)
        {
            // Everything is handled by the chained constructor.
        }

        // ----- Member Methods -----

        /// <summary>
        /// Calculates the area of the circle (π * r²).
        /// </summary>
        public double Area() => Math.PI * Radius * Radius;

        /// <summary>
        /// Calculates the circumference of the circle (2 * π * r).
        /// </summary>
        public double Circumference() => 2 * Math.PI * Radius;

        // Optional: override ToString for easy display
        public override string ToString() =>
            $"Circle #{Id}: radius = {Radius:F2}, area = {Area():F2}, circumference = {Circumference():F2}";
    }
}
