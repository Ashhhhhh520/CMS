using CMS.Auth;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        //private readonly IFreeSql freeSql;

        //public AccountController(IFreeSql freeSql)
        //{
        //    this.freeSql = freeSql;
        //}

        [HttpPost("Login")]
        public async Task<LoginResultModel> Login(UserModel user)
        {
            return new LoginResultModel { Code = 0, Msg = "登录成功" };

            //// 验证登录
            //var db_user = await freeSql.Select<users>()
            //    .Where(a => a.UserName==user.UserName)
            //    .FirstAsync();

            //if (db_user == null)
            //    return new LoginResultModel {  Msg = "该用户未注册!" };

            //if (user.Password != db_user.Password)
            //    return new LoginResultModel { Msg = "密码错误!" };

            ////var pwd = Extensions.CreateMd5(user.Password);
            ////if (pwd != db_user.ygmm)
            ////    return new LoginResultModel { Msg = "密码错误!" };

            //// 写入jwt的body中的信息
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Sid,db_user.ID.ToString()),
            //    new Claim(ClaimTypes.Name,db_user.Name),
            //};
            //// 生成 token
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_key_secret_key_secret_key_secret_key"));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var tokenOptions = new JwtSecurityToken(
            //    issuer: "cms.jwt",
            //    audience: "https://localhost:5126",
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(7),
            //    signingCredentials: creds
            //    );
            //var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            //// 写入到cookies中
            //Response.Cookies.Append("access_token", token);

            //return new LoginResultModel { Code = 1, Msg = "登录成功" };
        }

        [HttpPost("Logout")]
        public async Task<string> Logout()
        {
            HttpContext.Response.Cookies.Delete("access_token");
            return "OK";
        }

        [HttpGet]
        public string Test()
        {
            return "1";
        }
    }
}
