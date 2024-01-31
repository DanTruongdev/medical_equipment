namespace MedicalEquipmentWeb.Services.Model
{
    public class ServiceResponse <T>
    {
      

        public bool Succeeded { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
        public ServiceResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public ServiceResponse(bool succeeded, string? message)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public ServiceResponse(bool succeeded, string? message, T? data)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
        }
    }
}
