using ProjectManagement.DataAccessClasses;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.BusinessClasses
{
    public class UserBC
    {
        ProjectManagerEntities dbContext = null;

        public UserBC()
        {
            dbContext = new ProjectManagerEntities();
        }

        public UserBC(ProjectManagerEntities context)
        {
            dbContext = context;
        }
        public List<Models.User> GetUser()
        {
            using (dbContext)
            {
                return dbContext.Users.Select(x => new Models.User()
                {
                    FirstName = x.First_Name,
                    LastName = x.Last_Name,
                    EmployeeId = x.Employee_ID,
                    UserId = x.User_ID
                }).ToList();
            }

        }

        public int InsertUserDetails(Models.User user)
        {
            using (dbContext)
            {
                dbContext.Users.Add(new User()
                {
                    Last_Name = user.LastName,
                    First_Name = user.FirstName,
                    Employee_ID = user.EmployeeId
                });
                return dbContext.SaveChanges();
            }
        }

        public int UpdateUserDetails(Models.User user)
        {
            using (dbContext)
            {
                var editDetail = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                // Modify existing records
                if (editDetail != null)
                {
                    editDetail.First_Name = user.FirstName;
                    editDetail.Last_Name = user.LastName;
                    editDetail.Employee_ID = user.EmployeeId;

                }
                return dbContext.SaveChanges();
            }

        }

        public int DeleteUserDetails(Models.User user)
        {
            using (dbContext)
            {
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                // Delete existing record
                if (editDetails != null)
                {
                    dbContext.Users.Remove(editDetails);
                }
                return dbContext.SaveChanges();
            }

        }
    }
}