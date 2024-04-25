using AutoMapper;
using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Publisher;

namespace WebMVC.Areas.Admin.Controllers;

public class PublisherController : AdminController
{
    private readonly IMapper _mapper;
    private readonly IPublisherService _publisherService;

    public PublisherController(
        IPublisherService publisherService,
        IMapper mapper,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _publisherService = publisherService;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PublisherCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var publisherResult = await _publisherService.CreatePublisher(
            _mapper.Map<PublisherRequest>(model)
        );

        var handleResult = HandleCreateResult(publisherResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.PublishersTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(PublisherController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Edit(int id)
    {
        var publisherResult = await _publisherService.GetPublisher(id);
        var handleResult = HandleReadResult(publisherResult);
        if (handleResult != null)
            return handleResult;

        var publisher = publisherResult.Data;

        return View(_mapper.Map<PublisherUpdateViewModel>(publisher));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, PublisherUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var publisherResult = await _publisherService.UpdatePublisher(
            id,
            _mapper.Map<PublisherRequest>(model)
        );

        var handleResult = HandleEditResult(publisherResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.PublishersTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(PublisherController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _publisherService.DeletePublisher(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.PublishersTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(PublisherController).Replace("Controller", ""),
            new { Area = "" }
        );
    }
}
