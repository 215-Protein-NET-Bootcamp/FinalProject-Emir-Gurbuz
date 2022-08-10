namespace Core.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
