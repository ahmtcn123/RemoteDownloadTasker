
namespace Core.Application.DTOs
{
    public class GenericResponse<T>
    {
        public required bool Success { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
    }
}