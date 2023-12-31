﻿using CollegeManagement_VNR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CollegeManagement_VNR
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new DbClgEntities())
            {

                string[] Admin = { "Admin" };
                string[] Teacher = { "Teacher" };
                string[] Student = { "Student" };
                var adminUsername = (from usertable in context.Users
                                     where usertable.User_Type == 1 && usertable.Username == username
                                     select usertable.Username).ToArray();
                var TeacherUsername = (from usertable in context.Users
                                       where usertable.User_Type == 2 && usertable.Username == username
                                       select usertable.Username).ToArray();
                var StudentUsername = (from usertable in context.Users
                                       where usertable.User_Type == 3 && usertable.Username == username
                                       select usertable.Username).ToArray();





                if (adminUsername.Length > 0)
                {
                    return Admin;
                }
                else if (TeacherUsername.Length > 0)
                {
                    return Teacher;
                }
                else if (StudentUsername.Length > 0)
                {
                    return Student;
                }


                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}