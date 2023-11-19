using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.Dto;
using Domain.Models.Identity;
using Domain.Models.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CredLend_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public SignInManager<User> _SignInManager;

        public UserController(IUserRepository<User> userRepository, IUnitOfWork uow, IMapper mapper, IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _uow = uow;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _SignInManager = signInManager;
        }


        [HttpGet("AllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var allUsers = _userManager.Users.ToList();
            return Ok(allUsers);
        }


        [HttpGet("ActiveUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetActiveUsers(){
            var users  = _userManager.Users.ToList();

            var activeUsers = users.Where(user => user.IsActive == true).ToList();
            
            return Ok(activeUsers);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = userDto.UserName,
                        NormalizedEmail = userDto.Email,
                        Email = userDto.Email,
                        CompleteName = userDto.CompleteName,
                        CPF = userDto.CPF,
                        BirthDate = userDto.BirthDate,
                        IsActive = true
                    };


                    var result = await _userManager.CreateAsync(
                        user, userDto.Password
                    );

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());
                        var token = GenerateJWToken(appUser).Result;
                        return Ok(token);
                    }

                }

                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Erro ao salvar as alterações: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Exceção interna: " + ex.InnerException.Message);
                }
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
            }


        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
           {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
           };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);

                if(user.IsActive)
                {
                    var result = await _SignInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == user.Email.ToUpper());

                        var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                        return Ok(new
                        {
                            token = GenerateJWToken(appUser).Result,
                            user = appUser
                        });
                    }

                    return Unauthorized();
                } else
                {
                    return NotFound("Usuário não encontrado!");
                }
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
            }
        }


        /*[Authorize(Roles = "Admin")]
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetById(string UserId)
        {
            var user = await _userRepository.GetById(UserId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(user);
        }*/

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(string UserId)
        {
            var entity = await _userRepository.GetById(UserId);

            if (entity == null) return NotFound();

            entity.IsActive = false;

           await _uow.SaveChangesAsync();

           return Ok();
        }

    }


}