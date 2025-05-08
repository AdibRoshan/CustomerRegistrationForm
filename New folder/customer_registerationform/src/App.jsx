import React, { useState } from 'react';
import axios from 'axios';

function AddCustomerForm() {
  const [customerData, setCustomerData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    dateOfBirth: '',
    address: '',
    employerName: '',
    jobTitle: '',
    salary: '',
    employmentStartDate: '',
  });

  // Handle input change
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCustomerData({ ...customerData, [name]: value });
  };

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();

    // Make a POST request to the backend API
    axios.post('https://localhost:7106/api/Customer', customerData)
      .then((response) => {
        console.log('Customer added:', response.data);
        alert('Customer added successfully!');
      })
      .catch((error) => {
        console.error('There was an error adding the customer:', error);
        alert('Error adding customer.');
      });
  };

  return (
    <div>
      <h2>Add New Customer</h2>
      <form onSubmit={handleSubmit}>
        <label>
          First Name:
          <input
            type="text"
            name="firstName"
            value={customerData.firstName}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Last Name:
          <input
            type="text"
            name="lastName"
            value={customerData.lastName}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Email:
          <input
            type="email"
            name="email"
            value={customerData.email}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Phone:
          <input
            type="text"
            name="phone"
            value={customerData.phone}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Date of Birth:
          <input
            type="date"
            name="dateOfBirth"
            value={customerData.dateOfBirth}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Address:
          <input
            type="text"
            name="address"
            value={customerData.address}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Employer Name:
          <input
            type="text"
            name="employerName"
            value={customerData.employerName}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Job Title:
          <input
            type="text"
            name="jobTitle"
            value={customerData.jobTitle}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Salary:
          <input
            type="number"
            name="salary"
            value={customerData.salary}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <label>
          Employment Start Date:
          <input
            type="date"
            name="employmentStartDate"
            value={customerData.employmentStartDate}
            onChange={handleInputChange}
            required
          />
        </label>
        <br />
        <button type="submit">Add Customer</button>
      </form>
    </div>
  );
}

export default AddCustomerForm;
