# US13 - To edit a staff’s profile


## 1. Requirements Engineering

### 1.1. User Story Description

As an Admin, I want to edit a staff’s profile, so that I can update their information.

### 1.2. Acceptance Criteria

- Admins can search for and select a staff profile to edit.
- Editable fields include contact information, availability slots, and specialization.
- The edited data is updated in real-time across the system.

### 1.3. Found out Dependencies

* Needs to have a staff profile to edit.

### 1.4 Input and Output Data

**Input Data:**

- email (string) - Email address of the staff member to update.
- firstName (string) - New first name for the staff member.
- lastName (string) - New last name for the staff member.
- phone (string) - New phone number for the staff member.
- specializationId (long?) - ID of the new specialization for the staff member.
- availabilitySlots (List<AvailabilitySlot>) - New availability slots for the staff member.
* Output Data:

* On success:

- Staff profile updated in the system.

* On failure:

- 404 Not Found if the staff member with the given email does not exist.

