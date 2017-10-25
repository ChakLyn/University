using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityPolicy.Models;

namespace SecurityPolicy.Core
{
    public class SecurityPolicyImitation
    {
        private SecurityPolicyModel _securityPolicyModel;
        private UserModel _currentUser;
        private Random random = new Random();

        public SecurityPolicyImitation(SecurityPolicyModel securityPolicyModel)
        {
            _securityPolicyModel = securityPolicyModel;
            InitializeRightOfTheUsers();
        }

        public void StartImitationSystem()
        {
            bool dialogIsOn = true;
            while (dialogIsOn)
            {
                if (_currentUser != null)
                {
                    WorkWithUser();
                    _currentUser = null;
                }
                else
                {
                    Console.WriteLine("\nEnter your name: ");
                    string name = Console.ReadLine();

                    if ((_currentUser = _securityPolicyModel.
                        Users.
                        FirstOrDefault(user => user.IdUser.ToLower() == name.ToLower())) != null)
                    {
                        Console.WriteLine("Identification passed successfully.");

                        if (_currentUser.IsAdmin)
                            Console.WriteLine("You are admin.");

                        Console.WriteLine("Yours rights: ");
                        PrintRights(_currentUser);
                    }
                    else
                    {
                        Console.WriteLine("We are sorry but there isn't account with such name.");
                    }
                }
            }
        }

        private void WorkWithUser()
        {
            bool inSystem = true;
            while (inSystem)
            {
                Console.Write($"{_currentUser.IdUser}: ");
                string command = Console.ReadLine();

                if (command.ToLower() == "exit")
                    return;

                if (!_securityPolicyModel.Rights.Contains(command.ToLower()))
                {
                    Console.WriteLine("We are sorry but system dosen't know such command");
                    continue;
                }

                Console.Write("-> Enter name of the object: ");
                string objectName = Console.ReadLine();

                if (!_currentUser.Rights.Keys.Contains(objectName))
                {
                    Console.WriteLine($"We are sorry but there isn't such object {objectName}.");
                    continue;
                }

                if (HaveRights(command, objectName))
                {
                    if (command.ToLower() == "grant")
                    {
                        GrantRights(objectName);
                    }
                    Console.WriteLine("Operation seccessfully done.");
                }
            }
        }

        private bool HaveRights(string right, string objectName)
        {
            if (!_currentUser.Rights[objectName].Contains(right))
            {
                Console.WriteLine($"We are soryy but you don't have {right} right for {objectName}");
                return false;
            }
            return true;
        }

        private void GrantRights(string objectName)
        {
            UserModel userModel;
            Console.Write("Enter the name of the right: ");
            string right = Console.ReadLine();
            if (!HaveRights(right, objectName))
            {
                return;
            }

            Console.Write("Enter user name for giving him rights: ");
            string userNameForRights = Console.ReadLine();

            if ((userModel = _securityPolicyModel.
                Users.
                FirstOrDefault(user => user.IdUser.ToLower() == userNameForRights.ToLower())) == null)
            {
                Console.WriteLine("We are sorry but there isn't such user.");
                return;
            }

            if (userModel.Rights[objectName].Contains(right))
            {
                Console.WriteLine($"User {userModel.IdUser} already has {right} for {objectName}.");
                return;
            }

            userModel.Rights[objectName].Add(right);
        }

        private void PrintRights(UserModel currentUser)
        {
            foreach (var objectRights in currentUser.Rights)
            {
                string rights = string.Join(", ", objectRights.Value.ToArray());
                Console.WriteLine($"{objectRights.Key}: {rights}");
            }
        }

        private void InitializeRightOfTheUsers()
        {
            foreach (UserModel user in _securityPolicyModel.Users)
            {
                foreach (string securedObject in _securityPolicyModel.Objects)
                {
                    if (user.IsAdmin)
                    {
                        user.Rights.Add(securedObject, new List<string>(_securityPolicyModel.Rights));
                    }
                    else
                    {
                        var tempRightList = new List<string>(_securityPolicyModel.Rights);
                        int countOfRights = random.Next(0, _securityPolicyModel.Rights.Count);
                        for (int i = 0; i < countOfRights; i++)
                        {
                            string right = tempRightList[random.Next(0, tempRightList.Count)];
                            tempRightList.Remove(right);

                            if (!user.Rights.ContainsKey(securedObject))
                            {
                                user.Rights.Add(securedObject, new List<string>());
                            }

                            user.Rights[securedObject].Add(right);
                        }
                    }
                }
            }
        }
    }
}
