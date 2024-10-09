using System;
namespace e_commerce.Server.DTO.Response
{
    public class ServiceResponseDto<T>
	{
		public T? Data { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = string.Empty;
	}
}