﻿using Sculptor.Common.Models.Order;

namespace Sculptor.Common.Models.Timetable;

public class TimetableVM
{
    public DateTime DateTime { get; set; }

    public List<OrderVM>? Orders { get; set; }
}
