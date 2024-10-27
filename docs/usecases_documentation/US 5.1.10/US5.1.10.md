# US5.1.10 - To delete patient profile


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to delete a patient profile, so that I can remove patients who are no longer under car


### 1.2. Acceptance Criteria

**AC1** - Admins can search for a patient profile and mark it for deletion.

**AC2** - Before deletion, the system prompts the admin to confirm the action.

**AC3** - Once deleted, all patient data is permanently removed from the system within a predefined time frame.

**AC4** - The system logs the deletion for audit and GDPR compliance purposes.

### 1.3. Found out Dependencies

* 

### 1.4 Input and Output Data

**Input Data:**

* Route parameter:
- email (string) - Patient's email address.

- **Output Data:**

* (In)Success of the operation
