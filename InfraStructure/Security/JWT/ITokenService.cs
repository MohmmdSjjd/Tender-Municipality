using Domain.Models.User;

namespace InfraStructure.Security.JWT;

public interface ITokenService
{
    string GenerateToken(TenderUser user, IList<string> userRolesList);
}