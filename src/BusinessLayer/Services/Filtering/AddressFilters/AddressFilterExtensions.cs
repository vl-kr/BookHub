using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.AddressFilters;

public static class AddressFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Address> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b =>
                (b.Street != null && b.Street.ToLower().Contains(normalizedSearchTerm))
                || (b.City != null && b.City.ToLower().Contains(normalizedSearchTerm))
                || (b.PostalCode != null && b.PostalCode.ToLower().Contains(normalizedSearchTerm))
                || (b.Country != null && b.Country.ToLower().Contains(normalizedSearchTerm))
            );
    }

    public static void FilterOn(this EFCoreQueryObject<Address> query, AddressFilter addressFilter)
    {
        if (!string.IsNullOrWhiteSpace(addressFilter.Street))
            query.Filter(b => b.Street != null && b.Street.Contains(addressFilter.Street));
        if (!string.IsNullOrWhiteSpace(addressFilter.City))
            query.Filter(b => b.City != null && b.City.Contains(addressFilter.City));
        if (!string.IsNullOrWhiteSpace(addressFilter.PostalCode))
            query.Filter(b =>
                b.PostalCode != null && b.PostalCode.Contains(addressFilter.PostalCode)
            );
        if (!string.IsNullOrWhiteSpace(addressFilter.Country))
            query.Filter(b => b.Country != null && b.Country.Contains(addressFilter.Country));
    }
}
