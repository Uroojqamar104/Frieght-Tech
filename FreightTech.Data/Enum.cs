namespace FreightTech.Enum
{
    public enum RowState
    {
        Created = 1,
        Updated = 2,
        Deleted = 3
    }

    public enum UserRole
    {
        SuperAdmin = 1,
        Customer = 2,
        Driver = 3
    }

    public enum DriverStatus
    {
        LoggedIn = 1,
        LoggedOut = 2
    }

    public enum OrderStatus
    {
        New = 1,
        Pending = 2,
        Cancelled = 3,
        Done = 4
    }
}
