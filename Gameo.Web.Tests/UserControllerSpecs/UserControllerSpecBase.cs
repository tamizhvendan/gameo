﻿using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Controllers;
using Gameo.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.UserControllerSpecs
{
    public abstract class UserControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IUserRepository> UserRepositoryMock;
        protected UserController UserController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            UserController = new UserController(UserRepositoryMock.Object, BranchRepositoryMock.Object);  
        }        
    }
}