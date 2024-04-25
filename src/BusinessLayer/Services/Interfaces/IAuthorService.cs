using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AuthorFilters;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService
{
    Task<ServiceResult<AuthorResponse>> CreateAuthor(AuthorRequest authorRequest);

    Task<ServiceResult<IEnumerable<AuthorResponse>>> GetAuthors(
        PageOptions pageOptions,
        AuthorFilter authorFilter
    );

    Task<ServiceResult<AuthorResponse>> GetAuthor(int id);
    Task<ServiceResult<AuthorResponse>> UpdateAuthor(int id, AuthorRequest authorRequest);
    Task<ServiceResult<AuthorResponse>> DeleteAuthor(int id);

    Task<PaginationObject<Author>> GetPaginatedAuthors(PageOptions pageOptions);
}
