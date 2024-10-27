# US15 - List/search staff profiles


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to list/search staff profiles, so that I can see the details, edit, and remove staff profiles.
### 1.2. Acceptance Criteria

- Admins can search staff profiles by attributes such as name, email, or specialization.
- The system displays search results in a list view with key staff information (name, email,
  specialization).
- Admins can select a profile from the list to view, edit, or deactivate.

### 1.3. Found out Dependencies

* Needs to have staff profile to list.

### 1.4 Input and Output Data

**Input Data:**

- email (string) - Email address of the staff member to update.
- FullName (string) - New first name for the staff member.
- specialization- Specialization of the staff member.
* Output Data:

* On success:

- Displays the Staff List.

* On failure:

- 404 Not Found if the staff member with the given email does not exist.

