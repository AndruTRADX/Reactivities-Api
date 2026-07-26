using System;
using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.SetMainPhoto;

public class SetMainPhotoProfileAction : IRequest<ApiResponse<Unit>>
{
    public string PhotoId { get; set; } = string.Empty;
}
