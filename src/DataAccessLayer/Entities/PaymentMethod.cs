﻿namespace DataAccessLayer.Entities;

public class PaymentMethod : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
