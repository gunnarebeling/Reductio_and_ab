// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Reflection;


List<Product> products = new List<Product>
{
    new Product
    {
        Name = "Wizard Hat",
        Price = 29.99m,
        Available = true,
        ProductTypeId = 1 
    },
    new Product
    {
        Name = "Healing Potion",
        Price = 15.50m,
        Available = true,
        ProductTypeId = 2 
    },
    new Product
    {
        Name = "Magic Wand",
        Price = 45.00m,
        Available = false,
        ProductTypeId = 4 
    },
    new Product
    {
        Name = "Invisibility Cloak",
        Price = 120.00m,
        Available = true,
        ProductTypeId = 1 
    },
    new Product
    {
        Name = "Enchanted Mirror",
        Price = 75.00m,
        Available = true,
        ProductTypeId = 3 
    }
};

List<ProductType> productTypes = new List<ProductType>
{
    new ProductType {Id = 1, Name = "apparel"},
    new ProductType {Id = 2, Name = "potions"},
    new ProductType {Id = 3, Name = "enchanted objects"}, 
    new ProductType {Id = 4, Name = "wands"},
};

string findProductType(Product product)
{
    ProductType type = productTypes.First(p => p.Id == product.ProductTypeId);
    return type.Name;
}
void allProducts()
{
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name} - type: {findProductType(products[i])}");
    }
}

void addProduct()
{
    Console.WriteLine("Enter Name of product:");
    string newName = Console.ReadLine();

    decimal newPrice = 0;
    while(newPrice <= 0)
    {
        Console.WriteLine("Enter price of product:");
        try
        {
            newPrice = decimal.Parse(Console.ReadLine().Trim());
            if (newPrice <= 0)
            {
                Console.WriteLine("price needs to be a positive number over 0");
            }
        }
        catch (FormatException)
        {   
            Console.WriteLine("needs to be in number form");
        }

    }
    int newProductType = choiceSelection(productTypes, type => type.Name, "select the type of product:");
    Product newProduct = new Product
    {
        Name = newName,
        Price = newPrice,
        Available = true,
        ProductTypeId = newProductType
    };

    products.Add(newProduct);
    Console.Clear();
    Console.WriteLine($"{newProduct.Name} has been added to products");
    
}

void deleteProduct()
{
    int choice = choiceSelection(products, product => product.Name, "please select which product to delete:");
    string choiceName = products[choice - 1].Name;
    products.RemoveAt(choice -1);
    Console.WriteLine($"{choiceName} deleted from products");

}
void updateProperty(int productChoice)
{
    List<string> PropertyNames = new List<string> {"Name", "Price", "Available", "Product Type", "Exit"};
        int choice = 0;
        while (choice != 5)
        {
            Console.Clear();
            choice = choiceSelection(PropertyNames, p => p, "select which property to update");
            switch(choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Enter new name:");
                    string oldName = products[productChoice - 1].Name;
                    string newName = Console.ReadLine();
                    products[productChoice - 1].Name = newName;
                    Console.WriteLine($"product name changed from {oldName} to {newName}");
                    Console.ReadKey();

                    break;
                case 2:
                    decimal newPrice = 0;
                    while(newPrice <= 0)
                    {   Console.Clear();
                        Console.WriteLine("Enter new Price");
                        try
                        {
                            newPrice = decimal.Parse(Console.ReadLine().Trim());
                            if (newPrice <= 0)
                            {
                                Console.WriteLine("price needs to be a positive number over 0");
                            }
                        }
                        catch (FormatException)
                        {   
                            Console.WriteLine("needs to be in number form");
                        }

                    }
                    products[productChoice - 1].Price = newPrice;
                    Console.WriteLine($"product price is now {newPrice}");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    List<string> availableList = new List<string> { "yes", "no"};
                    int avChoice = choiceSelection(availableList, a => a, "change if product is available or not:");
                    if (avChoice == 1)
                    {
                        products[productChoice - 1].Available = true;
                        Console.WriteLine("product is now available");
                        Console.ReadKey();
                    }
                    else
                    {
                        products[productChoice - 1].Available = false;
                        Console.WriteLine("product is now not available");
                        Console.ReadKey();
                    }
                    break;
                case 4:
                    Console.Clear();
                    int typeChoice = choiceSelection(productTypes, a => a.Name, "change the type of product it is");
                    products[productChoice - 1].ProductTypeId = typeChoice;
                    Console.WriteLine($"the product type is now {productTypes[typeChoice].Name}");
                    Console.ReadKey();
                    break;
                case 5:
                    break;

            }
            
        }
}
void updateProduct()
{
    bool exit = false;
    int productChoice = 0;
    while (!exit)
    {   Console.Clear();
        Console.WriteLine(@$"select which product to edit:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name}");
        }
        Console.WriteLine($"{products.Count + 1}. Exit");
        try
        {
            productChoice = int.Parse(Console.ReadLine().Trim());
            if (productChoice < 1 || productChoice > products.Count + 1)
            {
                Console.WriteLine("must pick from givin choices");
            }
            else if (productChoice == products.Count + 1)
            {
                exit = true;
                return;
            }     
        }
        catch (FormatException)
        {
            Console.WriteLine("choice must be an integer");   
        }
        
        updateProperty(productChoice);     
    }
}

void searchByType()
{
    int typeOption = choiceSelection(productTypes, t => t.Name,"select what type to search by:");
    List<Product> productsByType = products.Where(p => p.ProductTypeId == typeOption).ToList();
    Console.Clear();
    Console.WriteLine(@$"these products are {productTypes[typeOption - 1].Name}");
    for (int i = 0; i < productsByType.Count; i++)
    {
        Console.WriteLine($"{productsByType[i].Name}");
    }
    Console.ReadKey();
}

int response = 0;
while (response != 6)
{
    Console.Clear();
    Console.WriteLine(@"Please select an option:
                        1. View All Prodcuts
                        2. Add a product
                        3. Delete a product
                        4. Update product
                        5. Search product by type
                        6. Exit");
    try
    {
        response = int.Parse(Console.ReadLine().Trim());
        switch(response)
        {
            case 1:
                Console.Clear();
                allProducts();
                Console.ReadKey();
                break;
                
            case 2:
                Console.Clear();
                addProduct();
                Console.ReadKey();
                break;
            case 3:
                Console.Clear();
                deleteProduct();
                Console.ReadKey();
                break;
            case 4:
                Console.Clear();
                updateProduct();
                break;
            case 5:
                Console.Clear();
                searchByType();
                break;

            case 6:
                Console.Clear();
                Console.WriteLine("BYE!!");
                Console.ReadKey();
                break;
        }
            
    }
    catch (FormatException)
    {
        
        Console.WriteLine("selection needs to be a number");
    }

}

int choiceSelection<T>(List<T> list, Func<T, string> displayName, string selectionMessage)
{
    int Choice = 0;
    while (Choice < 1 || Choice > list.Count)
    {
        Console.WriteLine(selectionMessage);
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i +1}. {displayName(list[i])}");
        }
                        
        try
        {
            Choice = int.Parse(Console.ReadLine().Trim());
            if (Choice < 1 || Choice > productTypes.Count)
            {
                Console.WriteLine($"the type can only be between 1-{list.Count}");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("selection needs to be an integer");
        }                    
    }
    
    return Choice;

}
