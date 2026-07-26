using System;
using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Commands.RemovePhoto;

public class RemovePhotoProfileCommand : IRequest<ApiResponse<Unit>>
{
    public string PhotoId { get; set; } = string.Empty;
}   
