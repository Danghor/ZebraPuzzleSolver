namespace ZebraPuzzleSolver
{
    class Car
    {
        public Brand? Brand { get; set; }
        public Color? Color { get; set; }
        public Job? Job { get; set; }
        public CD? CD { get; set; }
        public City? City { get; set; }

        public override string ToString() => $"{Brand} {Color} {Job} {CD} {City}";
    }
}
