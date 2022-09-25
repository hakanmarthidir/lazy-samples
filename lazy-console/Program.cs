using System;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Student()
    {
        Console.WriteLine("initialized");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        //Sample 1 - Basic
        Lazy<Student> student1 = new Lazy<Student>();

        //IsValueCreated should be false
        Console.WriteLine("student1 created : " + student1.IsValueCreated);

        //when i try to access it for the first time, it will be initialized.
        student1.Value.FirstName = "Hakan";

        //now IsValueCreated is true
        Console.WriteLine("student created : " + student1.IsValueCreated);

        student1.Value.LastName = "Hidir";
        Console.WriteLine($"{student1.Value.FirstName}");
        Console.WriteLine($"{student1.Value.LastName}");

        //------------------------------
        //Sample 2
        //with specific constructor
        Lazy<Student> student2 = new Lazy<Student>(() => new Student() { FirstName = "Michael", LastName = "Jordan" });

        Console.WriteLine("student2 created : " + student2.IsValueCreated);
        Console.WriteLine($"{student2.Value.FirstName} {student2.Value.LastName}");
        Console.WriteLine("student2 created : " + student2.IsValueCreated);

        //------------------------------
        //Sample 3
        Order order = new Order(Guid.NewGuid());
        Console.WriteLine("CargoChecked : " + order.CargoChecked);
        Console.WriteLine("Order Items : " + order.ItemsChecked);
        Console.WriteLine("CargoName : " + order.CargoFirm.CargoName);

        Console.ReadLine();
    }
}

//SAMPLE 3 
class Order
{
    private Lazy<IList<OrderItem>> _orderItems;
    private Lazy<Cargo> _cargo;
    public Guid OrderId { get; private set; }

    public Order(Guid id)
    {
        OrderId = id;

        _cargo = new Lazy<Cargo>(() =>
        {
            return new Cargo(this.OrderId);
        });

        _orderItems = new Lazy<IList<OrderItem>>(() =>
        {
            return new List<OrderItem>()
            {
                new OrderItem("Item 1"),
                new OrderItem("Item 2")
            };
        });
    }

    public bool CargoChecked
    {
        get
        {
            return _cargo.IsValueCreated;
        }
    }
    public bool ItemsChecked
    {
        get
        {
            return _orderItems.IsValueCreated;
        }
    }

    public Cargo CargoFirm
    {
        get
        {
            return _cargo.Value;
        }
    }
}

class OrderItem
{
    public string Name { get; private set; }

    public OrderItem(string itemName)
    {
        this.Name = itemName;
    }
}

class Cargo
{
    public Guid OrderId { get; private set; }
    public string CargoName { get; private set; }

    public Cargo(Guid orderId)
    {
        Console.WriteLine("Cargo created.");
        this.OrderId = orderId;
        this.CargoName = "XYZ Cargo";
    }
}
