using System;
using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;
using Reactivities.Application.Specification.Photos;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Profiles.queries.GetPagedPhotos;

public class GetPagedProfilePhotosQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetPagedProfilePhotosQuery, ApiResponse<PagedResponse<PhotoResponse>>>
{
    public async Task<ApiResponse<PagedResponse<PhotoResponse>>> Handle(GetPagedProfilePhotosQuery request, CancellationToken cancellationToken)
    {
        var spec = new PhotoSpecification(request);
        var response = await unitOfWork.Repository<Photo>().GetAllWithSpec(spec);

        var specCount = new PhotoCountSpecification(request);
        var totalCount = await unitOfWork.Repository<Photo>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<Photo>, IReadOnlyList<PhotoResponse>>(response);

        return new ApiResponse<PagedResponse<PhotoResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
