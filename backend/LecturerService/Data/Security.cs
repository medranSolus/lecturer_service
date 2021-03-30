using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LecturerService.Data
{
    public class Security
    {
        static HashAlgorithm algorithm = SHA256.Create();

        public static string GetHash(string pass)
        {
            StringBuilder str = new StringBuilder();
            foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass)))
                str.Append(b.ToString("X2"));
            return str.ToString();
        }

        public static Model.Lecturer GetLecturer(IIdentity contextIdentity, Model.LSContext dbCtx)
        {
            if (contextIdentity is ClaimsIdentity identity)
            {
                Claim claim = identity.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
                if (claim != null)
                    return dbCtx.Lecturers.Find(claim.Value);
            }
            return null;
        }

        public static bool IsAdmin(IIdentity contextIdentity, Model.LSContext dbCtx)
        {
            Model.Lecturer lc = GetLecturer(contextIdentity, dbCtx);
            if (lc == null)
                return false;
            return lc.RoleTypeID == Role.Admin;
        }
    }
}