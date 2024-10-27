# US17 - As a doctor, update an Operation Request

## 1. Requirements Engineering

### 1.1. User Story Description

As a Doctor, I want to update an operation requisition, so that the Patient has access to the necessary healthcare.

### 1.2. Acceptance Criteria

**AC1** - Doctors can update operation requests they created (e.g., change the deadline or priority).

**AC2** - The system checks that only the requesting doctor can update the operation request.

**AC3** - The system logs all updates to the operation request (e.g., changes to priority or deadline).

**AC4** - Updated requests are reflected immediately in the system and notify the Planning Module of
any changes.


### 1.3. Found out Dependencies

* There is a dependency with US16.

### 1.4 Input and Output Data

**Input Data:**

* Typed data:
    * operationRequestId
    * priority
    * deadline


**Output Data:**

* (In)success of an operation
