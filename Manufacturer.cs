using System;
using System.Collections.Generic;

namespace chizhprimebd;

public partial class Manufacturer
{
    public int IdManufacturers { get; set; }

    public string Manufacturers { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
