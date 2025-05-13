using System;
using System.Collections.Generic;

namespace chizhprimebd;

public partial class Car
{
    public int IdCars { get; set; }

    public int? Manufacturers { get; set; }

    public int? Model { get; set; }

    public double? Price { get; set; }

    public double? Mileage { get; set; }

    public virtual Manufacturer? ManufacturersNavigation { get; set; }

    public virtual Model? ModelNavigation { get; set; }
}
