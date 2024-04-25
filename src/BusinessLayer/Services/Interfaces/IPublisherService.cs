using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.PublisherFilters;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService
{
    Task<ServiceResult<PublisherResponse>> CreatePublisher(PublisherRequest publisherRequest);

    Task<ServiceResult<IEnumerable<PublisherResponse>>> GetPublishers(
        PageOptions pageOptions,
        PublisherFilter publisherFilter
    );

    Task<ServiceResult<PublisherResponse>> GetPublisher(int id);

    Task<ServiceResult<PublisherResponse>> UpdatePublisher(
        int id,
        PublisherRequest publisherRequest
    );

    Task<ServiceResult<PublisherResponse>> DeletePublisher(int id);

    Task<PaginationObject<Publisher>> GetPaginatedPublishers(PageOptions pageOptions);
}
