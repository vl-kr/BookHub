﻿namespace BusinessLayer.DTOs.Responses.Address;

public class AddressResponse : BaseResponse
{
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
