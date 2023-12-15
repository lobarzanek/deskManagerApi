namespace deskManagerApi.Models
{
    public enum Role
    {
        Admin = 1,
        User = 2,
    }
    public enum ItemType
    {
        Other = 0,
        Mouse = 1,
        Keyboard = 2,
        Monitor = 3,
    }
    public enum ItemStatus
    {
        Unknown = 0,
        Free = 1,
        Used = 2,
        Broken = 3,
    }
    public enum IssueStatus
    {
        Created = 0,
        New = 1,
        Open = 2,
        InProgress = 3,
        Blocked = 4,
        Done = 5
    }
}
