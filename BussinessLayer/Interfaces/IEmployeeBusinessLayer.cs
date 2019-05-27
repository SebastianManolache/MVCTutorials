using BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IEmployeeBusinessLayer
    {
        List<Employee> GetEmployees();

        Task<Employee> SaveEmployeeAsync(Employee employee);

        UserStatus GetUserValidity(UserDetails user);

        void UploadEmployees(List<Employee> employees);
    }
}
