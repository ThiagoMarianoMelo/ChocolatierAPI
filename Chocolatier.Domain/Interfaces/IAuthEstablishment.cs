namespace Chocolatier.Domain.Interfaces
{
    public interface IAuthEstablishment
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
