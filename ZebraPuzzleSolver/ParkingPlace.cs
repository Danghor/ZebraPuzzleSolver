using System;
using System.Collections.Generic;
using System.Linq;

namespace ZebraPuzzleSolver
{
    class ParkingPlace
    {
        public Car[] Cars { get; } = new Car[5];

        internal void Solve()
        {
            for (int i = 0; i < Cars.Length; i++)
            {
                Cars[0] = new Car();
            }

            foreach (var car in Cars)
            {
                if (car.Brand == null)
                {

                }
            }
        }

        private bool IsValid()
        {
            
            bool ferrariIsRed = IsValidCombination(c => c.Brand, c => c.Color, Brand.Ferrari, Color.Rot);
            bool teacherIsSilver = IsValidCombination(c => c.Job, c => c.Color, Job.Lehrer, Color.Silber);

            return ferrariIsRed && teacherIsSilver;
        }

        private bool IsValidCombination<First, Second>(Func<Car, First> getFirst, Func<Car, Second> getSecond, First first, Second second)
        {
            return !Cars.Any(c => getFirst(c) != null && getSecond(c) != null &&
            (EqualityComparer<First>.Default.Equals(getFirst(c), first) && !EqualityComparer<Second>.Default.Equals(getSecond(c), second)
            ||
            !EqualityComparer<First>.Default.Equals(getFirst(c), first) && EqualityComparer<Second>.Default.Equals(getSecond(c), second)));
        }

        private bool PropertyIsUnique<T>(Func<Car, T> getProperty)
        {
            return Cars.Sum(c => getProperty(c))
        }
    }
}
