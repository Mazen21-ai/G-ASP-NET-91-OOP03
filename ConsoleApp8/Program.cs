
/*
 
Q1

A) Overloading: same method name with different parameters.
Overriding: redefining a parent method in the child class using "override".

B) Static Binding: resolved at compile time.
Dynamic Binding: resolved at runtime based on the object's actual type.

Q2

A) "sealed" class prevents inheritance.

B) "sealed" class prevents inheriting the class, while "sealed" method prevents further overriding of that method.

C) No, because "sealed" prevents the method from being overridden again.
*/

/*
using System;

struct DeliveryAddress
{
    public string Street;
    public string City;
    public string Country;

    public DeliveryAddress(string street, string city, string country)
    {
        Street = street;
        City = city;
        Country = country;
    }

    public override string ToString()
    {
        return Street + ", " + City + ", " + Country;
    }
}

class Shipment
{
    private string trackingCode;
    private string description;
    private decimal weight;
    private decimal deliveryFee;

    public string TrackingCode
    {
        get { return trackingCode; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tracking code cannot be empty.");

            trackingCode = value;
        }
    }

    public string Description
    {
        get { return description; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.");

            description = value;
        }
    }

    public decimal Weight
    {
        get { return weight; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Weight cannot be negative.");

            weight = value;
        }
    }

    public decimal DeliveryFee
    {
        get { return deliveryFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Delivery fee cannot be negative.");

            deliveryFee = value;
        }
    }

    public DeliveryAddress Destination { get; set; }

    public Shipment()
    {
        TrackingCode = "";
        Description = "";
        Weight = 0;
        DeliveryFee = 0;
        Destination = new DeliveryAddress();
    }

    public Shipment(string trackingCode, string description)
        : this()
    {
        TrackingCode = trackingCode;
        Description = description;
    }

    public Shipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
    {
        TrackingCode = trackingCode;
        Description = description;
        Weight = weight;
        DeliveryFee = deliveryFee;
        Destination = destination;
    }

    public virtual decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5);
        }
    }

    public void UpdateDeliveryFee(decimal newFee)
    {
        if (newFee < 0)
            throw new ArgumentException("Delivery fee cannot be negative.");

        DeliveryFee = newFee;
    }

    public void UpdateWeight(decimal newWeight)
    {
        Weight = newWeight;
    }

    public void UpdateWeight(decimal newWeight, decimal extraPackingWeight)
    {
        Weight = newWeight + extraPackingWeight;
    }

    public virtual void PrintShipment()
    {
        Console.WriteLine("Tracking Code : " + TrackingCode);
        Console.WriteLine("Description : " + Description);
        Console.WriteLine("Weight : " + Weight + " KG");
        Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
        Console.WriteLine("Estimated Cost: " + EstimatedCost + " EGP");
    }
}

class StandardShipment : Shipment
{
    public StandardShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Standard Shipment");
        base.PrintShipment();
    }
}

class ExpressShipment : Shipment
{
    private decimal extraFee;

    public decimal ExtraFee
    {
        get { return extraFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Extra fee cannot be negative.");

            extraFee = value;
        }
    }
    public ExpressShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        decimal extraFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        ExtraFee = extraFee;
    }

    public override decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5) + ExtraFee;
        }
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Express Shipment");
        Console.WriteLine("Tracking Code : " + TrackingCode);
        Console.WriteLine("Description : " + Description);
        Console.WriteLine("Weight : " + Weight + " KG");
        Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
        Console.WriteLine("Extra Fee : " + ExtraFee + " EGP");
        Console.WriteLine("Estimated Cost: " + EstimatedCost + " EGP");
    }
}

class InternationalShipment : Shipment
{
    private string destinationCountry;
    private decimal customsFee;

    public string DestinationCountry
    {
        get { return destinationCountry; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Destination country cannot be empty.");

            destinationCountry = value;
        }
    }

    public decimal CustomsFee
    {
        get { return customsFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Customs fee cannot be negative.");

            customsFee = value;
        }
    }

    public InternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    public override decimal EstimatedCost
    {
        get
        {
            return DeliveryFee + (Weight * 5) + CustomsFee;
        }
    }

    public virtual void GenerateCustomsReport()
    {
        Console.WriteLine("Customs Report Generated.");
    }

    public override void PrintShipment()
    {
        Console.WriteLine("International Shipment");
        Console.WriteLine("Tracking Code : " + TrackingCode);
        Console.WriteLine("Description : " + Description);
        Console.WriteLine("Weight : " + Weight + " KG");
        Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
        Console.WriteLine("Destination Country : " + DestinationCountry);
        Console.WriteLine("Customs Fee : " + CustomsFee + " EGP");
        Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
    }
}

class PriorityInternationalShipment : InternationalShipment
{
    public PriorityInternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(
            trackingCode,
            description,
            weight,
            deliveryFee,
            destination,
            destinationCountry,
            customsFee)
    {
    }

    public sealed override void GenerateCustomsReport()
    {
        Console.WriteLine("Priority Customs Report Generated.");
    }
}

sealed class CompletedShipment : Shipment
{
    public CompletedShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Completed Shipment");
        base.PrintShipment();
    }
}
class Driver
{
    public int DriverId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    public Driver(int driverId, string fullName, string phoneNumber)
    {
        DriverId = driverId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }
}

class DeliveryCenter
{
    private Shipment[] shipments;

    public string CenterName { get; set; }

    public Driver Driver { get; set; }

    public Shipment[] Shipments
    {
        get { return shipments; }
    }

    public DeliveryCenter(string centerName)
    {
        CenterName = centerName;
        shipments = new Shipment[20];
    }

    public bool AddShipment(Shipment shipment)
    {
        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i] == null)
            {
                shipments[i] = shipment;
                return true;
            }
        }

        return false;
    }

    public bool RemoveShipment(string trackingCode)
    {
        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i] != null &&
                shipments[i].TrackingCode == trackingCode)
            {
                shipments[i] = null;
                return true;
            }
        }

        return false;
    }

    public Shipment this[int index]
    {
        get
        {
            if (index < 0 || index >= shipments.Length)
                throw new IndexOutOfRangeException();

            return shipments[index];
        }

        set
        {
            if (index < 0 || index >= shipments.Length)
                throw new IndexOutOfRangeException();

            shipments[index] = value;
        }
    }

    public Shipment this[string trackingCode]
    {
        get
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null &&
                    shipments[i].TrackingCode == trackingCode)
                {
                    return shipments[i];
                }
            }

            return null;
        }
    }

    public void PrintAllShipments()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("Delivery Center");
        Console.WriteLine("==========================================");
        Console.WriteLine("Driver : " + Driver.FullName);
        Console.WriteLine("------------------------------------------");

        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i] != null)
            {
                shipments[i].PrintShipment();
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}

static class DeliveryHelper
{
    public static void PrintShipmentDetails(Shipment shipment)
    {
        shipment.PrintShipment();
    }
}

class Program
{
    static void Main()
    {
        Driver driver = new Driver(
            1,
            "Ahmed Mohamed",
            "01000000000"
        );

        DeliveryCenter center = new DeliveryCenter("Smart Delivery Center");

        center.Driver = driver;

        DeliveryAddress address1 =
            new DeliveryAddress("Street 1", "Cairo", "Egypt");

        DeliveryAddress address2 =
            new DeliveryAddress("Street 2", "Giza", "Egypt");

        DeliveryAddress address3 =
            new DeliveryAddress("Street 3", "Alexandria", "Egypt");

        StandardShipment standard =
            new StandardShipment(
                "SH001",
                "Laptop",
                3,
                80,
                address1
            );

        ExpressShipment express =
            new ExpressShipment(
                "SH002",
                "Mobile Phone",
                2,
                60,
                address2,
                30
            );
        InternationalShipment international =
                    new InternationalShipment(
                        "SH003",
                        "Television",
                        8,
                        120,
                        address3,
                        "Germany",
                        100
                    );

        center.AddShipment(standard);
        center.AddShipment(express);
        center.AddShipment(international);

        center.PrintAllShipments();

        Console.WriteLine("==========================================");
        Console.WriteLine("Printing Using DeliveryHelper...");
        Console.WriteLine("------------------------------------------");

        DeliveryHelper.PrintShipmentDetails(standard);
        Console.WriteLine("Standard Shipment Printed Successfully.");

        DeliveryHelper.PrintShipmentDetails(express);
        Console.WriteLine("Express Shipment Printed Successfully.");

        DeliveryHelper.PrintShipmentDetails(international);
        Console.WriteLine("International Shipment Printed Successfully.");

        Console.WriteLine("==========================================");
        Console.WriteLine("Updating Weight...");

        Console.WriteLine("Original Weight : " + standard.Weight + " KG");

        standard.UpdateWeight(5);

        Console.WriteLine("Updated Weight : " + standard.Weight + " KG");

        standard.UpdateWeight(5, 0.5m);

        Console.WriteLine(
            "Updated Weight After Packing : "
            + standard.Weight
            + " KG"
        );

        Console.WriteLine("==========================================");
        Console.WriteLine("Printing Using Shipment[]...");

        Shipment[] mixedShipments =
        {
            standard,
            express,
            international
        };

        foreach (Shipment shipment in mixedShipments)
        {
            shipment.PrintShipment();
            Console.WriteLine("------------------------------------------");
        }

        Console.WriteLine("==========================================");

        InternationalShipment priority =
            new PriorityInternationalShipment(
                "SH004",
                "Documents",
                1,
                100,
                address3,
                "France",
                50
            );

        priority.GenerateCustomsReport();

        CompletedShipment completed =
            new CompletedShipment(
                "SH005",
                "Package",
                2,
                50,
                address1
            );

        completed.PrintShipment();

        Console.WriteLine("==========================================");
        Console.WriteLine("Search Shipment:");

        Shipment found = center["SH002"];

        if (found != null)
            Console.WriteLine("Found: " + found.TrackingCode);

        Console.WriteLine("==========================================");
    }
}
*/