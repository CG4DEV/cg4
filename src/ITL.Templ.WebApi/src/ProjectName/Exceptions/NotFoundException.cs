namespace ProjectName.Exceptions
{
    public class NotFoundException : ProjectNameException
    {
        public NotFoundException()
            : base("Resource not found")
        {
        }
    }
}
