namespace CleanArchMvc.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error) { }

        public static void When(bool hasErrro, string error)
        {
            if (hasErrro) 
                throw new DomainExceptionValidation(error);
        }
    }
}
