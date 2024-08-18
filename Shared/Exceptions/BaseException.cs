
namespace Shared.Exceptions
{
    public abstract class BaseException : Exception
    {

        public int Code { get; set; }

        protected BaseException(int code, string message) : base(message)
        {
            Code = code;
        }

    }
}
