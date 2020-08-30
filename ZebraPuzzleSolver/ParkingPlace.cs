using System;
using System.Collections.Generic;
using System.Linq;

namespace ZebraPuzzleSolver
{
    class ParkingPlace
    {
        public Car[] Cars { get; } = new Car[5];

        public ParkingPlace()
        {
            for (int i = 0; i < Cars.Length; i++)
            {
                Cars[i] = new Car();
            }

            Cars[3].CD = CD.Eminem;
        }

        internal void Solve()
        {
            foreach (var carCity in Cars)
            {
                foreach (var carCD in Cars)
                {
                    foreach (var carBrand in Cars)
                    {
                        foreach (var carColor in Cars)
                        {
                            foreach (var carJob in Cars)
                            {
                                if (carJob.Job == null)
                                {
                                    foreach (var job in Enum.GetValues(typeof(Job)).Cast<Job>())
                                    {
                                        carJob.Job = job;
                                        if (IsValid())
                                        {
                                            Solve();
                                        }
                                        carJob.Job = null;
                                    }
                                    return;
                                }
                            }

                            if (carColor.Color == null)
                            {
                                foreach (var color in Enum.GetValues(typeof(Color)).Cast<Color>())
                                {
                                    carColor.Color = color;
                                    if (IsValid())
                                    {
                                        Solve();
                                    }
                                    carColor.Color = null;
                                }
                                return;
                            }
                        }

                        if (carBrand.Brand == null)
                        {
                            foreach (var brand in Enum.GetValues(typeof(Brand)).Cast<Brand>())
                            {
                                carBrand.Brand = brand;
                                if (IsValid())
                                {
                                    Solve();
                                }
                                carBrand.Brand = null;
                            }
                            return;
                        }
                    }

                    if (carCD.CD == null)
                    {
                        foreach (var cd in Enum.GetValues(typeof(CD)).Cast<CD>())
                        {
                            carCD.CD = cd;
                            if (IsValid())
                            {
                                Solve();
                            }
                            carCD.CD = null;
                        }
                        return;
                    }
                }

                if (carCity.City == null)
                {
                    foreach (var city in Enum.GetValues(typeof(City)).Cast<City>())
                    {
                        carCity.City = city;
                        if (IsValid())
                        {
                            Solve();
                        }
                        carCity.City = null;
                    }
                    return;
                }
            }

            Print();
            Console.Read();
        }

        private bool IsValid()
        {
            return
                PropertyIsUnique(c => c.Brand)
                && PropertyIsUnique(c => c.Color)
                && PropertyIsUnique(c => c.Job)
                && PropertyIsUnique(c => c.CD)
                && PropertyIsUnique(c => c.City)
                && IsInSameCar(c => c.Brand, c => c.Color, Brand.Ferrari, Color.Rot)
                && IsInSameCar(c => c.Job, c => c.Color, Job.Lehrer, Color.Silber)
                && IsInSameCar(c => c.Brand, c => c.CD, Brand.VW, CD.Madonna)
                && IsInSameCar(c => c.Brand, c => c.City, Brand.BMW, City.München)
                && IsInSameCar(c => c.Job, c => c.CD, Job.Metzger, CD.Abba)
                && IsInSameCar(c => c.Job, c => c.City, Job.Notar, City.Köln)
                && IsInSameCar(c => c.Job, c => c.Brand, Job.Schreiner, Brand.Ford)
                && IsInSameCar(c => c.Color, c => c.City, Color.Grün, City.Hamburg)
                && IsNextTo(c => c.Color, c => c.Brand, Color.Blau, Brand.BMW)
                && IsNextTo(c => c.Color, c => c.City, Color.Braun, City.Hamburg)
                && IsNextTo(c => c.CD, c => c.Job, CD.Beatles, Job.Lehrer)
                && IsNextTo(c => c.City, c => c.Job, City.Berlin, Job.Bäcker)
                && IsNotNextTo(c => c.City, c => c.Brand, City.Stuttgart, Brand.BMW)
                && IsNextTo(c => c.Color, c => c.Brand, Color.Blau, Brand.Smart);
        }

        private bool IsNextTo<First, Second>(Func<Car, First> getFirst, Func<Car, Second> getSecond, First first, Second second)
        {
            var firstCar = Cars.SingleOrDefault(c => EqualityComparer<First>.Default.Equals(getFirst(c), first));
            var secondCar = Cars.SingleOrDefault(c => EqualityComparer<Second>.Default.Equals(getSecond(c), second));

            if (firstCar == null || secondCar == null)
            {
                return true;
            }

            var firstCarPosition = Array.IndexOf(Cars, firstCar);
            var secondCarPosition = Array.IndexOf(Cars, secondCar);

            var result = Math.Abs(firstCarPosition - secondCarPosition) == 1;
            return result;
        }
        private bool IsNotNextTo<First, Second>(Func<Car, First> getFirst, Func<Car, Second> getSecond, First first, Second second)
        {
            var firstCar = Cars.SingleOrDefault(c => EqualityComparer<First>.Default.Equals(getFirst(c), first));
            var secondCar = Cars.SingleOrDefault(c => EqualityComparer<Second>.Default.Equals(getSecond(c), second));

            if (firstCar == null || secondCar == null)
            {
                return true;
            }

            var firstCarPosition = Array.IndexOf(Cars, firstCar);
            var secondCarPosition = Array.IndexOf(Cars, secondCar);

            var result = Math.Abs(firstCarPosition - secondCarPosition) > 1;
            return result;
        }

        private bool IsInSameCar<First, Second>(Func<Car, First> getFirst, Func<Car, Second> getSecond, First first, Second second)
        {
            var result = !Cars.Any(c => getFirst(c) != null && getSecond(c) != null &&
            (EqualityComparer<First>.Default.Equals(getFirst(c), first) && !EqualityComparer<Second>.Default.Equals(getSecond(c), second)
            ||
            !EqualityComparer<First>.Default.Equals(getFirst(c), first) && EqualityComparer<Second>.Default.Equals(getSecond(c), second)));

            return result;
        }

        private bool PropertyIsUnique<T>(Func<Car, T> getProperty)
        {
            var listOfProperties = Cars.Select(c => getProperty(c)).Where(i => i != null);

            var result = listOfProperties.Count() == listOfProperties.Distinct().Count();
            return result;
        }

        private void Print()
        {
            foreach(var car in Cars)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine();
        }
    }
}
