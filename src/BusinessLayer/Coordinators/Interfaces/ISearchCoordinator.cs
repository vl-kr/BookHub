using BusinessLayer.Models;

namespace BusinessLayer.Coordinators.Interfaces;

public interface ISearchCoordinator
{
    Task<SearchResult> Search(string searchTerm);
}
