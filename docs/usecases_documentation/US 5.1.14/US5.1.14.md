# US14 - Deactivate a staff profile.


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to deactivate a staff profile, so that I can remove them from the hospital’s active roster without losing their historical data.
### 1.2. Acceptance Criteria

- Admins can search for and select a staff profile to edit.
- Deactivating a staff profile removes them from the active roster, but their historical data (e.g.,
  appointments) remains accessible.
### 1.3. Found out Dependencies

* Needs to have a staff profile to deactivate

### 1.4 Input and Output Data

**Input Data:**

- ID (license number) 
* Output Data:

* On success:

- Staff profile deactivated in the system.

* On failure:

- 404 Not Found if the staff member with the given email does not exist.

