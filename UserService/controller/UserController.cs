using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Interface;
using UserService.Model;

namespace UserService.controller
{
    [ApiController]
    [Route("userservice/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly UserManager<AppUser> _userManager;

        private readonly ITokenService _tokenService;
        public UserController(UserManager<AppUser> userManager, ITokenService tokenService){
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> UserSignUp([FromBody] SignUpDto signUpDto)
        {
                try{
                    if(!ModelState.IsValid){
                        return BadRequest(ModelState);
                    }
                    if(await signUpDto.Email.IfEmailExists(_userManager)){
                        return BadRequest("Email already exists");
                    }
                    AppUser user = new AppUser{
                        UserName = signUpDto.UserName,
                        Email = signUpDto.Email,
                    };

                    IdentityResult createduser = await _userManager.CreateAsync(user, signUpDto.Password);
                    if(createduser.Succeeded)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");
                        if(roleResult.Succeeded)
                        {
                            IList<string> roles = await _userManager.GetRolesAsync(user);
                            return Ok(new UserDto{
                                Id = user.Id,
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = await _tokenService.CreateToken(user)
                            });
                        }else{
                            return StatusCode(500, "Error while assigning role");
                        }
                    }else{
                            return StatusCode(500, createduser.Errors);
                        }
                }catch(Exception e )
                {
                    return StatusCode(500, e);
                }
        }
    
    
    
        [HttpPost("Login")]
        public async Task <IActionResult> Login([FromBody] LogInDto logInDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(logInDto.Email);
            if(user == null)
                return BadRequest("Username Or Password is incorrect");

            bool result = await _userManager.CheckPasswordAsync(user, logInDto.Password);

            if(result == false)
                return BadRequest("Username Or Password is incorrect");

             return Ok(new UserDto{
                                Id = user.Id,
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = await _tokenService.CreateToken(user)
                            });

        }
    }
}