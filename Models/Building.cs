namespace deskManagerApi.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Floor> Floors { get; set; }
    }
}
