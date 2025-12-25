using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.Authentication.Command.Models;
using ThyroCareX.Data.Models;
using ThyroCareX.Data.Models.Identity;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Authentication.Command.Handler
{
    public class AuthentiactionHandler:ResponseHandler,IRequestHandler<RegisterDoctorCommand,Response<string>>
                                                      ,IRequestHandler<SignInCommand,Response<string>>
    {

        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IAuthentcationService _authService;
        #endregion
        #region Constructor
        public AuthentiactionHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager
                                     , SignInManager<User> signInManager, IDoctorService doctorService,
                                       IMapper mapper, IAuthentcationService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _doctorService = doctorService;
            _mapper = mapper;
            _authService = authService;
        }


        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
        {

                if (request.Password != request.ConfirmPassword)
                return new Response<string>("Passwords do not match", false);
            
            var roleExists = await _roleManager.RoleExistsAsync("Doctor");
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole<int>("Doctor"));
                if (!roleResult.Succeeded)
                    return new Response<string>("Failed to create Doctor role", false);
            }


            var user =_mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new Response<string>(string.Join(", ", result.Errors.Select(e => e.Description)), false);

            await _userManager.AddToRoleAsync(user, "Doctor");
            var doctor = _mapper.Map<Doctor>(request);
            doctor.UserId = user.Id;

            var doctorResult = await _doctorService.AddAsync(doctor);
            return new Response<string>("Doctor Registered Successfully", true);
        }



        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check If user is exist or not
            var user=await _userManager.FindByEmailAsync(request.Email.Trim());
            //return the email not found
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                            return BadRequest<string>("Email Or Password Is Wrong");
            //Generate Token
            var accesstoken= await _authService.GetJWTToken(user);
            //return the token
            return Success(accesstoken,"Login Successfully");

        }

        #endregion
    }
}
