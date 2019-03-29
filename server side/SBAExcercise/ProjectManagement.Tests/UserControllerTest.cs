using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using ProjectManagement.Controllers;
using ProjectManagement.BusinessClasses;
using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagement.Tests
{
    class MockProjectManagerEntities : DataAccessClasses.ProjectManagerEntities
    {
        private DbSet<DataAccessClasses.User> _users = null;
        private DbSet<DataAccessClasses.Project> _projects = null;
        private DbSet<DataAccessClasses.Task> _tasks = null;
        public override DbSet<DataAccessClasses.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<DataAccessClasses.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<DataAccessClasses.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var controller = new UserController(new UserBC(context));
            var result = controller.GetUser() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<User>));
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

        [TestMethod]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "indrayan";
            user.LastName = "ganguly";
            user.EmployeeId = "123456";
            user.UserId = 123;
            var controller = new UserController(new UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();

            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Khush";
            user.LastName = "Sengupta";
            user.EmployeeId = "123";
            user.UserId = 503322;

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "KHUSH");
        }

        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Sriparna";
            user.LastName = "Sengupta";
            user.EmployeeId = "503322";
            user.UserId = 503322;

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DataAccessClasses.User>();
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "418220",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DataAccessClasses.User()
            {
                Employee_ID = "503322",
                First_Name = "Sriparna",
                Last_Name = "Sengupta",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

    }
}

