using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Template.Core.Extensions;

public static class SecurityKeyExtensions
{
    public static SymmetricSecurityKey ToUtf8SymmetricSecurityKey(this string keyStr)
    {
        var keyBytes = Encoding.UTF8.GetBytes(keyStr);
        var key = new SymmetricSecurityKey(keyBytes);
        return key;
    }
}