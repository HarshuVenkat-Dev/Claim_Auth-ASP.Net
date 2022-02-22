using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPISecurity.Models;
using Newtonsoft.Json;

namespace WebAPISecurity.Controllers
{
    public class UserController : ApiController
    {
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/server/info")]
        public IHttpActionResult Get()
        {
            return Ok("Server time is: " + DateTime.Now.ToString());
        }

        
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("api/user/normal")]
        public IHttpActionResult ResourceUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + "; Your role is : " + string.Join(",", roles.ToList()));
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/admin")]
        public IHttpActionResult ResourceAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + "; Your role is : " + string.Join(",", roles.ToList()));
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/test")]
        public async Task<List<users>> TestingApiAsync()
        {
            var users =new List<users>() ;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://gorest.co.in/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("public/v2/users");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success", response);
                    string department = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<users>>(department);
                    foreach (var use in users)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", use.id, use.name, use.email, use.status, use.gender);
                    }
                    /*Console.WriteLine("Id:{0}\tName:{1}", department.id, department.name);*/
                    /*                   Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
                    */
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

            }
            return users;
            
        }
    }
}