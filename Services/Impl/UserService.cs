using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public User FindOne(int id)
        {
            return _userRepository.FindOne(id);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }

        public User FindByEmail(string email, int isGoogle)
        {
            return _userRepository.FindByEmail(email, isGoogle);
        }

        public User FindByPhone(string phone)
        {
            return _userRepository.FindByPhone(phone);
        }

        public User FindByUsernameAndEmail(string username, string email)
        {
            return _userRepository.FindByUsernameAndEmail(username, email);
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return _userRepository.FindByUsernameAndPassword(username, password);
        }

        public List<User> FindByIds(List<int> ids)
        {
            return _userRepository.FindByIds(ids);
        }

        public List<User> FindAllActive()
        {
            return _userRepository.FindAllActive();
        }
    }
} 