using System;
using System.Collections.Generic;

namespace chizhprimebd;

public partial class Model
{
    public int IdModels { get; set; }

    public int? Manufacturers { get; set; }

    public string? Model1 { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual Manufacturer? ManufacturersNavigation { get; set; }
}
