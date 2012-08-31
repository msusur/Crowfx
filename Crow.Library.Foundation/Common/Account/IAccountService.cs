
namespace Crow.Library.Foundation.Common.Account
{
    public interface IAccountService
    {
        UserContext CurrentUser { get; set; }
    }
}