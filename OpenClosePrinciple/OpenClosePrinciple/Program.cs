namespace OpenClosePrinciple
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            ShoesBefore shoesBefore = new ShoesBefore { Price = 100, Size = 9 };
            Console.WriteLine(shoesBefore.GetDiscount("Nike"));

            Shoe nikeshoe = new Nike();
            nikeshoe.Size = 9;
            nikeshoe.Price = 100;
            nikeshoe.DiscountAvailabe = .10;
            Console.WriteLine(nikeshoe.GetDiscount());

            Shoe adidasshoe = new Adidas();
            adidasshoe.Size = 9;
            adidasshoe.Price = 3590;
            adidasshoe.DiscountAvailabe = .15;
            Console.WriteLine(adidasshoe.GetDiscount());
            Console.Read();
        }
    }

    // ShoeBefore class is following Single Responsibility principle as it is performing only one task
    // (if we ignore calculation of discount) but it will violate OCP if new requirement comes eg if 
    // we want a new shoeType to be added for discount "PUMA" then we need to change the current 
    // implementation of ShoeBefore class GetDiscount method by adding more else if() condition
    public class ShoesBefore
    {
        public int Size { get; set; }
        public double Price { get; set; }
        public double GetDiscount(string shoeType)
        {
            if (shoeType == "Nike")
            {
                return this.Price - (.10 * this.Price);
            }
            else if (shoeType == "Adidas")
            {
                return this.Price - (.20 * this.Price);
            }
            else
            {
                return this.Price;
            }
        }
    }


    // With the below implementation if new cliet comes apart from Nike, Adidas, Puma 
    // then we dont need to change the current class implementation thus following OCP
    // and now each class is performing single task thus following single responsibility principle also.
    public class Shoe
    {
        public int Size { get; set; }
        public double Price { get; set; }
        public double DiscountAvailabe { get; set; }

        public virtual double GetDiscount()
        {
            return this.Price;
        }
    }

    public class Discount
    {
        public double CalculateDiscount(double price, double discount)
        {
            return price - (discount * price);
        }
    }

    public class Nike : Shoe
    {
        private readonly Discount discountCalculator = new Discount();

        public override double GetDiscount()
        {
            return this.discountCalculator.CalculateDiscount(base.GetDiscount(), base.DiscountAvailabe);
        }
    }

    public class Adidas : Shoe
    {
        private readonly Discount discountCalculator = new Discount();

        public override double GetDiscount()
        {
            return this.discountCalculator.CalculateDiscount(base.GetDiscount(), base.DiscountAvailabe);
        }
    }

    public class Puma : Shoe
    {
        private readonly Discount discountCalculator = new Discount();
        public override double GetDiscount()
        {
            return this.discountCalculator.CalculateDiscount(base.GetDiscount(), base.DiscountAvailabe);
        }
    }

}
