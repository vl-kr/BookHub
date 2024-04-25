using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.GenreFilters;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IGenreService
{
    Task<ServiceResult<GenreResponse>> CreateGenre(GenreRequest authorRequest);

    Task<ServiceResult<IEnumerable<GenreResponse>>> GetGenres(
        PageOptions pageOptions,
        GenreFilter genreFilter
    );

    Task<ServiceResult<GenreResponse>> GetGenre(int id);
    Task<ServiceResult<GenreResponse>> UpdateGenre(int id, GenreRequest authorRequest);
    Task<ServiceResult<GenreResponse>> DeleteGenre(int id);
    Task<PaginationObject<Genre>> GetPaginatedGenres(PageOptions pageOptions);
}
