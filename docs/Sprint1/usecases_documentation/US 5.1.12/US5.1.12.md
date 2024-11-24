# US12 - To create a new staff profile


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to create a new staff profile, so that I can add them to the hospital’s roster.

### 1.2. Acceptance Criteria

- Admins can input staff details such as first name, last name, contact information, and
  specialization.
- A unique staff ID (License Number) is generated upon profile creation.
- The system ensures that the staff’s email and phone number are unique.


### 1.3. Found out Dependencies

* n/a

### 1.4 Input and Output Data

**Input Data:**

* Typed data:
- FirstName (string) - The first name of the new staff member.
- LastName (string) - The last name of the new staff member.
- LicenseNumber (string) - The license number of the new staff member.
- Phone (string) - The phone number of the new staff member.
- Email (string) - The email address of the new staff member.
- Specialization (string) - The specialization of the new staff member.
- AvailabilitySlots (List<string>) - Availability slots for the new staff member.
- 
* Output Data:

* On success:

- New staff member registered in the system.
- 201 Created response, along with the newly created staff member details.
* On failure:

- 400 Bad Request if required fields (first name, last name, license number, phone, or email) are missing.



