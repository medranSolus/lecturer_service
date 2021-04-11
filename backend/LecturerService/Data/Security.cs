using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

        public static Model.Lecturer GetLecturer(HttpContext context, Model.LSContext dbCtx)
        {
            Claim claim = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
            if (claim != null)
                return dbCtx.Lecturers.Find(claim.Value);
            return null;
        }

        public static bool IsAdmin(HttpContext context, Model.LSContext dbCtx)
        {
            Model.Lecturer lc = GetLecturer(context, dbCtx);
            if (lc == null)
                return false;
            return lc.RoleTypeID == Role.Admin;
        }
    }
}