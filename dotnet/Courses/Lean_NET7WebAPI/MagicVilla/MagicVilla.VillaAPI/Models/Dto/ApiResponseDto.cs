using System.Net;

namespace MagicVilla.VillaAPI.Models.Dto;

public class ApiResponseDto
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
}