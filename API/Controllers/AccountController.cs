using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,
         ITokenService tokenService,
         IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExits(registerDto.Username)) 
            {
                return BadRequest("Username is Taken.");
            }

            var user = _mapper.Map<AppUser>(registerDto);

            using var hmac = new HMACSHA512();
 
            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash( Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt =  hmac.Key ;
   
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
            };
        
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login){

            var user = await _context.Users
            .Include( x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == login.Username);

            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for( int i= 0; i< computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl  = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs
            };
        }

        private async Task<bool> UserExits(string username){
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }   

    }
}