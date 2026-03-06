namespace HibaVonal.Shared.DTO
{
    public class ApiResponse<T>
    {
        public List<T> Values { get; set; }

        public string Message { get; set; }
    }
}
