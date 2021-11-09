using System;
class View
{
    public Controller controller;
    public View(){}
    public string Input()
    {
        controller = new Controller();
        Console.WriteLine("What you want to do with the shop?");
        Console.WriteLine("Enter:\n\t1.1 - Insert order\n\t1.2 - Insert product\n\t1.3 - Insert category\n\t1.4 - Insert section");
        Console.WriteLine("\n\t2.1 - Delete order\n\t2.2 - Delete product\n\t2.3 - Delete category\n\t2.4 - Delete section");
        Console.WriteLine("\n\t'random {num}' - to generate random entities\n\t'order {id}' - to take a list of products\n\tcat {section name} - to take categories of section\n\tprod {category name} - to take products of categories");
        return Console.ReadLine();
    }
}