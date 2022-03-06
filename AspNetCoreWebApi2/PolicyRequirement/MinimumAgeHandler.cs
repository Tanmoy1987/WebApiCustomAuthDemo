using System;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreWebApi2 {
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement> {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement) {
          var claim= context.User.FindFirst(c => c.Type== ClaimTypes.DateOfBirth);
          if(claim== null)
            return Task.CompletedTask;
          var provider= CultureInfo.InvariantCulture;
          var dateofBirth= DateTime.ParseExact(claim.Value, "dd-MM-yyyy hh:mm:ss", provider);

          if(DateTime.Now.Year- dateofBirth.Year > 18) {
              context.Succeed(requirement);
          }
          return Task.CompletedTask;
      }
    }
}