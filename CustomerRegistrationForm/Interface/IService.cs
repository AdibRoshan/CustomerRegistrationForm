using CustomerRegistrationForm.Model;

namespace CustomerRegistrationForm.Interface
    {
    public interface IService
        {
        Task<List<Customer>> GetAllAsync ( ); 
        Task<Customer> GetByIdAsync ( int id ); 
        Task AddAsync ( Customer customer );
        Task<Customer> UpdateAsync ( Customer customer ); 
        Task DeleteAsync ( int id ); 
        }
    }
