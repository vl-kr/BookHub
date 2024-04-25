using AutoMapper;
using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.GenreFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class GenreService : IGenreService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public GenreService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<GenreResponse>> CreateGenre(GenreRequest genreRequest)
    {
        var genre = _mapper.Map<Genre>(genreRequest);
        await _uow.GenreRepository.AddAsync(genre);
        await _uow.CommitAsync();
        return new ServiceResult<GenreResponse>(
            _mapper.Map<GenreResponse>(genre),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<GenreResponse>>> GetGenres(
        PageOptions pageOptions,
        GenreFilter genreFilter
    )
    {
        var query = new EFCoreQueryObject<Genre>(_context);

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(genreFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var genres = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<GenreResponse>>(
            genres.Select(_mapper.Map<GenreResponse>)
        );
    }

    public async Task<ServiceResult<GenreResponse>> GetGenre(int id)
    {
        var genre = await _uow.GenreRepository.FindByIdAsync(id);
        if (genre == null)
            return new ServiceResult<GenreResponse>("Genre not found", ServiceResultCode.NotFound);
        return new ServiceResult<GenreResponse>(_mapper.Map<GenreResponse>(genre));
    }

    public async Task<ServiceResult<GenreResponse>> UpdateGenre(int id, GenreRequest genreRequest)
    {
        var existingGenre = await _uow.GenreRepository.FindByIdAsync(id);
        if (existingGenre == null)
            return new ServiceResult<GenreResponse>("Genre not found", ServiceResultCode.NotFound);
        try
        {
            _uow.GenreRepository.Update(_mapper.Map(genreRequest, existingGenre));
            await _uow.CommitAsync();
            return new ServiceResult<GenreResponse>(_mapper.Map<GenreResponse>(existingGenre));
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<GenreResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<GenreResponse>> DeleteGenre(int id)
    {
        var genre = await _uow.GenreRepository.FindByIdAsync(id);
        if (genre == null)
            return new ServiceResult<GenreResponse>("Genre not found", ServiceResultCode.NotFound);
        try
        {
            _uow.GenreRepository.Remove(genre);
            await _uow.CommitAsync();
            return new ServiceResult<GenreResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<GenreResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<PaginationObject<Genre>> GetPaginatedGenres(PageOptions pageOptions)
    {
        var query = new EFCoreQueryObject<Genre>(_context);
        query.SearchFor(pageOptions.SearchTerm);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);
        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        return result;
    }
}
