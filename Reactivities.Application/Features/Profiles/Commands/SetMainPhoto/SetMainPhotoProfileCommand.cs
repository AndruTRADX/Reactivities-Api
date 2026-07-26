using System;
using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Commands.SetMainPhoto;

public class SetMainPhotoProfileCommand : IRequest<ApiResponse<Unit>>
{
    public string PhotoId { get; set; } = string.Empty;
}
