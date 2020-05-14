﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : MyController
    {
        //身份验证
        //private Authenticate authenticate = new Authenticate();
        //授权
        //private Authorize authorize = new Authorize();
        public Authenticate authenticate;
        public Authorize authorize;

        public ServiceController(Authorize authorize, IHttpContextAccessor httpContextAccessor,Authenticate authenticate):base(httpContextAccessor)
        {
            this.authenticate = authenticate;
            this.authorize = authorize;

        }

        [HttpPost]
        [Route("~/api/re")]
        public ActionResult GetToRegist()
        {
            string userName = Payload["firstName"];
            string password = Payload["password"];
            string type = Payload["type"];
            //string userName=Payload[""]
            string result=authenticate.Registe(userName, password, int.Parse(type));
            if(result==("用户已存在"))
            {
                return Ok(new 
                {
                    success=false,
                    msg=result,
                    license=""
              
                }
                    );
                
            }
            else
            {
                return Ok(new
                {
                    success = false,
                    msg = "注册成功",
                    license = result
                }
                );
            }


        }

        [HttpPost]
        [Route("~/api/getUsers")]

        public ActionResult GetRegistInfo()
        {
            string AllUser = authenticate.GetUserInfo();
            return Ok(new
            {
                success = true,
                msg = "注册信息",
                data = AllUser

            }
                ) ;
        }
        [HttpPost]
        [Route("~/api/delUsers")]
        public ActionResult DeleteUser()
        {
            string license = Payload["license"];

            string result = authenticate.DeleteUser(license);
            if(result=="不存在此用户")
            {
                return Ok(new
                {
                    success = false,
                    msg = result
                }
                ) ;
            }
            else
            {
                return Ok(new
                {
                    success = true,
                    msg = result

                }
                    ) ;
            }

        }


    }

}
