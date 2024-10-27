# US5.1.9 - To edit patient profile


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to edit an existing patient profile, so that I can update their information when needed.


### 1.2. Acceptance Criteria

**AC1** - Admins can search for and select a patient profile to edit.

**AC2** - Editable fields include name, contact information, medical history, and allergies.

**AC3** - Changes to sensitive data (e.g., contact information) trigger an email notification to the patient.

**AC4** - The system logs all profile changes for auditing purposes

### 1.3. Found out Dependencies

* 

### 1.4 Input and Output Data

**Input Data:**

* Route parameter:
- id (long) - ID of the operation request to delete.

- **Output Data:**

* Filtered requests list
* (In)Success of the operation
