# US18 - To remove an operation Request


## 1. Requirements Engineering

### 1.1. User Story Description

As a Doctor, I want to remove an operation requisition, so that the healthcare activities are provided as necessary.


### 1.2. Acceptance Criteria

**AC1** - Doctors can delete operation requests they created if the operation has not yet been
scheduled.

**AC2** - A confirmation prompt is displayed before deletion.

**AC3** - Once deleted, the operation request is removed from the patient’s medical record and cannot
be recovered.

**AC4** - The system notifies the Planning Module and updates any schedules that were relying on this
request.

### 1.3. Found out Dependencies

* US5.1.16 - We need to create an operationRequest if we want to delete one.

### 1.4 Input and Output Data

**Input Data:**

* Route parameter:
- id (long) - ID of the operation request to delete.

- **Output Data:**

* Filtered requests list
* (In)Success of the operation
