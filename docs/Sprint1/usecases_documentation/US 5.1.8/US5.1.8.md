# US5.1.8 - To create new patient


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to create a new patient profile, so that I can register their personal details and medical history.


### 1.2. Acceptance Criteria

**AC1** - Admins can input patient details such as first name, last name, date of birth, contact information, and medical history.

**AC2** - A unique patient ID (Medical Record Number) is generated upon profile creation.

**AC3** - The system validates that the patient’s email and phone number are unique.

**AC4** - The profile is stored securely in the system, and access is governed by role-based permissions.

### 1.3. Found out Dependencies

* 

### 1.4 Input and Output Data

**Input Data:**

* Route parameter:
- full name (string) - Patient's full name.
- birthdate (string) - patient's date of birth


- **Output Data:**

* Patient's data
* (In)Success of the operation
