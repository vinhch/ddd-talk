namespace DDDTalk.Infrastructure.Persistence
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }

        string CreationDate { get; set; }

        string ModificationBy { get; set; }

        string ModificationDate { get; set; }
    }
}